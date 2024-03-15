using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class DetailedLocationRepository
    {
        private const string FilePath = "../../../Resources/Data/detailedlocation.csv";
        private readonly Serializer<DetailedLocation> _serializer;
        private List<DetailedLocation> _detailedLocations;

        public DetailedLocationRepository()
        {
            _serializer = new Serializer<DetailedLocation>();
            _detailedLocations = _serializer.FromCSV(FilePath);
        }

        public List<DetailedLocation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public DetailedLocation Save(DetailedLocation detailedLocation)
        {
            detailedLocation.Id = NextId();
            _detailedLocations.Add(detailedLocation);
            _serializer.ToCSV(FilePath, _detailedLocations);
            return detailedLocation;
        }

        public int NextId()
        {
            _detailedLocations = _serializer.FromCSV(FilePath);
            if (_detailedLocations.Count < 1)
            {
                return 1;
            }
            return _detailedLocations.Max(l => l.Id) + 1;
        }

        public void Delete(DetailedLocation detailedLocation)
        {
            _detailedLocations = _serializer.FromCSV(FilePath);
            DetailedLocation founded = _detailedLocations.Find(l => l.Id == detailedLocation.Id);
            _detailedLocations.Remove(founded);
            _serializer.ToCSV(FilePath, _detailedLocations);
        }

        public DetailedLocation Update(DetailedLocation detailedLocation)
        {
            _detailedLocations = _serializer.FromCSV(FilePath);
            DetailedLocation current = _detailedLocations.Find(l => l.Id == detailedLocation.Id);
            int index = _detailedLocations.IndexOf(current);
            _detailedLocations.Remove(current);
            _detailedLocations.Insert(index, detailedLocation);
            _serializer.ToCSV(FilePath, _detailedLocations);
            return detailedLocation;
        }

        public DetailedLocation GetDetailedLocationById(int id)
        {
            return _detailedLocations.FirstOrDefault(l => l.Id == id);
        }

        public DetailedLocation GetByAddress(string streetName)
        {
            _detailedLocations = _serializer.FromCSV(FilePath);
            return _detailedLocations.FirstOrDefault(loc => loc.Address.Equals(streetName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
