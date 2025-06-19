using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(int userId, string title, string content);
        List<NotificationDTO> GetNotifications(int userId);
        bool DeleteNotification(int notificationId);
    }
}
