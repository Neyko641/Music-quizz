using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Database;
using MusicQuizAPI.Models.Database;
using MusicQuizAPI.Exceptions;
using System.Linq;

namespace MusicQuizAPI.Services
{
    public class FriendshipService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserRepository _userRepo;
        private readonly FriendshipRepository _friendshipRepository;

        public FriendshipService(ILogger<UserService> logger, UserRepository userRepo,
            FriendshipRepository friendshipRepository)
        {
            _logger = logger;
            _userRepo = userRepo;
            _friendshipRepository = friendshipRepository;
        }

        public void SendRequest(User user, int id)
        {
            User potentialFriend = _userRepo.Get(id);

            if (potentialFriend == null) 
            {
                throw new NotExistException($"User [{id}] doesn't exist!");
            }
            
            if (_friendshipRepository.Exist(user.UserID, potentialFriend.UserID))
            {
                throw new AlreadyExistException($"Friend request from User [{user.UserID}] " + 
                    $"to user [{id}] is already sended!");
            }

            _friendshipRepository.Add(new Friendship
            {
                RequestedUserID = user.UserID,
                AcceptedUserID = potentialFriend.UserID,
                StartDate = DateTime.Now
            });
        }

        public void AcceptRequest(User user, int id)
        {
            Friendship fs = _friendshipRepository.Get(id, user.UserID);

            if (fs == null) 
            {
                throw new NotExistException($"No request is sent from user [{id}] to user [{user.UserID}]!");
            }

            if (fs.IsAccepted)
            {
                throw new AlreadyExistException($"User [{user.UserID}] and [{id}] are already friends!");
            }
            
            fs.IsAccepted = true;
            _friendshipRepository.Update(fs);
        }

        public void DeclineRequest(User user, int id)
        {
            Friendship fs = _friendshipRepository.Get(id, user.UserID);

            if (fs == null) 
            {
                throw new NotExistException($"No request is sent from user [{id}] to user [{user.UserID}]!");
            }

            if (fs.IsAccepted)
            {
                throw new AlreadyExistException($"User [{user.UserID}] and [{id}] are already friends!");
            }
            
            _friendshipRepository.Remove(fs);
        }

        public void RemoveFriend(User user, int id)
        {
            Friendship fs = _friendshipRepository.Get(user.UserID, id);

            if (fs == null)
            {
                throw new NotExistException($"User [{id}] is already not friend with user [{user.UserID}]!");
            }

            _friendshipRepository.Remove(fs);
        }

        public List<User> GetFriends(User user)
        {
            int id = user.UserID;

            return _friendshipRepository.GetAll()
                .Where(f => (f.RequestedUserID == id || f.AcceptedUserID == id) && f.IsAccepted)
                .Select(f => 
                {
                    User u;
                    if (f.AcceptedUserID == id) u = _userRepo.Get(f.RequestedUserID);
                    else u = _userRepo.Get(f.AcceptedUserID);
                    u.IsFriend = true;
                    return u;
                })
                .ToList();
        }

        public List<Friendship> GetAllRelationships(User user)
            => _friendshipRepository
                .GetAll(user.UserID)
                .Where(f => !f.IsAccepted && f.AcceptedUserID == user.UserID)
                .ToList();
    }
}