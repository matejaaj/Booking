using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        List<TourReservation> GetAll();
        List<TourReservation> GetAllByTourInstanceId(int tourInstanceId);
        List<TourReservation> GetAllByUserId(int userId);
        TourReservation Save(TourReservation tourReservation);
        void Delete(TourReservation tourReservation);
        TourReservation Update(TourReservation tourReservation);
    }
}
