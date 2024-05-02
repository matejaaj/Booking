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
        TourRequest GetById(int requestId);
        TourRequest Save(TourRequest request);
        void Delete(TourRequest request);
        TourRequest Update(TourRequest request);
        int NextId();
    }
}
