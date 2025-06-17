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
    public class AddReactorData
    {
        [Fact]
        public async Task AddReactorData_CallsRepositoryMethod()
        {
            // Arrange
            var mockRepo = new Mock<IReactorRepository>();
            var service = new ReactorService(mockRepo.Object);

            var testDto = new ReactorHistoryDTO
            {
                ReactorId = 1,
                EnergySaturation = 5000,
                Temperature = 7000,
                FieldStrength = 4000,
                FuelExhaustion = 0.3f
            };

            // Act
            await service.AddReactorData(testDto);

            // Assert
            mockRepo.Verify(r => r.AddReactorData(It.Is<ReactorHistoryDTO>(
                dto => dto.ReactorId == testDto.ReactorId &&
                       dto.EnergySaturation == testDto.EnergySaturation &&
                       dto.Temperature == testDto.Temperature &&
                       dto.FieldStrength == testDto.FieldStrength &&
                       dto.FuelExhaustion == testDto.FuelExhaustion
            )), Times.Once);
        }
    }
}
