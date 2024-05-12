using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/notifications.csv";
        private readonly Serializer<Notification> _serializer;
        private List<Notification> _notifications;

        public NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = _serializer.FromCSV(FilePath);
        }

        public List<Notification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Notification GetById(int notificationId)
        {
            return _notifications.FirstOrDefault(n => n.Id == notificationId);
        }

        public void Save(Notification notification)
        {
            notification.Id = NextId();
            _notifications = _serializer.FromCSV(FilePath);
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public void Delete(int id)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Notification found = _notifications.Find(n => n.Id == id);
            if (found != null)
            {
                _notifications.Remove(found);
                _serializer.ToCSV(FilePath, _notifications);
            }
        }

        public int NextId()
        {
            _notifications = _serializer.FromCSV(FilePath);
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(n => n.Id) + 1;
        }

        public List<Notification> GetAllForUserId(int userId)
        {
            return _notifications.Where(n => n.TargetUserId == userId).ToList();
        }

        public Notification Update(Notification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            Notification current = _notifications.Find(n => n.Id == notification.Id);
            if (current != null)
            {
                int index = _notifications.IndexOf(current);
                _notifications[index] = notification;
                _serializer.ToCSV(FilePath, _notifications);
            }
            return notification;
        }
    }
}
