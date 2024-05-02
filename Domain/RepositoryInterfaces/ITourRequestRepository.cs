using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        TourRequest GetById(int reviewId);
        TourRequest Save(TourRequest review);
        void Delete(TourRequest review);
        TourRequest Update(TourRequest review);
        int NextId();
    }
}
