using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourGuestRepository
    {
        List<TourGuest> GetAll();
        List<TourGuest> GetAllByTourInstanceId(int tourInstanceId);
        List<TourGuest> GetAllByTouristId(int touristId);
        TourGuest GetById(int id);
        TourGuest Save(TourGuest tourGuest);
        void SaveMultiple(List<TourGuest> tourGuests);
        void Delete(TourGuest tourGuest);
        TourGuest Update(TourGuest tourGuest);
    }
}
