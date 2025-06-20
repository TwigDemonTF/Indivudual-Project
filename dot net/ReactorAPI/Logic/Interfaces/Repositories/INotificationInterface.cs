﻿using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface INotificationInterface
    {
        List<NotificationDTO> GetNotifications(int userId);
        bool DeleteNotification(int notificationId);
    }
}
