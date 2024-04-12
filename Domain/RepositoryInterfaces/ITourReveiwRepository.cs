using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        List<TourReview> GetAll();

        TourReview GetById(int reviewId);
        TourReview Save(TourReview review);
        void Delete(TourReview review);
        TourReview Update(TourReview review);
        int NextId();
    }
}
