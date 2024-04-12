using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IReservationModificationRequestRepository
    {
        List<ReservationModificationRequest> GetAll();
        ReservationModificationRequest Save(ReservationModificationRequest request);
        void Delete(ReservationModificationRequest request);
        ReservationModificationRequest Update(ReservationModificationRequest request);
        ReservationModificationRequest GetById(int id);
        ReservationModificationRequest GetByReservationId(int id);
    }
}
