using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class DetailedLocationService
    {
        private readonly IDetailedLocationRepository _detailedLocationRepository;

        public DetailedLocationService()
        {
            _detailedLocationRepository = Injector.CreateInstance<IDetailedLocationRepository>();
        }

        public List<DetailedLocation> GetAll()
        {
            return _detailedLocationRepository.GetAll();
        }

        public DetailedLocation Save(DetailedLocation detailedLocation)
        {
            return _detailedLocationRepository.Save(detailedLocation);
        }

        public void Delete(DetailedLocation detailedLocation)
        {
            _detailedLocationRepository.Delete(detailedLocation);
        }

        public List<string> GetAddressByCity(int cityId)
        {
            return _detailedLocationRepository.GetAddressByCity(cityId);
        }

        public DetailedLocation Update(DetailedLocation detailedLocation)
        {
            return _detailedLocationRepository.Update(detailedLocation);
        }

        public DetailedLocation GetDetailedLocationById(int id)
        {
            return _detailedLocationRepository.GetDetailedLocationById(id);
        }

        public DetailedLocation GetByAddress(string streetName)
        {
            return _detailedLocationRepository.GetByAddress(streetName);
        }
    }

}
