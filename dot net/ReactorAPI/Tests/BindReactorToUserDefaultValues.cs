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
    public class BindReactorToUserDefaultValues
    {
        [Fact]
        public async Task BindReactorToUser_DefaultValues_ReturnsFalse()
        {
            // Arrange
            var bindDto = new BindReactorDTO
            {
                userId = 0,
                reactorId = 0
            };

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.BindReactorToUser(bindDto))
                    .ReturnsAsync(false);

            var userService = new UserService(mockRepo.Object);

            // Act
            var result = await userService.BindReactorToUser(bindDto);

            // Assert
            Assert.False(result);
        }
    }
}
