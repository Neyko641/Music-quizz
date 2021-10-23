using System.Collections.Generic;
using System.Linq;
using System;
using MusicQuizAPI.Models.API;
using MusicQuizAPI.Helpers;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using System.Diagnostics;

namespace MusicQuizAPI.Services
{
    public class InitialDatabaseService
    {
        private readonly ILogger<InitialDatabaseService> _logger;
        private readonly MusicQuizRepository _repo;
        private readonly AnimeService _animeService;

        public InitialDatabaseService(ILogger<InitialDatabaseService> logger, MusicQuizRepository repo, AnimeService animeService)
        {
            _logger = logger;
            _repo = repo;
            _animeService = animeService;
        }

        public void InitializeAnimesAndSongs()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            _logger.LogInformation("Start ----\nInitiating the database tables 'Animes' and 'Songs' seeding!");
            
            _logger.LogInformation("1. Gettings all animes! ...");
            List<AnimeModel> animes = _animeService.GetAllAnimesFromAPI();
            _logger.LogInformation("... All animes are here.");

            int animeCount = animes.Count;
            int detailedAnimeCount = 0;
            int animeAddedToDb = 0;
            int songAddedToDb = 0;

            _logger.LogInformation("2. Gettings details of all anime! ...");

            foreach (AnimeModel anime in animes)
            {
                if (anime != null && !string.IsNullOrWhiteSpace(anime.source))
                {
                    if (!_repo.ExistAnime(anime.source))
                    {
                        if (_repo.AddAnime(new Anime { Title = anime.source }) > 0) animeAddedToDb++;
                    }
                    
                    DetailedAnimeModel detailedAnime = APIHelper.GetAnimeDetails(anime);
                    detailedAnimeCount++;

                    Anime animeDb = _repo.GetAnimeByTitle(anime.source);

                    if (animeDb.Songs == null) animeDb.Songs = new List<Song>();

                    if (detailedAnime.song != null && !string.IsNullOrWhiteSpace(detailedAnime.song.title) 
                        && !animeDb.Songs.Any(s => s.Title == detailedAnime.song.title))
                    {
                        Song song;
                        try 
                        {
                            song = new Song
                            {
                                AnimeID = animeDb.AnimeID,
                                Title = detailedAnime.song.title,
                                Anime = animeDb,
                                URL = detailedAnime.file,
                                SongType = detailedAnime.type,
                                DetailedSongType = detailedAnime.uid.Split('-')[0],
                                Artist = detailedAnime.song.artist,
                                Difficulty = detailedAnime.difficulty
                            };
                        }
                        catch (Exception) {continue;}

                        if (_repo.AddSong(song) > 0)
                        {
                            if (animeDb.Songs.Any(s => s.Title == detailedAnime.song.title))
                            {
                                songAddedToDb++;
                            }
                        }
                    }
                }
            }

            watch.Stop();
            _logger.LogInformation("... All possible detailed animes are added to the database!");

            _logger.LogInformation($"Normal animes get - {animeCount}\nDetailed animes get - {detailedAnimeCount}\n"
            + $"Animes added to the database - {animeAddedToDb}\nSongs added to database - {songAddedToDb}\n\n"
            + $"Time elapsed - {watch.Elapsed.Minutes} Minutes.\n ---- End!");
        }
    
        public void InitializeTopAnimes()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            _logger.LogInformation("Start ----\nInitiating the database table 'TopAnimes'!");

            _logger.LogInformation("1. Gettings all top anime titles! ...");
            List<string> topTitles = APIHelper.GetTopTitles();
            _logger.LogInformation("... All top anime titles are here.");

            int addedAnimesCount = 0;

            _logger.LogInformation("2. Gettings details of all anime! ...");
            for (int i = 0; i < topTitles.Count; i++)
            {
                string title = topTitles[i];
                if (!_repo.ExistTopAnime(title))
                {
                    TopAnime anime = new TopAnime { Title = title, Rank = i+1 };

                    if (_repo.AddTopAnime(anime) > 0) addedAnimesCount++;
                }
            }

            watch.Stop();
            _logger.LogInformation("... All possible top animes are added to the database!");

            _logger.LogInformation($"Top animes added - {addedAnimesCount}\n\n"
            + $"Time elapsed - {watch.Elapsed.Seconds} Seconds.\n ---- End!");
        }
    }
}