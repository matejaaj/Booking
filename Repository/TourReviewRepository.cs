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
    public class TourReviewRepository : ITourReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReviews.csv";
        private readonly Serializer<TourReview> _serializer;
        private List<TourReview> _tourReviews;

        public TourReviewRepository()
        {
            _serializer = new Serializer<TourReview>();
            _tourReviews = _serializer.FromCSV(FilePath);
        }

        public List<TourReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourReview GetById(int reviewId)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            return _tourReviews.FirstOrDefault(review => review.Id == reviewId);
        }

        public TourReview Save(TourReview review)
        {
            review.Id = NextId();
            _tourReviews = _serializer.FromCSV(FilePath);
            _tourReviews.Add(review);
            _serializer.ToCSV(FilePath, _tourReviews);
            return review;
        }

        public void Delete(TourReview review)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            TourReview found = _tourReviews.Find(r => r.Id == review.Id);
            if (found != null)
            {
                _tourReviews.Remove(found);
                _serializer.ToCSV(FilePath, _tourReviews);
            }
        }

        public TourReview Update(TourReview review)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            TourReview current = _tourReviews.Find(r => r.Id == review.Id);
            if (current != null)
            {
                int index = _tourReviews.IndexOf(current);
                _tourReviews[index] = review;
                _serializer.ToCSV(FilePath, _tourReviews);
            }
            return review;
        }

        public int NextId()
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            if (_tourReviews.Count < 1)
            {
                return 1;
            }
            return _tourReviews.Max(r => r.Id) + 1;
        }
    }
}
