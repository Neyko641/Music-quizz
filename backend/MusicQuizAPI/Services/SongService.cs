using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using MusicQuizAPI.Models.API;
using MusicQuizAPI.Helpers;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Services
{
    public class SongService
    {
        private readonly ILogger<SongService> _logger;
        private readonly Random _rnd;
        private readonly SongRepository _songRepo;
        private readonly AnimeRepository _animeRepo;
        private int _songCount;

        public SongService(ILogger<SongService> logger, SongRepository songRepo,
            AnimeRepository animeRepo)
        {
            _logger = logger;
            _songRepo = songRepo;
            _animeRepo = animeRepo;
            _songCount = _songRepo.Count;

            _rnd = new Random();
        }

        public List<Song> GetRandomSongs(int count, string difficulty)
        {
            List<Song> songs = new List<Song>();
            Song song;
            int index;

            while (count > 0)
            {
                index = _rnd.Next(_songCount);
                song = _songRepo.Get(index);

                if (!songs.Contains(song))
                {
                    if (song.Difficulty == difficulty)
                    {
                        song.Anime = _animeRepo.Get(song.AnimeID);
                        songs.Add(song);
                        count--;
                    }
                }
            }

            return songs;
        }

        public List<Song> SearchSong(string title, string searchType) 
        {
            List<Song> songs = new List<Song>();

            if (!string.IsNullOrWhiteSpace(title))
            {
                switch (searchType)
                {
                    case "anime-title": 
                        songs = _songRepo.GetAllThatContainsAnimeTitle(title.ToLower()).ToList();
                        songs.ForEach(s => s.Anime = _animeRepo.Get(s.AnimeID)); 
                        break;
                    case "song-title": 
                        songs = _songRepo.GetAllThatContainsSongTitle(title.ToLower()).ToList(); 
                        songs.ForEach(s => s.Anime = _animeRepo.Get(s.AnimeID)); 
                        break;
                }
            }

            return songs;
        }

        public Song GetSong(int id)
            => _songRepo.Get(id);
    }
}