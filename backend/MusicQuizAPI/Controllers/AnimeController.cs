﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Helpers;
using MusicQuizAPI.Models.Parameters;
using MusicQuizAPI.Models.API;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using MusicQuizAPI.Models.Database;

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/anime")]
    public class AnimeController : ControllerBase
    {
        AnimeService AnimeService { get; set; }
        UserService UserService { get; set; }
        FavoriteAnimeService FavoriteAnimeService { get; set; }
        ResultContext Result { get; set; }

        public AnimeController(AnimeService animeService, UserService userService,
            FavoriteAnimeService favoriteAnimeService)
        {
            AnimeService = animeService;
            UserService = userService;
            FavoriteAnimeService = favoriteAnimeService;
            Result = new ResultContext();
        }

        [HttpGet("search")]
        public IActionResult Search(string title) 
        {
            List<Anime> foundedAnimes = AnimeService.SearchAnime(title);
            Result.AddData(foundedAnimes);
            
            return Result.Result();
        }

        [HttpPost("add-favorite")]
        public IActionResult AddToFavorites([FromQuery]AddFavoriteParamModel parameters) 
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (CurrentUser != null) 
            {
                FavoriteAnime newFavoriteAnime = new FavoriteAnime
                {
                    UserID = CurrentUser.UserID,
                    AnimeID = parameters.ID,
                    Score = parameters.Score
                };

                if (FavoriteAnimeService.AddFavorite(newFavoriteAnime))
                {
                    Result.AddData($"The anime [{newFavoriteAnime.AnimeID}] " +
                        $"was added successfully to the user [{newFavoriteAnime.UserID}]!",
                        HttpStatusCode.Created);
                }
                else
                {
                    Result.AddException($"The anime [{newFavoriteAnime.AnimeID}] " + 
                        $"is already in favorites for user [{newFavoriteAnime.UserID}] or doesn't exist!",
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
                FavoriteAnime favoriteAnime = new FavoriteAnime
                {
                    UserID = CurrentUser.UserID,
                    AnimeID = parameters.ID
                };

                if (FavoriteAnimeService.RemoveFavorite(favoriteAnime))
                {
                    Result.AddData($"The anime [{favoriteAnime.AnimeID}] was removed successfully " + 
                        $"from the user [{favoriteAnime.UserID}]!");
                }
                else
                {
                    Result.AddException($"The anime [{favoriteAnime.AnimeID}] is already " +
                        $"not in favorites for user {favoriteAnime.UserID}!",
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
                FavoriteAnime favoriteAnime = new FavoriteAnime
                {
                    UserID = CurrentUser.UserID,
                    AnimeID = parameters.ID,
                    Score = parameters.Score
                };

                if (FavoriteAnimeService.UpdateFavorite(favoriteAnime))
                {
                    Result.AddData($"The anime [{favoriteAnime.AnimeID}] updated successfully " + 
                        $"for the user [{favoriteAnime.UserID}]!");
                }
                else
                {
                    Result.AddException($"The anime [{favoriteAnime.AnimeID}] doesn't " +
                        $"exist in favorites for user [{favoriteAnime.UserID}]!",
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
                List<FavoriteAnime> favorites = FavoriteAnimeService.GetFavorites(CurrentUser);
                Result.AddData(favorites.Select(fa 
                    => ModelConverter.FromAnime(AnimeService.GetAnime(fa.AnimeID), fa.Score)));
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