using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using Logic.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class BindReactorToUserFAIL
    {
        [Fact]
        public async Task BindReactorToUser_InvalidUser_ReturnsFalse()
        {
            // Arrange
            var bindDto = new BindReactorDTO
            {
                userId = -1,
                reactorId = 42
            };

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.BindReactorToUser(bindDto))
                    .ReturnsAsync(false); // Simulates failure (e.g., user not found)

            var userService = new UserService(mockRepo.Object);

            // Act
            var result = await userService.BindReactorToUser(bindDto);

            // Assert
            Assert.False(result);
            mockRepo.Verify(repo => repo.BindReactorToUser(bindDto), Times.Once);
        }
    }
}
