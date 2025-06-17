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
    public class BindReactorToUserPASS
    {
        [Fact]
        public async Task BindReactorToUser_ValidData_ReturnsTrue()
        {
            // Arrange
            var bindDto = new BindReactorDTO
            {
                userId = 1,
                reactorId = 99
            };

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.BindReactorToUser(bindDto))
                    .ReturnsAsync(true);

            var userService = new UserService(mockRepo.Object);

            // Act
            var result = await userService.BindReactorToUser(bindDto);

            // Assert
            Assert.True(result);
            mockRepo.Verify(repo => repo.BindReactorToUser(bindDto), Times.Once);
        }
    }
}
