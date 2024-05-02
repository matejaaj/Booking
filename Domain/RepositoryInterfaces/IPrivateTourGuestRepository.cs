using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IPrivateTourGuestRepository
    {
        List<PrivateTourGuest> GetAll();
        List<PrivateTourGuest> GetAllByTouristId(int touristId);
        PrivateTourGuest GetById(int id);
        PrivateTourGuest Save(PrivateTourGuest tourGuest);
        void SaveMultiple(List<PrivateTourGuest> tourGuests);
        void Delete(PrivateTourGuest tourGuest);
        PrivateTourGuest Update(PrivateTourGuest tourGuest);
    }
}
