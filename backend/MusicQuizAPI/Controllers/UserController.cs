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

namespace MusicQuizAPI.Controllers
{
    [Authorize]
    [EnableCors("MusicQuizPolicy")]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        UserService UserService { get; set; }
        FriendshipService FriendshipService { get; set; }
        ILogger<UserController> Logger { get; set; }
        ResultContext Result { get; set; }

        public UserController(ILogger<UserController> logger, UserService userService, 
            FriendshipService friendshipService)
        {
            Logger = logger;
            UserService = userService;
            FriendshipService = friendshipService;
            Result = new ResultContext();
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            Result.AddData(ModelConverter.FromUser(CurrentUser));
            
            return Result.Result();
        }


        [HttpGet("search")]
        public IActionResult Search([FromQuery]SearchUserParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<User> users = UserService.SearchUserByName(CurrentUser, 
                parameters.Name, parameters.Limit);

            Result.AddData(users.Select(u => ModelConverter.FromUser(u)));
            
            return Result.Result();
        }


        [HttpPost("add-friend")]
        public IActionResult InviteFriend([FromQuery]UserIDParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (FriendshipService.SendRequest(CurrentUser, parameters.ID))
            {
                Result.AddData("Successfully sended friend request from user " + 
                    $"[{CurrentUser.UserID}] to user [{parameters.ID}].");
            }
            else   
            {
                Result.AddException($"User[{CurrentUser.UserID}] already has sended invite " + 
                    $"to user [{parameters.ID}] or the user doesn't exist.", 
                    ExceptionCode.AlreadyInOrDoesNotExistArgument);
            }
            
            return Result.Result();
        }


        [HttpPut("accept-friend")]
        public IActionResult AcceptFriend([FromQuery]UserIDParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (FriendshipService.AcceptRequest(CurrentUser, parameters.ID))
            {
                Result.AddData($"Successfully accepted user[{parameters.ID}] for user[{CurrentUser.UserID}].");
            }   
            else 
            {
                Result.AddException($"Failed to accepted user[{parameters.ID}] for user[{CurrentUser.UserID}].", 
                    ExceptionCode.BadArgument);
            }
            
            return Result.Result();
        }


        [HttpPut("decline-friend")]
        public IActionResult DeclineFriend([FromQuery]UserIDParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (FriendshipService.RemoveFriend(CurrentUser, parameters.ID))
            {
                Result.AddData($"Successfully declined user[{parameters.ID}] for user[{CurrentUser.UserID}].");
            }
            else
            {
                Result.AddException($"Failed to decline user[{parameters.ID}] for " + 
                    $"user[{CurrentUser.UserID}].", ExceptionCode.BadArgument);
            }
            
            return Result.Result();
        }


        [HttpDelete("remove-friend")]
        public IActionResult RemoveFriend([FromQuery]UserIDParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (FriendshipService.RemoveFriend(CurrentUser, parameters.ID))
            {
                Result.AddData($"Successfully removed user[{parameters.ID}] for user[{CurrentUser.UserID}].");
            }
            else
            {
                Result.AddException($"Failed to remove user[{parameters.ID}] " + 
                    $"for user[{CurrentUser.UserID}].", ExceptionCode.BadArgument);
            } 
            
            return Result.Result();
        }


        [HttpGet("get-friends")]
        public IActionResult GetFriends([FromQuery]GetUserParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];
            
            Result.AddData(FriendshipService.GetFriends(CurrentUser, parameters.Limit)
                .Select(u => ModelConverter.FromUser(u))
            );
            
            return Result.Result();
        }
    }
}