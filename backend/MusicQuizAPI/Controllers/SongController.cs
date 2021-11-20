﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Helpers;
using MusicQuizAPI.Extensions;
using MusicQuizAPI.Models.Parameters;
using MusicQuizAPI.Models.Database;
using System.Net;
using AutoMapper;
using MusicQuizAPI.Models.Dtos;

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/song")]
    public class SongController : ControllerBase
    {
        #region Properties
        SongService SongService { get; set; }
        UserService UserService { get; set; }
        FavoriteSongService FavoriteSongService { get; set; }
        ILogger<SongController> Logger { get; set; }
        ResponseContext ResponseContext { get; set; }
        IMapper Mapper { get; set; }
        #endregion

        

        public SongController(ILogger<SongController> logger, SongService songService,
            UserService userService, FavoriteSongService favoriteSongService,
            IMapper mapper)
        {
            Logger = logger;
            SongService = songService;
            UserService = userService;
            FavoriteSongService = favoriteSongService;
            ResponseContext = new ResponseContext();
            Mapper = mapper;
        }



        #region Methods
        [HttpGet("random")]
        public IActionResult GetRandomSongs([FromQuery] RandomSongParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<Song> songs = SongService.GetRandomSongs(parameters.Count, parameters.Difficulty);

            ResponseContext.AddData(songs.Select(s => 
            {
                var song = Mapper.Map<SongReadDto>(s);
                song.UserScore = FavoriteSongService.GetFavoriteScore(CurrentUser.UserID, s.SongID);
                return song;

            }));

            return Ok(ResponseContext.Body);
        }


        [HttpGet]
        public IActionResult SearchSongs([FromQuery] SearchSongParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<Song> songs = SongService.SearchSong(parameters.Title, parameters.SearchType);

            ResponseContext.AddData(songs.Select(s => 
            {
                var song = Mapper.Map<SongReadDto>(s);
                song.UserScore = FavoriteSongService.GetFavoriteScore(CurrentUser.UserID, s.AnimeID);
                return song;

            }));

            return Ok(ResponseContext.Body);
        }


        [HttpPost("favorites")]
        public IActionResult AddFavoriteSong([FromBody] AddFavoriteParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FavoriteSong newFavoriteSong = new FavoriteSong
            {
                UserID = CurrentUser.UserID,
                SongID = parameters.ID,
                Score = parameters.Score
            };

            FavoriteSongService.AddFavorite(newFavoriteSong);
            
            ResponseContext.AddData($"The song [{newFavoriteSong.SongID}] " +
                $"was added successfully to the user [{newFavoriteSong.UserID}]!");

            return Created("", ResponseContext.Body);
        }


        [HttpDelete("favorites")]
        public IActionResult RemoveFavoriteSong([FromBody] RemoveFavoriteParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FavoriteSong favoriteSong = new FavoriteSong
            {
                UserID = CurrentUser.UserID,
                SongID = parameters.ID
            };

            FavoriteSongService.RemoveFavorite(favoriteSong);
            
            ResponseContext.AddData($"The song [{favoriteSong.SongID}] was removed successfully " +
                $"from the user [{favoriteSong.UserID}]!");

            return Ok(ResponseContext.Body);
        }


        [HttpPatch("favorites")]
        public IActionResult UpdateFavoriteSong([FromBody] UpdateFavoriteParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FavoriteSong favoriteSong = new FavoriteSong
            {
                UserID = CurrentUser.UserID,
                SongID = parameters.ID,
                Score = parameters.Score
            };

            FavoriteSongService.UpdateFavorite(favoriteSong);

            ResponseContext.AddData($"The song [{favoriteSong.SongID}] updated successfully " +
                $"for the user [{favoriteSong.UserID}]!");

            return Ok(ResponseContext.Body);
        }


        [HttpGet("favorites")]
        public IActionResult GetFavoriteSongs()
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<FavoriteSong> favorites = FavoriteSongService.GetFavorites(CurrentUser);

            ResponseContext.AddData(favorites.Select(fs =>
            {
                var song = Mapper.Map<SongReadDto>(SongService.GetSong(fs.SongID));
                song.UserScore = fs.Score;
                return song;
            }));

            return Ok(ResponseContext.Body);
        }
        #endregion
    }
}