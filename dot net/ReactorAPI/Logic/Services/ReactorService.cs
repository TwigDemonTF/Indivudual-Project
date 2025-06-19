using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await _reactorRepository.AddReactorData(reactorHistoryDto);

            var userId = await _reactorRepository.GetUserIdByReactorIdAsync(reactorHistoryDto.ReactorId);
            if (userId != null)
            {
                string title = $"Exceeded temperatures";
                string content = $"Reactor #{reactorHistoryDto.ReactorId} has exceeded safe temperatures.";
                await _notificationRepository.AddNotificationAsync(userId.Value, title, content);
            }
        }

        public List<ReactorStatusDTO> GetLatestReactorData(DateTime fromUtc, int reactorId)
        {
            return _reactorRepository.GetLatestReactorData(fromUtc, reactorId);
        }

        public ReactorDTO GetReactor(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
