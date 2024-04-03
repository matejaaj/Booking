using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IDriveReservationRepository
    {
        List<DriveReservation> GetAll();
        DriveReservation Save(DriveReservation driveReservation);
        void Delete(DriveReservation driveReservation);
        DriveReservation Update(DriveReservation driveReservation);
        List<int> FilterAvailableDrivers(List<int> drivers, DateTime? targetStartTime);
        List<DriveReservation> GetByDriver(int driverId);
        List<DriveReservation> GetByTourist(int touristId);
    }
}
