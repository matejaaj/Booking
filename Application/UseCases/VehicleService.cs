using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService()
        {
            _vehicleRepository = Injector.CreateInstance<IVehicleRepository>();
        }

        public VehicleService(IVehicleRepository vehicle)
        {
            _vehicleRepository = vehicle;
        }

        public List<Vehicle> GetAll()
        {
            return _vehicleRepository.GetAll();
        }

        public Vehicle Save(Vehicle vehicle)
        {
            return _vehicleRepository.Save(vehicle);
        }

        public void Delete(Vehicle vehicle)
        {
            _vehicleRepository.Delete(vehicle);
        }

        public Vehicle Update(Vehicle vehicle)
        {
            return _vehicleRepository.Update(vehicle);
        }

        public List<int> GetDriverIdsByLocationId(int locationId)
        {
            return _vehicleRepository.GetDriverIdsByLocationId(locationId);
        }
    }

}
