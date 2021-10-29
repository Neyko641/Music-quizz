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

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/anime")]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeService _animeService;
        private readonly UserService _userService;
        private readonly FavoriteAnimeService _favoriteAnimeService;

        public AnimeController(AnimeService animeService, UserService userService,
            FavoriteAnimeService favoriteAnimeService)
        {
            _animeService = animeService;
            _userService = userService;
            _favoriteAnimeService = favoriteAnimeService;
        }

        [HttpGet("search")]
        public IActionResult Search(string title)
        {
            var result = new ResultContext();

            result.AddData(_animeService.SearchAnime(title)
                .Select(a => ModelConverter.FromAnime(a)));
            
            return Ok(result.Result());
        }

        [HttpGet("add-favorite")]
        public IActionResult AddToFavorites(int id = -1, int score = 0) 
            => PerformAction("add-favorite", id, score);


        [HttpGet("remove-favorite")]
        public IActionResult RemoveFromFavorites(int id = -1) 
            => PerformAction("remove-favorite", id);


        [HttpGet("get-favorites")]
        public IActionResult GetFavorites() 
            => PerformAction("get-favorites");


        [HttpGet("update-score")]
        public IActionResult UpdateFavoriteAnimeScore(int id = -1, int score = 0) 
            => PerformAction("update-score", id, score);



        private IActionResult PerformAction(string action, params int[] values)
        {
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            if (user != null)
            {
                ResultContext result;
                
                switch (action)
                {
                    case "add-favorite": 
                        result = _favoriteAnimeService.AddFavoriteAnime(user, values[0], values[1]); 
                        break;
                    case "remove-favorite": 
                        result = _favoriteAnimeService.RemoveFavoriteAnime(user, values[0]); 
                        break;
                    case "get-favorites": 
                        result = _favoriteAnimeService.GetFavorites(user); 
                        break;
                    case "update-score": 
                        result = _favoriteAnimeService.UpdateFavoriteAnimeScore(user, values[0], values[1]); 
                        break;
                    default: 
                        result = new ResultContext(); 
                        break;
                }

                if (result.StatusCode == 200) return Ok(result.Result());
                else return BadRequest(result.Result());
            }
            return Unauthorized();
        }
    }
}