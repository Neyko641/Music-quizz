﻿using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using MusicQuizAPI.Helpers;
using MusicQuizAPI.Models.Dtos;
using MusicQuizAPI.Models.Parameters;
using MusicQuizAPI.Models.Database;
using AutoMapper;


namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/anime")]
    public class AnimeController : ControllerBase
    {
        #region Properties
        AnimeService AnimeService { get; set; }
        UserService UserService { get; set; }
        FavoriteAnimeService FavoriteAnimeService { get; set; }
        ResponseContext ResponseContext { get; set; }
        IMapper Mapper { get; set; }
        #endregion



        public AnimeController(AnimeService animeService, UserService userService,
            FavoriteAnimeService favoriteAnimeService, IMapper mapper)
        {
            AnimeService = animeService;
            UserService = userService;
            FavoriteAnimeService = favoriteAnimeService;
            ResponseContext = new ResponseContext();
            Mapper = mapper;
        }



        #region Methods
        [HttpGet]
        public IActionResult Search([FromQuery] SearchAnimeParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<Anime> foundedAnimes = AnimeService.SearchAnime(parameters.Title);

            ResponseContext.AddData(foundedAnimes.Select(a => 
            {
                var anime = Mapper.Map<AnimeReadDto>(a);
                anime.UserScore = FavoriteAnimeService.GetFavoriteScore(CurrentUser.UserID, a.AnimeID);
                return anime;

            }));
            
            return Ok(ResponseContext.Body);
        }


        [HttpPost("favorites")]
        public IActionResult AddFavoriteAnime([FromBody] AddFavoriteParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FavoriteAnime newFavoriteAnime = new FavoriteAnime
            {
                UserID = CurrentUser.UserID,
                AnimeID = parameters.ID,
                Score = parameters.Score
            };

            FavoriteAnimeService.AddFavorite(newFavoriteAnime);

            ResponseContext.AddData($"The anime [{newFavoriteAnime.AnimeID}] " +
                $"was added successfully to the user [{newFavoriteAnime.UserID}]!", HttpStatusCode.Created);

            return Created("", ResponseContext.Body);
        }


        [HttpDelete("favorites")]
        public IActionResult RemoveFavoriteAnime([FromBody] RemoveFavoriteParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FavoriteAnime favoriteAnime = new FavoriteAnime
            {
                UserID = CurrentUser.UserID,
                AnimeID = parameters.ID
            };

            FavoriteAnimeService.RemoveFavorite(favoriteAnime);
            
            ResponseContext.AddData($"The anime [{favoriteAnime.AnimeID}] was removed successfully " +
                $"from the user [{favoriteAnime.UserID}]!");

            return Ok(ResponseContext.Body);
        }


        [HttpPatch("favorites")]
        public IActionResult UpdateFavoriteAnime([FromBody] UpdateFavoriteParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FavoriteAnime favoriteAnime = new FavoriteAnime
            {
                UserID = CurrentUser.UserID,
                AnimeID = parameters.ID,
                Score = parameters.Score
            };

            FavoriteAnimeService.UpdateFavorite(favoriteAnime);

            ResponseContext.AddData($"The anime [{favoriteAnime.AnimeID}] updated successfully " +
                $"for the user [{favoriteAnime.UserID}]!");

            return Ok(ResponseContext.Body);
        }


        [HttpGet("favorites")]
        public IActionResult GetFavoriteAnime()
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<FavoriteAnime> favorites = FavoriteAnimeService.GetFavorites(CurrentUser.UserID);

            ResponseContext.AddData(favorites.Select(fa => 
            {
                var anime = Mapper.Map<AnimeReadDto>(AnimeService.GetAnime(fa.AnimeID));
                anime.UserScore = fa.Score;
                return anime;
            }));

            return Ok(ResponseContext.Body);
        }
        #endregion
    }
}