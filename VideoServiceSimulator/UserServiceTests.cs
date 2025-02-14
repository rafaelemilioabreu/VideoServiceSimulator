using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoServiceSimulatorMain.Services;

namespace VideoServiceSimulatorTest
{
    public class UserServiceTests
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService();
        }

        [Fact]
        public void CreateUser_ValidData_ShouldCreateUser()
        {
            var user = _userService.CreateUser("testuser", "test@example.com");

            // Assert
            Assert.NotNull(user);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("test@example.com", user.Email);
            Assert.NotEqual(default(DateTime), user.CreatedAt);
        }

        [Theory]
        [InlineData("", "test@example.com")]
        [InlineData("testuser", "")]
        [InlineData(null, "test@example.com")]
        [InlineData("testuser", null)]
        public void CreateUser_InvalidData_ShouldThrowException(string username, string email)
        {
            Assert.Throws<ArgumentException>(() =>
                _userService.CreateUser(username, email));
        }

        [Fact]
        public void CreateUser_InvalidEmail_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                _userService.CreateUser("testuser", "invalid-email"));
        }

        [Fact]
        public void CreateUser_DuplicateEmail_ShouldThrowException()
        {
            _userService.CreateUser("testuser1", "test@example.com");

            Assert.Throws<ArgumentException>(() =>
                _userService.CreateUser("testuser2", "test@example.com"));
        }

        [Fact]
        public void CreateUser_DuplicateUsername_ShouldThrowException()
        {
            _userService.CreateUser("testuser", "test1@example.com");

            Assert.Throws<ArgumentException>(() =>
                _userService.CreateUser("testuser", "test2@example.com"));
        }

        [Fact]
        public void GetUser_ExistingUser_ShouldReturnUser()
        {
            var createdUser = _userService.CreateUser("testuser", "test@example.com");

            var user = _userService.GetUser(createdUser.Id);

            
            Assert.NotNull(user);
            Assert.Equal(createdUser.Id, user.Id);
        }

        [Fact]
        public void GetUser_NonExistingUser_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                _userService.GetUser(999));
        }

        [Fact]
        public void UpdateUser_ValidData_ShouldUpdateUser()
        {
            var user = _userService.CreateUser("testuser", "test@example.com");

            _userService.UpdateUser(user.Id, "newusername", "new@example.com");

            var updatedUser = _userService.GetUser(user.Id);
            Assert.Equal("newusername", updatedUser.Username);
            Assert.Equal("new@example.com", updatedUser.Email);
        }

        [Fact]
        public void DeleteUser_ExistingUser_ShouldRemoveUser()
        {
            var user = _userService.CreateUser("testuser", "test@example.com");

            _userService.DeleteUser(user.Id);

            Assert.Throws<ArgumentException>(() =>
                _userService.GetUser(user.Id));
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            _userService.CreateUser("user1", "test1@example.com");
            _userService.CreateUser("user2", "test2@example.com");
            _userService.CreateUser("user3", "test3@example.com");

            var users = _userService.GetAllUsers();

           
            Assert.Equal(3, users.Count);
        }

        [Fact]
        public void EmailExists_ExistingEmail_ShouldReturnTrue()
        {
            _userService.CreateUser("testuser", "test@example.com");

            var exists = _userService.EmailExists("test@example.com");

            Assert.True(exists);
        }

        [Fact]
        public void UsernameExists_ExistingUsername_ShouldReturnTrue()
        {
            _userService.CreateUser("testuser", "test@example.com");

            var exists = _userService.UsernameExists("testuser");

            Assert.True(exists);
        }
    }
}
