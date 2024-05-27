using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class LocationStateRepository : ILocationStateRepository
    {
        private const string FilePath = "../../../Resources/Data/locationstate.csv";
        private readonly Serializer<LocationState> _serializer;
        private List<LocationState> _locations;

        public LocationStateRepository()
        {
            _serializer = new Serializer<LocationState>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public void Delete(int id)
        {
            _locations = _serializer.FromCSV(FilePath);
            LocationState founded = _locations.Find(l => l.LocationId == id);
            _locations.Remove(founded);
            _serializer.ToCSV(FilePath, _locations);
        }

        public LocationState Get(int id)
        {
            _locations = _serializer.FromCSV(FilePath).ToList();
            return _locations.FirstOrDefault(l => l.LocationId == id);
        }

        public List<LocationState> GetAll()
        {
            return _serializer.FromCSV(FilePath).ToList();
        }

        public LocationState Save(LocationState state)
        {
            _locations = _serializer.FromCSV(FilePath).ToList();
            _locations.Add(state);
            _serializer.ToCSV(FilePath, _locations);
            return state;
        }

        public LocationState Update(LocationState state)
        {
            _locations = _serializer.FromCSV(FilePath);
            LocationState current = _locations.Find(l => l.LocationId == state.LocationId);
            int index = _locations.IndexOf(current);
            _locations.Remove(current);
            _locations.Insert(index, state);
            _serializer.ToCSV(FilePath, _locations);
            return state;
        }
    }
}
