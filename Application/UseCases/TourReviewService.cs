using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourReviewService
    {
        private ITourReviewRepository _tourReviewRepository;

        public TourReviewService()
        {
            _tourReviewRepository = Injector.CreateInstance<ITourReviewRepository>();
        }

        public List<TourReview> GetAll()
        {
            return _tourReviewRepository.GetAll();
        }

        public TourReview Save(TourReview review)
        {
            return _tourReviewRepository.Save(review);
        }

        public void Delete(TourReview review)
        {
            _tourReviewRepository.Delete(review);
        }

        public TourReview Update(TourReview review)
        {
            return _tourReviewRepository.Update(review);
        }

        public int NextId()
        {
            return _tourReviewRepository.NextId();
        }

        public TourReview GetById(int id)
        {
            return _tourReviewRepository.GetById(id);
        }

        public bool HasUserReviewedTour(int userId, int tourInstanceId)
        {
            return _tourReviewRepository.HasUserReviewedTour(userId, tourInstanceId);
        }

    }
}
