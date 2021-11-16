using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;
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
            if (_userRepo.Exist(username) || string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password)) return false;

            _userRepo.Add(new User 
            { 
                Username = username, Password = password, RegisteredDate = DateTime.Now
            });

            return _userRepo.Exist(username);
        }

        public int GetIDByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return -1;

            var user = _userRepo.Get(username);

            if (user != null) return user.UserID;
            else return -1;
        }

        public User GetByID(int id)
        {
            if (id < 0) return null;

            return _userRepo.Get(id);
        }

        public User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;

            return _userRepo.Get(username);
        }

        public List<User> SearchUserByName(User user, string name, int limit)
        {
            List<User> users = new List<User>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                var friends = _friendshipService.GetFriends(user);

                users = _userRepo.GetAllThatContainsName(name.ToLower())
                    .Take(limit)
                    .ToList();

                users.ForEach(u => u.IsFriend = friends.Contains(u));
            }

            return users;
        }
    }
}