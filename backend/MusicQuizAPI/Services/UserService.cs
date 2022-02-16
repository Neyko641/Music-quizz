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

        public bool RegisterUser(string email, string password, string username)
        {
            if (_userRepo.ExistWithEmail(email))
            {
                throw new AlreadyExistException($"Email '{email}' is already taken!");
            } 

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)
                || string.IsNullOrWhiteSpace(email))
            {
                throw new BadArgumentException("Username, Password and Email cannot be empty!");
            }

            _userRepo.Add(new User 
            { 
                Username = username, 
                Password = password, 
                Email = email,
                RegisteredDate = DateTime.Now
            });

            return _userRepo.ExistWithEmail(email);
        }

        public int GetIDByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new BadArgumentException("Email cannot be empty!");

            var user = _userRepo.GetByEmail(email);

            if (user != null) return user.UserID;
            else return -1;
        }

        public User GetByID(int id)
        {
            if (id < 0) throw new BadArgumentException("Index cannot be less than 0!");

            return _userRepo.Get(id);
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new BadArgumentException("Email cannot be empty!");

            return _userRepo.GetByEmail(email);
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

        public void UpdateUser(User user)
        {
            if (_userRepo.Update(user) == 0)
            {
                throw new UnexcpectedException("Cannot update the user!");
            }
        }
    }
}