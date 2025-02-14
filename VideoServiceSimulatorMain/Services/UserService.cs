using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoServiceSimulatorMain.Models;

namespace VideoServiceSimulatorMain.Services
{
    public class UserService
    {
        private readonly List<User> _users;
        private int _nextUserId = 1;

        public UserService()
        {
            _users = new List<User>();
        }

        public User CreateUser(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format");

            if (EmailExists(email))
                throw new ArgumentException("Email already exists");

            if (UsernameExists(username))
                throw new ArgumentException("Username already exists");

            var user = new User
            {
                Id = _nextUserId++,
                Username = username,
                Email = email,
                CreatedAt = DateTime.Now,
                Videos = new List<Video>(),
                Comments = new List<Comment>(),
                Subscribers = new List<Subscription>(),
                Subscriptions = new List<Subscription>()
            };

            _users.Add(user);
            return user;
        }

        public User GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId)
                ?? throw new ArgumentException("User not found");
        }

        public List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public void UpdateUser(int userId, string username, string email)
        {
            var user = GetUser(userId);

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format");

            if (email != user.Email && EmailExists(email))
                throw new ArgumentException("Email already exists");

            if (username != user.Username && UsernameExists(username))
                throw new ArgumentException("Username already exists");

            user.Username = username;
            user.Email = email;
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            _users.Remove(user);
        }

        public List<Video> GetUserVideos(int userId)
        {
            var user = GetUser(userId);
            return user.Videos.ToList();
        }

        public bool EmailExists(string email)
        {
            return _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool UsernameExists(string username)
        {
            return _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
