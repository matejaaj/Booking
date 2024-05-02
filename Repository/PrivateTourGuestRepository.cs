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
    public class PrivateTourGuestRepository : IPrivateTourGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/privateTourGuest.csv";
        private readonly Serializer<PrivateTourGuest> _serializer;
        private List<PrivateTourGuest> _privateTourGuests;

        public PrivateTourGuestRepository()
        {
            _serializer = new Serializer<PrivateTourGuest>();
            _privateTourGuests = _serializer.FromCSV(FilePath);
        }

        public List<PrivateTourGuest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<PrivateTourGuest> GetAllByTouristId(int touristId)
        {
            var allPrivateTourGuests = _serializer.FromCSV(FilePath);
            var filteredPrivateTourGuests = allPrivateTourGuests.Where(tg => tg.TouristId == touristId).ToList();
            return filteredPrivateTourGuests;
        }

        public PrivateTourGuest Save(PrivateTourGuest privateTourGuest)
        {
            privateTourGuest.Id = NextId();
            _privateTourGuests.Add(privateTourGuest);
            _serializer.ToCSV(FilePath, _privateTourGuests);
            return privateTourGuest;
        }

        public void SaveMultiple(List<PrivateTourGuest> privateTourGuests)
        {
            foreach (var guest in privateTourGuests)
            {
                guest.Id = NextId();
                _privateTourGuests.Add(guest);
            }

            _serializer.ToCSV(FilePath, _privateTourGuests);
        }

        public int NextId()
        {
            if (_privateTourGuests.Count < 1)
            {
                return 1;
            }
            return _privateTourGuests.Max(tg => tg.Id) + 1;
        }

        public void Delete(PrivateTourGuest privateTourGuest)
        {
            PrivateTourGuest found = _privateTourGuests.Find(tg => tg.Id == privateTourGuest.Id);
            _privateTourGuests.Remove(found);
            _serializer.ToCSV(FilePath, _privateTourGuests);
        }

        public PrivateTourGuest Update(PrivateTourGuest privateTourGuest)
        {
            PrivateTourGuest current = _privateTourGuests.Find(tg => tg.Id == privateTourGuest.Id);
            int index = _privateTourGuests.IndexOf(current);
            _privateTourGuests[index] = privateTourGuest;
            _serializer.ToCSV(FilePath, _privateTourGuests);
            return privateTourGuest;
        }

        public PrivateTourGuest GetById(int id)
        {
            return _privateTourGuests.FirstOrDefault(guest => guest.Id == id);
        }
    }
}
