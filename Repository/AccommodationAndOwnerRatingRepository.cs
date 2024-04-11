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
    public class AccommodationAndOwnerRatingRepository : IAccommodationAndOwnerRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationsandownersrating.csv";
        private readonly Serializer<AccommodationAndOwnerRating> _serializer;
        private List<AccommodationAndOwnerRating> _accommodationAndOwnerRating;

        public AccommodationAndOwnerRatingRepository()
        {
            _serializer = new Serializer<AccommodationAndOwnerRating>();
            _accommodationAndOwnerRating = _serializer.FromCSV(FilePath);
        }

        public List<AccommodationAndOwnerRating> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationAndOwnerRating Save(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
            accommodationAndOwnerRating.RatingId = NextId();
            _accommodationAndOwnerRating = _serializer.FromCSV(FilePath);
            _accommodationAndOwnerRating.Add(accommodationAndOwnerRating);
            _serializer.ToCSV(FilePath, _accommodationAndOwnerRating);
            return accommodationAndOwnerRating;
        }

        public int NextId()
        {
            _accommodationAndOwnerRating = _serializer.FromCSV(FilePath);
            if (_accommodationAndOwnerRating.Count < 1)
            {
                return 1;
            }
            return _accommodationAndOwnerRating.Max(a => a.RatingId) + 1;
        }

        public void Delete(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
            _accommodationAndOwnerRating = _serializer.FromCSV(FilePath);
            AccommodationAndOwnerRating founded = _accommodationAndOwnerRating.Find(g => g.RatingId == accommodationAndOwnerRating.RatingId);
            _accommodationAndOwnerRating.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationAndOwnerRating);
        }

        public AccommodationAndOwnerRating Update(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
            _accommodationAndOwnerRating = _serializer.FromCSV(FilePath);
            AccommodationAndOwnerRating current = _accommodationAndOwnerRating.Find(g => g.RatingId == accommodationAndOwnerRating.RatingId);
            int index = _accommodationAndOwnerRating.IndexOf(current);
            _accommodationAndOwnerRating.Remove(current);
            _accommodationAndOwnerRating.Insert(index, accommodationAndOwnerRating);
            _serializer.ToCSV(FilePath, _accommodationAndOwnerRating);
            return accommodationAndOwnerRating;
        }

        public AccommodationAndOwnerRating GetById(int id)
        {
            _accommodationAndOwnerRating = _serializer.FromCSV(FilePath);
            return _accommodationAndOwnerRating.Find(a => a.RatingId == id);
        }

        public AccommodationAndOwnerRating GetByReservationId(int id)
        {
            _accommodationAndOwnerRating = _serializer.FromCSV(FilePath);
            return _accommodationAndOwnerRating.Find(a => a.AccommodationReservationId == id);
        }
    }
}
