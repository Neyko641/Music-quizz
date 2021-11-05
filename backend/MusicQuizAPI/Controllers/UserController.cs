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

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly FriendshipService _friendshipService;
        private readonly ILogger<SongController> _logger;

        public UserController(ILogger<SongController> logger, UserService userService, 
            FriendshipService friendshipService)
        {
            _logger = logger;
            _userService = userService;
            _friendshipService = friendshipService;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var result = new ResultContext();
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            result.AddData(ModelConverter.FromUser(user));
            
            return Ok(result.Result());
        }

        [HttpGet("search")]
        public IActionResult Search(string name, int limit = 10)
        {
            var result = new ResultContext();
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            result.AddData(_userService.SearchUserByName(user, name, limit)
                .Select(u => ModelConverter.FromUser(u)));
            
            return Ok(result.Result());
        }

        [HttpPost("add-friend")]
        public IActionResult InviteFriend(int id)
        {
            var result = new ResultContext();
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            if (_friendshipService.Send(user, id))
                result.AddData($"Successfully sended friend request to user[{id}] from user[{user.UserID}].");
            else result.AddExceptionMessage($"User[{user.UserID}] already has sended invite to user[{id}]" +
                $" or the user doesn't exist.");
            
            return Ok(result.Result());
        }

        [HttpPut("accept-friend")]
        public IActionResult AcceptFriend(int id)
        {
            var result = new ResultContext();
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            if (_friendshipService.Accept(user, id))
                result.AddData($"Successfully accepted user[{id}] for user[{user.UserID}].");
            else result.AddExceptionMessage($"Failed to accepted user[{id}] for user[{user.UserID}].");
            
            return Ok(result.Result());
        }

        [HttpPut("decline-friend")]
        public IActionResult DeclineFriend(int id)
        {
            var result = new ResultContext();
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            if (_friendshipService.Remove(user, id))
                result.AddData($"Successfully declined user[{id}] for user[{user.UserID}].");
            else result.AddExceptionMessage($"Failed to decline user[{id}] for user[{user.UserID}].");
            
            return Ok(result.Result());
        }

        [HttpDelete("remove-friend")]
        public IActionResult RemoveFriend(int id)
        {
            var result = new ResultContext();
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            if (_friendshipService.Remove(user, id))
                result.AddData($"Successfully removed user[{id}] for user[{user.UserID}].");
            else result.AddExceptionMessage($"Failed to remove user[{id}] for user[{user.UserID}].");
            
            return Ok(result.Result());
        }

        [HttpGet("get-friends")]
        public IActionResult GetFriends(int limit = 10)
        {
            var result = new ResultContext();
            var user = ClientHelper.GetUserFromHttpContext(HttpContext, _userService);

            result.AddData(_friendshipService.GetFriends(user, limit)
                .Select(u => ModelConverter.FromUser(u))
            );
            
            return Ok(result.Result());
        }
    }
}