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
        private readonly Random _rnd = new Random();
        private readonly MusicQuizRepository _repo;
        private int _animesCount;
        private int _songCount;

        public SongService(ILogger<SongService> logger, MusicQuizRepository repo)
        {
            _logger = logger;
            _repo = repo;
            _animesCount = _repo.AnimesCount;
            _songCount = _repo.SongCount;
        }

        public List<Song> GetRandomSongs(int count, string difficulty)
        {
            List<Song> songs = new List<Song>();
            Song song;
            int index;

            while (count > 0)
            {
                index = _rnd.Next(_songCount);
                song = _repo.GetSongByID(index);

                if (!songs.Contains(song))
                {
                    if (song.Difficulty == difficulty)
                    {
                        song.Anime = _repo.GetAnimeByID(song.AnimeID);
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
                        songs = _repo.GetSongsThatContainsAnimeTitle(title.ToLower()).ToList();
                        songs.ForEach(s => s.Anime = _repo.GetAnimeByID(s.AnimeID)); 
                        break;
                    case "song-title": 
                        songs = _repo.GetSongsThatContainsSongTitle(title.ToLower()).ToList(); 
                        songs.ForEach(s => s.Anime = _repo.GetAnimeByID(s.AnimeID)); 
                        break;
                }
            }

            return songs;
        }
    }
}