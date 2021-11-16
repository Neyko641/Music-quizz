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
using AutoMapper;
using MusicQuizAPI.Models.Dtos;
using System.Net;

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
        IMapper Mapper { get; set; }

        public UserController(ILogger<UserController> logger, UserService userService,
            FriendshipService friendshipService, IMapper mapper)
        {
            Logger = logger;
            UserService = userService;
            FriendshipService = friendshipService;
            Result = new ResultContext();
            Mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetUser([FromQuery] UnrequiredUserIDParamModel parameters)
        {
            if (parameters.ID == -1)
            {
                Result.AddData(Mapper.Map<UserReadDto>((User)HttpContext.Items["User"]));
            }
            else
            {
                User user = UserService.GetByID(parameters.ID);

                if (user != null) Result.AddData(Mapper.Map<UserSecuredReadDto>(user));
                else Result.AddException($"Cannot find user [{parameters.ID}]!",
                    ExceptionCode.AlreadyInOrDoesNotExistArgument, HttpStatusCode.NotFound);
            }
            

            return Result.Result();
        }


        [HttpGet("search")]
        public IActionResult Search([FromQuery] SearchUserParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<User> users = UserService.SearchUserByName(CurrentUser,
                parameters.Name, parameters.Limit);

            switch (parameters.Type)
            {
                case "simple": 
                    Result.AddData(Mapper.Map<IEnumerable<UserSimplifiedReadDto>>(users));
                    break;
                case "detailed":
                    Result.AddData(Mapper.Map<IEnumerable<UserSecuredReadDto>>(users));
                    break;
                default:
                    Result.AddException("How did you even get here ?!?", ExceptionCode.Unknown);
                    break;
            }

            return Result.Result();
        }


        [HttpPost("add-friend")]
        public IActionResult InviteFriend([FromQuery] UserIDParamModel parameters)
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
        public IActionResult AcceptFriend([FromQuery] UserIDParamModel parameters)
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
        public IActionResult DeclineFriend([FromQuery] UserIDParamModel parameters)
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
        public IActionResult RemoveFriend([FromQuery] UserIDParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (FriendshipService.RemoveFriend(CurrentUser, parameters.ID))
            {
                Result.AddData($"Successfully removed user[{parameters.ID}] for user[{CurrentUser.UserID}].");
            }
            else
            {
                Result.AddException($"Failed to remove user[{parameters.ID}] " +
                    $"for user[{CurrentUser.UserID}].", ExceptionCode.AlreadyInOrDoesNotExistArgument);
            }

            return Result.Result();
        }


        [HttpGet("get-friends")]
        public IActionResult GetFriends([FromQuery] GetUserParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<User> friends = FriendshipService.GetFriends(CurrentUser) // I'm joking, you don't have any friends
                .Take(parameters.Limit)
                .ToList();

            Result.AddData(Mapper.Map<IEnumerable<UserSecuredReadDto>>(friends));

            return Result.Result();
        }
    }
}