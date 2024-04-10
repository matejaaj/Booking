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
    public class GuestRatingRepository : IGuestRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/guest_ratings.csv";
        private readonly Serializer<GuestRating> _serializer;
        private List<GuestRating> _guestRating;

        public GuestRatingRepository()
        {
            _serializer = new Serializer<GuestRating>();
            _guestRating = _serializer.FromCSV(FilePath);
        }

        public List<GuestRating> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuestRating Save(GuestRating guestRating)
        {
            guestRating.GuestRatingId = NextId();
            _guestRating = _serializer.FromCSV(FilePath);
            _guestRating.Add(guestRating);
            _serializer.ToCSV(FilePath, _guestRating);
            return guestRating;
        }

        public int NextId()
        {
            _guestRating = _serializer.FromCSV(FilePath);
            if (_guestRating.Count < 1)
            {
                return 1;
            }
            return _guestRating.Max(a => a.GuestRatingId) + 1;
        }

        public void Delete(GuestRating guestRating)
        {
            _guestRating = _serializer.FromCSV(FilePath);
            GuestRating founded = _guestRating.Find(g => g.GuestRatingId == guestRating.GuestRatingId);
            _guestRating.Remove(founded);
            _serializer.ToCSV(FilePath, _guestRating);
        }

        public GuestRating Update(GuestRating guestRating)
        {
            _guestRating = _serializer.FromCSV(FilePath);
            GuestRating current = _guestRating.Find(g => g.GuestRatingId == guestRating.GuestRatingId);
            int index = _guestRating.IndexOf(current);
            _guestRating.Remove(current);
            _guestRating.Insert(index, guestRating);
            _serializer.ToCSV(FilePath, _guestRating);
            return guestRating;
        }
    }
}
