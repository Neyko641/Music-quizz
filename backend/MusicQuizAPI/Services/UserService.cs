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

        private MusicQuizRepository _repo;

        public UserService(ILogger<UserService> logger, MusicQuizRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public bool RegisterUser(string username, string password)
        {
            if (_repo.ExistUser(username) || string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password)) return false;

            _repo.AddUser(new User 
            { 
                Username = username, Password = password, RegisteredDate = DateTime.Now
            });

            return _repo.ExistUser(username);
        }

        public int GetIDByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return -1;

            var user = _repo.GetUserByUsername(username);

            if (user != null) return user.UserID;
            else return -1;
        }

        public User GetByID(int id)
        {
            if (id < 0) return null;

            return _repo.GetUserByID(id);
        }

        public User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;

            return _repo.GetUserByUsername(username);
        }
    }
}