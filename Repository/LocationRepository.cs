using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";
        private readonly Serializer<Location> _serializer;
        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations = _serializer.FromCSV(FilePath);
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }

        public int NextId()
        {
            _locations = _serializer.FromCSV(FilePath);
            if (_locations.Count < 1)
            {
                return 1;
            }
            return _locations.Max(l => l.Id) + 1;
        }

        public void Delete(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location founded = _locations.Find(l => l.Id == location.Id);
            _locations.Remove(founded);
            _serializer.ToCSV(FilePath, _locations);
        }

        public Location Update(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location current = _locations.Find(l => l.Id == location.Id);
            int index = _locations.IndexOf(current);
            _locations.Remove(current);
            _locations.Insert(index, location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }

      public List<KeyValuePair<int,string>> GetCityByCountry(string country)
        {
            var cities = GetAll()
                .Where(location => location.Country == country)
                .Select(location => new KeyValuePair<int, string>(location.Id, location.City))
                .Distinct()
                .OrderBy(pair => pair.Value)
                .ToList();

            return cities;
        }

        public Location GetLocationById(int locationId)
        {
            return _locations.FirstOrDefault(l => l.Id == locationId);
        }
    }
}
