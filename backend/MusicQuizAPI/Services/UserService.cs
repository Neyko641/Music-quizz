using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Models;

namespace MusicQuizAPI.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;

        private UserRepository _userRepo;

        public UserService(ILogger<UserService> logger, UserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
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
    }
}