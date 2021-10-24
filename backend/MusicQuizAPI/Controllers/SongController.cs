﻿﻿using Microsoft.AspNetCore.Mvc;
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

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/song")]
    public class SongController : ControllerBase
    {
        private readonly SongService _songService;
        private readonly UserService _userService;
        private readonly FavoriteSongService _favoriteSongService;
        private readonly ILogger<SongController> _logger;

        public SongController(ILogger<SongController> logger, SongService songService, 
            UserService userService, FavoriteSongService favoriteSongService)
        {
            _logger = logger;
            _songService = songService;
            _userService = userService;
            _favoriteSongService = favoriteSongService;
        }

        [HttpGet("random")]
        public IActionResult Random([FromQuery]RandomSongParamModel parameters) 
        {
            var result = new ResultContext();

            result.AddData(_songService.GetRandomSongs(parameters.Count, parameters.Difficulty)
                .Select(s => ModelConverter.FromSong(s)));
            
            return Ok(result.Result());
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery]SearchSongParamModel parameters)
        {
            var result = new ResultContext();

            result.AddData(_songService.SearchSong(parameters.Value, parameters.SearchType)
                .Select(s => ModelConverter.FromSong(s)));
            
            return Ok(result.Result());
        }

        [HttpGet("add-favorite")]
        public IActionResult AddToFavorites(int id = -1) => PerformAction("add-favorite", id);


        [HttpGet("remove-favorite")]
        public IActionResult RemoveFromFavorites(int id = -1) => PerformAction("remove-favorite", id);


        [HttpGet("get-favorites")]
        public IActionResult GetFavorites() => PerformAction("get-favorites");


        
        private IActionResult PerformAction(string action, int value = -1)
        {
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            if (user != null)
            {
                ResultContext result;
                
                switch (action)
                {
                    case "add-favorite": result = _favoriteSongService.AddFavoriteSong(user, value); break;
                    case "remove-favorite": result = _favoriteSongService.RemoveFavoriteSong(user, value); break;
                    case "get-favorites": result = _favoriteSongService.GetFavorites(user); break;
                    default: result = new ResultContext(); break;
                }

                if (result.StatusCode == 200) return Ok(result.Result());
                else return BadRequest(result.Result());
            }
            return Unauthorized();
        }
    }
}