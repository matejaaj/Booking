using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IDetailedLocationRepository
    {
        List<DetailedLocation> GetAll();
        DetailedLocation Save(DetailedLocation detailedLocation);
        void Delete(DetailedLocation detailedLocation);
        List<string> GetAddressByCity(int cityId);
        DetailedLocation Update(DetailedLocation detailedLocation);
        DetailedLocation GetDetailedLocationById(int id);
        DetailedLocation GetByAddress(string streetName);
    }
}
