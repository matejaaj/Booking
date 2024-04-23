using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public int GetLocationIdByCity(string city)
        {
            return _locationRepository.GetLocationIdByCity(city);
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location Save(Location location)
        {
            return _locationRepository.Save(location);
        }

        public void Delete(Location location)
        {
            _locationRepository.Delete(location);
        }

        public Location Update(Location location)
        {
            return _locationRepository.Update(location);
        }

        public List<KeyValuePair<int, string>> GetCitiesByCountry(string country)
        {
            return _locationRepository.GetCitiesByCountry(country);
        }

        public Location GetLocationById(int locationId)
        {
            return _locationRepository.GetLocationById(locationId);
        }

        public List<KeyValuePair<int, string>> GetAllCountries()
        {
            var locations = _locationRepository.GetAll();
            var countries = locations
                .GroupBy(loc => loc.Country)
                .Select(grp => grp.First())
                .Select(loc => new KeyValuePair<int, string>(loc.Id, loc.Country))
                .OrderBy(c => c.Value)
                .ToList();
            return countries;
        }
    }
}
