using Xunit;
using Moq;
using Logic.Services;
using Logic.Interfaces.Repositories;
using System;
using Logic.DTO_s;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void AuthenticateUser_WithValidCredentials_ReturnsUserDTO()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var loginDto = new LoginDTO
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var expectedUser = new UserDTO
            {
                Id = 1,
                Email = loginDto.Email,
                minecraftUsername = "TestUser",
                Password = loginDto.Password,
                reactorId = 0
            };

            mockRepo.Setup(r => r.AuthenticateUser(loginDto)).Returns(expectedUser);
            var userService = new UserService(mockRepo.Object);

            // Act
            var result = userService.AuthenticateUser(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.Equal(expectedUser.minecraftUsername, result.minecraftUsername);
        }
    }
}
