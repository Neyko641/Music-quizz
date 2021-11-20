using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;
using MusicQuizAPI.Exceptions;
using System.Linq;

namespace MusicQuizAPI.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserRepository _userRepo;
        private readonly FriendshipService _friendshipService;

        public UserService(ILogger<UserService> logger, UserRepository userRepo,
            FriendshipService friendshipService)
        {
            _logger = logger;
            _userRepo = userRepo;
            _friendshipService= friendshipService;
        }

        public bool RegisterUser(string username, string password)
        {
            if (_userRepo.Exist(username))
            {
                throw new AlreadyExistException($"Username '{username}' is already taken!");
            } 

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new BadArgumentException("Username and password cannot be empty!");
            }

            _userRepo.Add(new User 
            { 
                Username = username, Password = password, RegisteredDate = DateTime.Now
            });

            return _userRepo.Exist(username);
        }

        public int GetIDByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new BadArgumentException("Username cannot be empty!");

            var user = _userRepo.Get(username);

            if (user != null) return user.UserID;
            else return -1;
        }

        public User GetByID(int id)
        {
            if (id < 0) throw new BadArgumentException("Index cannot be less than 0!");

            return _userRepo.Get(id);
        }

        public User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new BadArgumentException("Username cannot be empty!");

            return _userRepo.Get(username);
        }

        public List<User> SearchUserByName(User user, string name, int limit)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new BadArgumentException("Username cannot be empty!");

            List<User> users = _userRepo.GetAllThatContainsName(name.ToLower())
                .Take(limit)
                .ToList();

            List<User> friends = _friendshipService.GetFriends(user);

            users.ForEach(u => u.IsFriend = friends.Contains(u));

            return users;
        }
    }
}