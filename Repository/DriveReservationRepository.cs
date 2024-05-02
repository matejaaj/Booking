using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class DriveReservationRepository : IDriveReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/drivereservation.csv";
        private readonly Serializer<DriveReservation> _serializer;
        private List<DriveReservation> _driveReservations;

        public DriveReservationRepository()
        {
            _serializer = new Serializer<DriveReservation>();
            _driveReservations = _serializer.FromCSV(FilePath);
        }

        public List<DriveReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public DriveReservation Save(DriveReservation driveReservation)
        {
            driveReservation.Id = NextId();
            _driveReservations = _serializer.FromCSV(FilePath);
            _driveReservations.Add(driveReservation);
            _serializer.ToCSV(FilePath, _driveReservations);
            return driveReservation;
        }

        public int NextId()
        {
            _driveReservations = _serializer.FromCSV(FilePath);
            if (_driveReservations.Count < 1)
            {
                return 1;
            }
            return _driveReservations.Max(r => r.Id) + 1;
        }

        public void Delete(DriveReservation driveReservation)
        {
            _driveReservations = _serializer.FromCSV(FilePath);
            DriveReservation founded = _driveReservations.Find(r => r.Id == driveReservation.Id);
            _driveReservations.Remove(founded);
            _serializer.ToCSV(FilePath, _driveReservations);
        }

        public DriveReservation Update(DriveReservation driveReservation)
        {
            _driveReservations = _serializer.FromCSV(FilePath);
            DriveReservation current = _driveReservations.Find(r => r.Id == driveReservation.Id);
            int index = _driveReservations.IndexOf(current);
            _driveReservations.Remove(current);
            _driveReservations.Insert(index, driveReservation);
            _serializer.ToCSV(FilePath, _driveReservations);
            return driveReservation;
        }

        public List<int> FilterAvailableDrivers(List<int> drivers, DateTime? targetStartTime)
        {
            var unavailableDrivers = GetAll()
                                    .Where(reservation => reservation.DepartureTime == targetStartTime && drivers.Contains(reservation.DriverId))
                                    .Select(reservation => reservation.DriverId)
                                    .Distinct()
                                    .ToList();

            return drivers.Except(unavailableDrivers).ToList();
        }

        public List<DriveReservation> GetByDriver(int driverId)
        {
            _driveReservations = _serializer.FromCSV(FilePath);
            _driveReservations.ForEach(r => r.UpdateTourist());
            return _driveReservations.FindAll(r => r.DriverId == driverId);
        }

        public List<DriveReservation> GetByTourist(int touristId)
        {
            _driveReservations = _serializer.FromCSV(FilePath);
            return _driveReservations.FindAll(r => r.TouristId == touristId);
        }

        public List<DriveReservation> GetByTouristAndStatus(int touristId, string status)
        {
            _driveReservations = _serializer.FromCSV(FilePath);
            var statusRepository = new DriveReservationStatusRepository(); 
            var statusId = statusRepository.GetAll().FirstOrDefault(s => s.Name == status)?.Id;

            return _driveReservations.Where(r => r.TouristId == touristId && r.DriveReservationStatusId == statusId).ToList();
        }

        public List<DriveReservation> GetByTouristAndStatuses(int touristId, List<string> statuses)
        {
            _driveReservations = _serializer.FromCSV(FilePath);
            var statusRepository = new DriveReservationStatusRepository();
            var statusIds = statusRepository.GetAll().Where(s => statuses.Contains(s.Name)).Select(s => s.Id).ToList();

            return _driveReservations.Where(r => r.TouristId == touristId && statusIds.Contains(r.DriveReservationStatusId)).ToList();
        }

        public DriveReservation GetById(int id)
        {
            _driveReservations = _serializer.FromCSV(FilePath);  // Reload the data
            return _driveReservations.FirstOrDefault(r => r.Id == id);
        }
    }
}
