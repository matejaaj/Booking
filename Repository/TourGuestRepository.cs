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
    public class TourGuestRepository : ITourGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourguest.csv";
        private readonly Serializer<TourGuest> _serializer;
        private List<TourGuest> _tourGuests;

        public TourGuestRepository()
        {
            _serializer = new Serializer<TourGuest>();
            _tourGuests = _serializer.FromCSV(FilePath);
        }

        public List<TourGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourGuest> GetAllByTourInstanceId(int tourInstanceId)
        {
            var allTourGuests = _serializer.FromCSV(FilePath);
            var filteredTourGuests = allTourGuests.Where(tg => tg.TourReservationId == tourInstanceId).ToList();
            return filteredTourGuests;
        }

        public List<TourGuest> GetAllByTouristId(int touristId)
        {
            var allTourGuests = _serializer.FromCSV(FilePath);
            var filteredTourGuests = allTourGuests.Where(tg => tg.TouristId == touristId).ToList();
            return filteredTourGuests;
        }

        public TourGuest Save(TourGuest tourGuest)
        {
            tourGuest.Id = NextId();
            _tourGuests.Add(tourGuest);
            _serializer.ToCSV(FilePath, _tourGuests);
            return tourGuest;
        }

        public void SaveMultiple(List<TourGuest> tourGuests)
        {
            foreach (var tourGuest in tourGuests)
            {
                tourGuest.Id = NextId();
                _tourGuests.Add(tourGuest);
            }

            _serializer.ToCSV(FilePath, _tourGuests);
        }


        public int NextId()
        {
            if (_tourGuests.Count < 1)
            {
                return 1;
            }
            return _tourGuests.Max(tg => tg.Id) + 1;
        }

        public void Delete(TourGuest tourGuest)
        {
            TourGuest found = _tourGuests.Find(tg => tg.Id == tourGuest.Id);
            _tourGuests.Remove(found);
            _serializer.ToCSV(FilePath, _tourGuests);
        }

        public TourGuest Update(TourGuest tourGuest)
        {
            TourGuest current = _tourGuests.Find(tg => tg.Id == tourGuest.Id);
            int index = _tourGuests.IndexOf(current);
            _tourGuests.Remove(current);
            _tourGuests.Insert(index, tourGuest);
            _serializer.ToCSV(FilePath, _tourGuests);
            return tourGuest;
        }

        public TourGuest GetById(int id)
        {
            return _tourGuests.FirstOrDefault(guest => guest.Id == id);
        }
    }
}
