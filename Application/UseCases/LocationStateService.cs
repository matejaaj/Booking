using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class LocationStateService
    {
        private readonly ILocationStateRepository _locationRepository;
        public LocationStateService() {
            _locationRepository = Injector.CreateInstance<ILocationStateRepository>();
        }
        public LocationStateService(ILocationStateRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }


        public List<LocationState> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public LocationState Save(LocationState location)
        {
            return _locationRepository.Save(location);
        }

        public void Delete(LocationState location)
        {
            _locationRepository.Delete(location.LocationId);
        }

        public LocationState Update(LocationState location)
        {
            return _locationRepository.Update(location);
        }
    }
}
