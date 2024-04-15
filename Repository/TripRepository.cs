using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class TripRepository
    {
        private const string FilePath = "../../../Resources/Data/trips.csv";
        private readonly Serializer<Trip> _serializer;
        private List<Trip> _trips;

        public TripRepository()
        {
            _serializer = new Serializer<Trip>();
            _trips = _serializer.FromCSV(FilePath);
        }

        public List<Trip> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Trip Save(Trip trip)
        {
            trip.Id = NextId();
            _trips = _serializer.FromCSV(FilePath);
            _trips.Add(trip);
            _serializer.ToCSV(FilePath, _trips);
            return trip;
        }

        public int NextId()
        {
            _trips = _serializer.FromCSV(FilePath);
            if (_trips.Count < 1)
            {
                return 1;
            }
            return _trips.Max(t => t.Id) + 1;
        }

        public void Delete(Trip trip)
        {
            _trips = _serializer.FromCSV(FilePath);
            Trip found = _trips.Find(t => t.Id == trip.Id);
            if (found != null)
            {
                _trips.Remove(found);
                _serializer.ToCSV(FilePath, _trips);
            }
        }

        public Trip Update(Trip trip)
        {
            _trips = _serializer.FromCSV(FilePath);
            Trip current = _trips.Find(t => t.Id == trip.Id);
            if (current != null)
            {
                int index = _trips.IndexOf(current);
                _trips[index] = trip;
                _serializer.ToCSV(FilePath, _trips);
            }
            return trip;
        }
    }
}
