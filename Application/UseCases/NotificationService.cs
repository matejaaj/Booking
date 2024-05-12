using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public List<Notification> GetAllNotifications()
        {
            return _notificationRepository.GetAll();
        }

        public void Save(Notification notification)
        {
            _notificationRepository.Save(notification);
        }

        public void RemoveNotification(int id)
        {
            _notificationRepository.Delete(id);
        }

        public List<Notification> GetNotificationsForUser(int userId)
        {
            return _notificationRepository.GetAllForUserId(userId);
        }
    }
}
