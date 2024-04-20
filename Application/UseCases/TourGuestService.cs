using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public List<TourGuest> GetAllByTouristForTourInstance(int touristId, int tourInstanceId)
        { 
            var guestsForTourInstance = GetAllByTourInstanceId(tourInstanceId);


            var guestsForTouristInTourInstance = guestsForTourInstance.Where(guest => guest.TouristId == touristId).ToList();

            return guestsForTouristInTourInstance;
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

        public TourGuest GetById(int id)
        {
            return _tourGuestRepository.GetById(id);
        }

        public ObservableCollection<TourGuest> InitializeTourGuests(int tourInstanceId)
        {
            var guests = new ObservableCollection<TourGuest>();
            foreach (var tourGuest in GetAll())
            {
                if (tourInstanceId == tourGuest.TourReservationId)
                {
                    guests.Add(tourGuest);
                }
            }
            return guests;
        }
    }

}
