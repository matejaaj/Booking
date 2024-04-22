using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class DriveReservationService
    {
        private readonly IDriveReservationRepository _driveReservationRepository;

        public DriveReservationService()
        {
            _driveReservationRepository = Injector.CreateInstance<IDriveReservationRepository>();
        }

        public DriveReservationService(IDriveReservationRepository driveReservationRepository)
        {
            _driveReservationRepository = driveReservationRepository;
        }

        public List<DriveReservation> GetAll()
        {
            return _driveReservationRepository.GetAll();
        }

        public DriveReservation Save(DriveReservation driveReservation)
        {
            return _driveReservationRepository.Save(driveReservation);
        }

        public void Delete(DriveReservation driveReservation)
        {
            _driveReservationRepository.Delete(driveReservation);
        }

        public DriveReservation Update(DriveReservation driveReservation)
        {
            return _driveReservationRepository.Update(driveReservation);
        }

        public List<int> FilterAvailableDrivers(List<int> drivers, DateTime? targetStartTime)
        {
            return _driveReservationRepository.FilterAvailableDrivers(drivers, targetStartTime);
        }

        public List<DriveReservation> GetByDriver(int driverId)
        {
            return _driveReservationRepository.GetByDriver(driverId);
        }

        public List<DriveReservation> GetByTourist(int touristId)
        {
            return _driveReservationRepository.GetByTourist(touristId);
        }

        public List<DriveReservation> GetByTouristAndStatus(int touristId, string status)
        {
            return _driveReservationRepository.GetByTouristAndStatus(touristId, status);
        }

        public List<DriveReservation> GetByTouristAndStatuses(int touristId, List<string> statuses)
        {
            return _driveReservationRepository.GetByTouristAndStatuses(touristId, statuses);
        }
    }

}
