using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourInstanceRepository
    {
        List<TourInstance> GetAll();
        List<TourInstance> GetAllByTourId(int tourId);

        TourInstance GetById(int id);
        TourInstance GetByDateAndId(int tourId, DateTime date);
        TourInstance Save(TourInstance tourStartDate);
        void Delete(TourInstance tourStartDate);
        TourInstance Update(TourInstance tourStartDate);
    }
}
