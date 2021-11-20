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
using MusicQuizAPI.Exceptions;
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
        #region Properties
        UserService UserService { get; set; }
        FriendshipService FriendshipService { get; set; }
        ILogger<UserController> Logger { get; set; }
        ResponseContext ResponseContext { get; set; }
        IMapper Mapper { get; set; }
        #endregion




        public UserController(ILogger<UserController> logger, UserService userService,
            FriendshipService friendshipService, IMapper mapper)
        {
            Logger = logger;
            UserService = userService;
            FriendshipService = friendshipService;
            ResponseContext = new ResponseContext();
            Mapper = mapper;
        }



        #region  Methods
        [HttpGet]
        public IActionResult GetUser([FromQuery] UnrequiredUserIDParamModel parameters)
        {
            if (parameters.ID == -1)
            {
                ResponseContext.AddData(Mapper.Map<UserReadDto>((User)HttpContext.Items["User"]));
            }
            else
            {
                User user = UserService.GetByID(parameters.ID);

                if (user != null) 
                {
                    ResponseContext.AddData(Mapper.Map<UserSecuredReadDto>(user));
                }
                else throw new NotExistException($"User [{parameters.ID}] doesn't exist!");
            }

            return Ok(ResponseContext.Body);
        }


        [HttpGet("search")]
        public IActionResult Search([FromQuery] SearchUserParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<User> users = UserService.SearchUserByName(CurrentUser, parameters.Name, parameters.Limit);

            switch (parameters.Type)
            {
                case "simple": 
                    ResponseContext.AddData(Mapper.Map<IEnumerable<UserSimplifiedReadDto>>(users));
                    break;
                case "detailed":
                    ResponseContext.AddData(Mapper.Map<IEnumerable<UserSecuredReadDto>>(users));
                    break;
                default:
                    throw new UnexcpectedException("How did you even get here ?!?");
            }

            return Ok(ResponseContext.Body);
        }


        [HttpPost("friends")]
        public IActionResult SendFriendRequest([FromBody] UserIDParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FriendshipService.SendRequest(CurrentUser, parameters.ID);
            
            ResponseContext.AddData("Successfully sended friend request from user " +
                $"[{CurrentUser.UserID}] to user [{parameters.ID}].");

            return Ok(ResponseContext.Body);
        }


        [HttpPut("friends")]
        public IActionResult ManageFriendRequest([FromBody] ManageFriendRequestParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            if (parameters.IsAccepted)
            {
                FriendshipService.AcceptRequest(CurrentUser, parameters.ID);
                ResponseContext.AddData($"Successfully accepted user [{parameters.ID}] for user [{CurrentUser.UserID}].");
            }
            else
            {
                FriendshipService.DeclineRequest(CurrentUser, parameters.ID);
                ResponseContext.AddData($"Successfully declined user [{parameters.ID}] for user [{CurrentUser.UserID}].");
            }

            return Ok(ResponseContext.Body);
        }


        [HttpDelete("friends")]
        public IActionResult RemoveFriend([FromBody] UserIDParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            FriendshipService.RemoveFriend(CurrentUser, parameters.ID);

            ResponseContext.AddData($"Successfully removed user [{parameters.ID}] for user [{CurrentUser.UserID}].");
            
            return Ok(ResponseContext.Body);
        }


        [HttpGet("friends")]
        public IActionResult GetFriends([FromQuery] GetUserParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<User> friends = FriendshipService.GetFriends(CurrentUser) // I'm joking, you don't have any friends
                .Take(parameters.Limit)
                .ToList();

            ResponseContext.AddData(Mapper.Map<IEnumerable<UserSecuredReadDto>>(friends));

            return Ok(ResponseContext.Body);
        }


        [HttpGet("requests")]
        public IActionResult GetFriendRequests([FromQuery] GetUserParamModel parameters)
        {
            User CurrentUser = (User)HttpContext.Items["User"];

            List<User> friends = FriendshipService.GetFriends(CurrentUser) // I'm joking, you don't have any friends
                .Take(parameters.Limit)
                .ToList();

            ResponseContext.AddData(Mapper.Map<IEnumerable<UserSecuredReadDto>>(friends));

            return Ok(ResponseContext.Body);
        }
        #endregion
    }
}