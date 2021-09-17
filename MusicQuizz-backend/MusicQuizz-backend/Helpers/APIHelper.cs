using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using MusicQuizz_backend.Models;
using System.Text;
using System.Linq;

namespace MusicQuizz_backend.Helpers
{
    public static class APIHelper
    {
        private static readonly string _APIURL = "https://openings.moe/";
        private static readonly string _APIListURL = "https://openings.moe/api/list.php";
        private static readonly string _APIDetailURL = "https://openings.moe/api/details.php";

        public static List<AnimeModel> GetAnimes()
        {
            WebClient wc = new WebClient();
            string json = wc.DownloadString(_APIListURL);
            return JsonConvert.DeserializeObject<List<AnimeModel>>(json);
        }

        public static DetailedAnimeModel GetAnimeDetails(AnimeModel anime)
        {
            StringBuilder BobTheBuilder = new StringBuilder();
            BobTheBuilder.Append(_APIDetailURL);
            BobTheBuilder.Append("?name=");
            BobTheBuilder.Append(anime.uid);

            WebClient wc = new WebClient();
            
            string json = wc.DownloadString(BobTheBuilder.ToString());
            BobTheBuilder.Clear();

            var animeResult = JsonConvert.DeserializeObject<DetailedAnimeModel>(json);

            BobTheBuilder.Append(_APIURL);
            BobTheBuilder.Append("video/");
            BobTheBuilder.Append(animeResult.file);
            BobTheBuilder.Append(".webm");

            animeResult.file = BobTheBuilder.ToString();

            return animeResult;
        }

        public static List<DetailedAnimeModel> GetAnimesDetails(List<AnimeModel> animes)
        {
            WebClient wc = new WebClient();
            StringBuilder BobTheBuilder = new StringBuilder();
            
            List<DetailedAnimeModel> result = animes.Select(anime => 
            {
                BobTheBuilder.Append(_APIDetailURL);
                BobTheBuilder.Append("?name=");
                BobTheBuilder.Append(anime.uid);

                string json = wc.DownloadString(BobTheBuilder.ToString());
                BobTheBuilder.Clear();

                var animeResult = JsonConvert.DeserializeObject<DetailedAnimeModel>(json);

                BobTheBuilder.Append(_APIURL);
                BobTheBuilder.Append("video/");
                BobTheBuilder.Append(animeResult.file);
                BobTheBuilder.Append(".webm");

                animeResult.file = BobTheBuilder.ToString();
                BobTheBuilder.Clear();

                return animeResult;
            }).ToList();

            return result;
        }
    }
}