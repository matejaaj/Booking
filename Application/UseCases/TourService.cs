using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourService
    {
        private ITourRepository _tourRepository;

        public TourService()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public Tour Save(Tour tour)
        {
            return _tourRepository.Save(tour);
        }

        public void Delete(Tour tour)
        {
            _tourRepository.Delete(tour);
        }

        public Tour Update(Tour tour)
        {
            return _tourRepository.Update(tour);
        }

        public int NextId()
        {
            return _tourRepository.NextId();
        }
    }

}
