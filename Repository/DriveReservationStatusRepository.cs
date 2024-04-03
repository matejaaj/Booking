using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class DriveReservationStatusRepository
    {
        private const string FilePath = "../../../Resources/Data/drivereservationstatus.csv";
        private readonly Serializer<DriveReservationStatus> _serializer;
        private List<DriveReservationStatus> _driveReservationsStatus;

        public DriveReservationStatusRepository()
        {
            _serializer = new Serializer<DriveReservationStatus>();
            _driveReservationsStatus = _serializer.FromCSV(FilePath);
        }

        public List<DriveReservationStatus> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public DriveReservationStatus Save(DriveReservationStatus driveReservation)
        {
            driveReservation.Id = NextId();
            _driveReservationsStatus = _serializer.FromCSV(FilePath);
            _driveReservationsStatus.Add(driveReservation);
            _serializer.ToCSV(FilePath, _driveReservationsStatus);
            return driveReservation;
        }

        public int NextId()
        {
            _driveReservationsStatus = _serializer.FromCSV(FilePath);
            if (_driveReservationsStatus.Count < 1)
            {
                return 1;
            }
            return _driveReservationsStatus.Max(r => r.Id) + 1;
        }

        public void Delete(DriveReservationStatus driveReservation)
        {
            _driveReservationsStatus = _serializer.FromCSV(FilePath);
            DriveReservationStatus founded = _driveReservationsStatus.Find(r => r.Id == driveReservation.Id);
            _driveReservationsStatus.Remove(founded);
            _serializer.ToCSV(FilePath, _driveReservationsStatus);
        }

        public DriveReservationStatus Update(DriveReservationStatus driveReservation)
        {
            _driveReservationsStatus = _serializer.FromCSV(FilePath);
            DriveReservationStatus current = _driveReservationsStatus.Find(r => r.Id == driveReservation.Id);
            int index = _driveReservationsStatus.IndexOf(current);
            _driveReservationsStatus.Remove(current);
            _driveReservationsStatus.Insert(index, driveReservation);
            _serializer.ToCSV(FilePath, _driveReservationsStatus);
            return driveReservation;
        }

 
    }
}
