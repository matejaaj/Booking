using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetAll();
        Vehicle Save(Vehicle vehicle);
        void Delete(Vehicle vehicle);
        Vehicle Update(Vehicle vehicle);
        List<int> GetDriverIdsByLocationId(int locationId);
    }
}
