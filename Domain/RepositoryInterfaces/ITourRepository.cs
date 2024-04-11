using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetAll();

        Tour GetById(int tourId);
        Tour Save(Tour tour);
        void Delete(Tour tour);
        Tour Update(Tour tour);
    }
}
