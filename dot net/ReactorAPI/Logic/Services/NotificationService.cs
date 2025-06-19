using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class NotificationService : INotificationInterface
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public bool DeleteNotification(int notificationId)
        {
            return _notificationRepository.DeleteNotification(notificationId);
        }

        public List<NotificationDTO> GetNotifications(int userId)
        {
            return _notificationRepository.GetNotifications(userId);
        }
    }
}
