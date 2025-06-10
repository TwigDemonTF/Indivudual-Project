using Xunit;
using Moq;
using Logic.Services;
using Logic.DTO_s;
using Logic.Interfaces.Repositories;

namespace UnitTests
{
    public class UserServiceTests
    {
        [Fact]
        public void AuthenticateUser_WithValidCredentials_ReturnsUserDTO()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var expectedUser = new UserDTO
            {
                Id = 1,
                Email = "test@example.com",
                Password = "password123",
                minecraftUsername = "TestUser",
                reactorId = 0
            };

            // Create a mock repository
            var mockRepo = new Mock<IUserRepository>();

            // Set up the mock to return the expected user when AuthenticateUser is called
            mockRepo.Setup(repo => repo.AuthenticateUser(loginDto))
                    .Returns(expectedUser);

            // Inject the mock into the service
            var userService = new UserService(mockRepo.Object);

            // Act
            var result = userService.AuthenticateUser(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.Equal(expectedUser.Password, result.Password);
            Assert.Equal(expectedUser.minecraftUsername, result.minecraftUsername);
        }
    }
}
