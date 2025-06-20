using Logic.DTO_s;
using Logic.Exceptions;
using Logic.Exceptions.Dal;
using Logic.Exceptions.Logic;
using Logic.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ReactorService : IReactorInterface
    {
        private readonly IReactorRepository? _reactorRepository;
        private readonly INotificationRepository? _notificationRepository;

        public ReactorService(IReactorRepository reactorRepository, INotificationRepository notificationRepository)
        {
            _reactorRepository = reactorRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task AddReactorData(ReactorHistoryDTO reactorHistoryDto)
        {
            try { 
                await _reactorRepository.AddReactorData(reactorHistoryDto);
            }
            catch (Exception ex)
            {
                throw new CouldNotConnectToDatabaseLogicException("Failed to add reactor data due to database error.", ex);
            }

            var userId = await _reactorRepository.GetUserIdByReactorIdAsync(reactorHistoryDto.ReactorId);
            if (userId != null && reactorHistoryDto.Temperature > 8100)
            {
                string title = $"Exceeded temperatures";
                string content = $"Reactor #{reactorHistoryDto.ReactorId} has exceeded safe temperatures.";

                try { 
                    await _notificationRepository.AddNotificationAsync(userId.Value, title, content);
                }
                catch (Exception ex)
                {
                    throw new Exception("A problem has occured while adding a notification to the database.", ex);
                }
            }

            if (userId != null && reactorHistoryDto.FieldStrength < 100_000_0)
            {
                string title = $"Low Field Strength";
                string content = $"Reactor #{reactorHistoryDto.ReactorId} has exceeded a safe Field Strength.";

                try
                {
                    await _notificationRepository.AddNotificationAsync(userId.Value, title, content);
                }
                catch (Exception ex)
                {
                    throw new Exception("A problem has occured while adding a notification to the database.", ex);
                }
            }

            if (userId != null && reactorHistoryDto.FuelExhaustion > 7000)
            {
                string title = $"High Fuel Exhaustion";
                string content = $"Reactor #{reactorHistoryDto.ReactorId} has almost gone through all of its fuel.";

                try
                {
                    await _notificationRepository.AddNotificationAsync(userId.Value, title, content);
                }
                catch (Exception ex)
                {
                    throw new Exception("A problem has occured while adding a notification to the database.", ex);
                }
            }
        }

        public List<ReactorStatusDTO> GetLatestReactorData(DateTime fromUtc, int reactorId)
        {
            return _reactorRepository.GetLatestReactorData(fromUtc, reactorId);
        }

        public ReactorValuesDTO GetReactorValues(int reactorId)
        {
            try
            {
                return _reactorRepository.GetReactorValues(reactorId);
            }
            catch (DataNotFoundDALException ex)
            {
                throw new ReactorNotFoundLogicException($"No reactor found with ID {reactorId}");
            }
            catch (CouldNotConnectToDatabaseDALException ex)
            {
                throw new CouldNotConnectToDatabaseLogicException("Unable to access reactor database", ex);
            }
        }

        public Task<bool> UpdateReactorValues(int id, ReactorValuesDTO reactorValuesDto)
        {
            try
            {
                _reactorRepository.UpdateReactorValues(id, reactorValuesDto);
            }
            catch (DataNotFoundDALException ex)
            {
                throw new ReactorNotFoundLogicException($"Reactor with ID {id} was not found for update.");
            }
            catch (CouldNotConnectToDatabaseDALException ex)
            {
                throw new CouldNotConnectToDatabaseLogicException("Database unreachable during reactor update.", ex);
            }
            throw new Exception("Something went very wrong");
        }

    }
}
