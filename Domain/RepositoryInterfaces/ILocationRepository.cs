using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        List<Location> GetAll();

        int GetLocationIdByCity(string city);
        Location Save(Location location);
        void Delete(Location location);
        Location Update(Location location);
        List<string> GetCityByCountry(string country);
        Location GetLocationById(int locationId);
    }
}
