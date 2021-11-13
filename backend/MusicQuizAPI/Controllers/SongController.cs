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

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/song")]
    public class SongController : ControllerBase
    {
        SongService SongService  { get; set; }
        UserService UserService  { get; set; }
        FavoriteSongService FavoriteSongService  { get; set; }
        ILogger<SongController> Logger  { get; set; }
        ResultContext Result { get; set; }

        public SongController(ILogger<SongController> logger, SongService songService, 
            UserService userService, FavoriteSongService favoriteSongService)
        {
            Logger = logger;
            SongService = songService;
            UserService = userService;
            FavoriteSongService = favoriteSongService;
            Result = new ResultContext();
        }


        [HttpGet("random")]
        public IActionResult Random([FromQuery]RandomSongParamModel parameters) 
        {
            List<Song> songs = SongService.GetRandomSongs(parameters.Count, parameters.Difficulty);

            Result.AddData(songs.Select(s => ModelConverter.FromSong(s))); 
            
            return Result.Result();
        }


        [HttpGet("search")]
        public IActionResult Search([FromQuery]SearchSongParamModel parameters)
        {
            List<Song> songs = SongService.SearchSong(parameters.Title, parameters.SearchType);

            Result.AddData(songs.Select(s => ModelConverter.FromSong(s)));
            
            return Result.Result();
        }


        [HttpPost("add-favorite")]
        public IActionResult AddToFavorites([FromQuery]AddFavoriteParamModel parameters) 
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (CurrentUser != null) 
            {
                FavoriteSong newFavoriteSong = new FavoriteSong
                {
                    UserID = CurrentUser.UserID,
                    SongID = parameters.ID,
                    Score = parameters.Score
                };

                if (FavoriteSongService.AddFavorite(newFavoriteSong))
                {
                    Result.AddData($"The song [{newFavoriteSong.SongID}] " +
                        $"was added successfully to the user [{newFavoriteSong.UserID}]!",
                        HttpStatusCode.Created);
                }
                else
                {
                    Result.AddException($"The song [{newFavoriteSong.SongID}] " + 
                        $"is already in favorites for user [{newFavoriteSong.UserID}] or doesn't exist!",
                        ExceptionCode.AlreadyInOrDoesNotExistArgument);
                    }
            }
            else
            {
                Result.AddException("Unauthorized user!", 
                    ExceptionCode.Unauthorized, HttpStatusCode.Unauthorized);
            }

            return Result.Result();
        }


        [HttpDelete("remove-favorite")]
        public IActionResult RemoveFromFavorites([FromQuery]RemoveFavoriteParamModel parameters) 
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (CurrentUser != null) 
            {
                FavoriteSong favoriteSong = new FavoriteSong
                {
                    UserID = CurrentUser.UserID,
                    SongID = parameters.ID
                };

                if (FavoriteSongService.RemoveFavorite(favoriteSong))
                {
                    Result.AddData($"The song [{favoriteSong.SongID}] was removed successfully " + 
                        $"from the user [{favoriteSong.UserID}]!");
                }
                else
                {
                    Result.AddException($"The song [{favoriteSong.SongID}] is already " +
                        $"not in favorites for user {favoriteSong.UserID}!",
                        ExceptionCode.AlreadyInOrDoesNotExistArgument);
                }
            }
            else
            {
                Result.AddException("Unauthorized user!", 
                    ExceptionCode.Unauthorized, HttpStatusCode.Unauthorized);
            }

            return Result.Result();
        }


        [HttpPatch("update-favorite")]
        public IActionResult UpdateFavorite([FromQuery]UpdateFavoriteParamModel parameters) 
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (CurrentUser != null) 
            {
                FavoriteSong favoriteSong = new FavoriteSong
                {
                    UserID = CurrentUser.UserID,
                    SongID = parameters.ID,
                    Score = parameters.Score
                };

                if (FavoriteSongService.UpdateFavorite(favoriteSong))
                {
                    Result.AddData($"The song [{favoriteSong.SongID}] updated successfully " + 
                        $"for the user [{favoriteSong.UserID}]!");
                }
                else
                {
                    Result.AddException($"The song [{favoriteSong.SongID}] doesn't " +
                        $"exist in favorites for user [{favoriteSong.UserID}]!",
                        ExceptionCode.AlreadyInOrDoesNotExistArgument);
                }
            }
            else
            {
                Result.AddException("Unauthorized user!", 
                    ExceptionCode.Unauthorized, HttpStatusCode.Unauthorized);
            }

            return Result.Result();
        }


        [HttpGet("get-favorites")]
        public IActionResult GetFavorites() 
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (CurrentUser != null) 
            {
                List<FavoriteSong> favorites = FavoriteSongService.GetFavorites(CurrentUser);
                Result.AddData(favorites.Select(fs 
                    => ModelConverter.FromSong(fs.Song, fs.Score)));        
            }
            else
            {
                Result.AddException("Unauthorized user!", 
                    ExceptionCode.Unauthorized, HttpStatusCode.Unauthorized);
            }

            return Result.Result();
        }
    }
}