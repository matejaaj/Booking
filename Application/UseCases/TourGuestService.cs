using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourGuestService
    {
        private readonly ITourGuestRepository _tourGuestRepository;

        public TourGuestService()
        {
            _tourGuestRepository = Injector.CreateInstance<ITourGuestRepository>();
        }

        public List<TourGuest> GetAll()
        {
            return _tourGuestRepository.GetAll();
        }

        public List<TourGuest> GetAllByTourInstanceId(int tourInstanceId)
        {
            return _tourGuestRepository.GetAllByTourInstanceId(tourInstanceId);
        }

        public List<TourGuest> GetAllByTouristId(int touristId)
        {
            return _tourGuestRepository.GetAllByTouristId(touristId);
        }

        public TourGuest Save(TourGuest tourGuest)
        {
            return _tourGuestRepository.Save(tourGuest);
        }

        public void SaveMultiple(List<TourGuest> tourGuests)
        {
            _tourGuestRepository.SaveMultiple(tourGuests);
        }

        public void Delete(TourGuest tourGuest)
        {
            _tourGuestRepository.Delete(tourGuest);
        }

        public TourGuest Update(TourGuest tourGuest)
        {
            return _tourGuestRepository.Update(tourGuest);
        }
    }

}
