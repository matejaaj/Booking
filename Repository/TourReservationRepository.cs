using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreservation.csv";
        private readonly Serializer<TourReservation> _serializer;
        private List<TourReservation> _tourReservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourReservation> GetAllByTourInstanceId(int tourInstanceId)
        {
            var allTourReservations = _serializer.FromCSV(FilePath);
            var filteredTourReservations = allTourReservations.Where(tr => tr.TourInstanceId == tourInstanceId).ToList();
            return filteredTourReservations;
        }

        public List<TourReservation> GetAllByUserId(int userId)
        {
            var allTourReservations = _serializer.FromCSV(FilePath);
            var filteredTourReservations = allTourReservations.Where(tr => tr.UserId == userId).ToList();
            return filteredTourReservations;
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservations.Add(tourReservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            return tourReservation;
        }

        public int NextId()
        {
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(tr => tr.Id) + 1;
        }

        public void Delete(TourReservation tourReservation)
        {
            TourReservation found = _tourReservations.Find(tr => tr.Id == tourReservation.Id);
            if (found != null)
            {
                _tourReservations.Remove(found);
                _serializer.ToCSV(FilePath, _tourReservations);
            }
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            TourReservation current = _tourReservations.Find(tr => tr.Id == tourReservation.Id);
            if (current != null)
            {
                int index = _tourReservations.IndexOf(current);
                _tourReservations[index] = tourReservation; // Update the reservation at the same position
                _serializer.ToCSV(FilePath, _tourReservations);
            }
            return tourReservation;
        }
    }
}
