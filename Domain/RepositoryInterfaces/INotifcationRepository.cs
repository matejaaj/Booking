using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        List<Notification> GetAll();
        Notification GetById(int notificationId);
        void Save(Notification notification);
        void Delete(int id);
        List<Notification> GetAllForUserId(int userId);
    }
}
