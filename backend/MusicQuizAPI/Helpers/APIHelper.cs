using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Text;
using System.Linq;
using System;
using Newtonsoft.Json;
using MusicQuizAPI.Models;


namespace MusicQuizAPI.Helpers
{
    public static class APIHelper
    {
        private static readonly string _APIURL = "https://openings.moe/";
        private static readonly string _APIListURL = "https://openings.moe/api/list.php";
        private static readonly string _APIDetailURL = "https://openings.moe/api/details.php";

        // Unofficial MAL Api
        private static readonly string _APITopAnimesURL = "https://api.jikan.moe/v3/search/anime?q=&order_by=members";
        private static readonly HttpClient _client = new HttpClient();

        private static async Task<string> GetJson(string url)
        {
            var result = await _client.GetAsync(url);
            return await result.Content.ReadAsStringAsync();
        }

        public async static Task<List<AnimeModel>> GetAnimes()
        {
            string json = await GetJson(_APIListURL);
            return JsonConvert.DeserializeObject<List<AnimeModel>>(json);
        }

        public static DetailedAnimeModel GetAnimeDetails(AnimeModel anime)
        {
            var BobTheBuilder = new StringBuilder();
            BobTheBuilder.Append(_APIDetailURL);
            BobTheBuilder.Append("?name=");
            BobTheBuilder.Append(anime.uid);

            string json = GetJson(BobTheBuilder.ToString()).Result;
            var animeResult = JsonConvert.DeserializeObject<DetailedAnimeModel>(json);
            
            BobTheBuilder.Clear();
            BobTheBuilder.Append(_APIURL);
            BobTheBuilder.Append("video/");
            BobTheBuilder.Append(animeResult.file);
            BobTheBuilder.Append(".webm");

            animeResult.file = BobTheBuilder.ToString();
            animeResult.difficulty = anime.difficulty;

            return animeResult;
        }

        public static async Task<List<DetailedAnimeModel>> GetAnimesDetails(List<AnimeModel> animes)
        {
            var BobTheBuilder = new StringBuilder();
            
            List<DetailedAnimeModel> result = animes.Select(anime => 
            {
                BobTheBuilder.Append(_APIDetailURL);
                BobTheBuilder.Append("?name=");
                BobTheBuilder.Append(anime.uid);

                string json = GetJson(BobTheBuilder.ToString()).Result;
                var animeResult = JsonConvert.DeserializeObject<DetailedAnimeModel>(json);

                BobTheBuilder.Clear();
                BobTheBuilder.Append(_APIURL);
                BobTheBuilder.Append("video/");
                BobTheBuilder.Append(animeResult.file);
                BobTheBuilder.Append(".webm");

                animeResult.file = BobTheBuilder.ToString();
                animeResult.difficulty = anime.difficulty;
                BobTheBuilder.Clear();

                return animeResult;
            }).ToList();

            return await Task<List<DetailedAnimeModel>>.Factory.StartNew(() => result);
        }        

        public static List<AnimeModel> GetTopAnimes()
        {
            var topAnimes = new List<AnimeModel>();
            var BobTheBuilder = new StringBuilder();

            for (int i = 1; i <= 6; i++)
            {
                BobTheBuilder.Append(_APITopAnimesURL);
                BobTheBuilder.Append("&page=");
                BobTheBuilder.Append(i);

                string json = GetJson(BobTheBuilder.ToString()).Result;
                dynamic animeResult = JsonConvert.DeserializeObject(json);

                try
                {
                    foreach(var anime in animeResult.results)
                    {
                        topAnimes.Add(new AnimeModel { source = @anime.title });
                    }
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
                {
                    Console.WriteLine(string.Format("Error in top anime on page {0}!\n{1}", i, ex.Message));
                }
                finally { BobTheBuilder.Clear(); }

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return topAnimes;
        }
    }
}