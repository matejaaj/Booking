using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.WPF.View.Guide;
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

        public void ReportReview(int id)
        {
            TourReview newReview = GetById(id);
            newReview.IsValid = false;
            Update(newReview);
        }

        public List<TourReviewDTO> GetAllByTourInstanceId(int instanceId)
        {
            List<TourReviewDTO> reviews = new List<TourReviewDTO>();
            foreach (var review in GetAll())
            {
                if (review.TourInstanceId == instanceId)
                    reviews.Add(new TourReviewDTO(review));
            }
            return reviews;
        }
    }
}
