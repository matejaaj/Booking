using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        List<AccommodationReservation> GetAll();
        AccommodationReservation Save(AccommodationReservation accommodationReservation);
        void Delete(AccommodationReservation accommodationReservation);
        AccommodationReservation Update(AccommodationReservation accommodationReservation);
        List<AccommodationReservation> GetByUser(User user);
        List<AccommodationReservation> GetByAccommodationIds(List<int> accommodationIds);
        List<AccommodationReservation> GetByAccommodationId(int accommodationId);
    }
}
