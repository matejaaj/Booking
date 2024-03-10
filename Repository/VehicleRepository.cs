using BookingApp.Serializer;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class VehicleRepository
    {
        private const string FilePath = "../../../Resources/Data/vehicle.csv";

        private readonly Serializer<Vehicle> _serializer;
       
        private List<Vehicle> _vehicles;

        public VehicleRepository()
        {
            _serializer = new Serializer<Vehicle>();
            _vehicles = _serializer.FromCSV(FilePath);
        }
        
        public List<Vehicle> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Vehicle Save(Vehicle vehicle)
        {
            vehicle.Id = NextId();
            _vehicles = _serializer.FromCSV(FilePath);
            _vehicles.Add(vehicle);
            _serializer.ToCSV(FilePath, _vehicles);
            return vehicle;
        }
        public int NextId()
        {
            _vehicles = _serializer.FromCSV(FilePath);
            if(_vehicles.Count < 1) 
            {
                return 1;
            }
            return _vehicles.Max(v => v.Id)+1;
        }

        public void Delete(Vehicle vehicle) 
        {
            _vehicles = _serializer.FromCSV(FilePath);
            Vehicle founded = _vehicles.Find(v => v.Id ==  vehicle.Id);
            _vehicles.Remove(founded);
            _serializer.ToCSV(FilePath, _vehicles);
        
        }
        public Vehicle Update(Vehicle vehicle)
        {
            _vehicles = _serializer.FromCSV(FilePath);
            Vehicle current = _vehicles.Find(v =>v.Id == vehicle.Id);
            int index = _vehicles.IndexOf(current);
            _vehicles.Remove(current);
            _vehicles.Insert(index, vehicle);
            _serializer.ToCSV(FilePath, _vehicles);
            return current;
        }

    }
}
