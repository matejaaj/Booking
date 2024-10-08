﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository
{
    internal class VehicleRepository : IVehicleRepository
    {
        private const string FilePath = "../../../Resources/Data/vehicles.csv";

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
            vehicle.VehicleId = NextId();
            _vehicles = _serializer.FromCSV(FilePath);
            _vehicles.Add(vehicle);
            _serializer.ToCSV(FilePath, _vehicles);
            return vehicle;
        }
        public int NextId()
        {
            _vehicles = _serializer.FromCSV(FilePath);
            if (_vehicles.Count < 1)
            {
                return 1;
            }
            return _vehicles.Max(v => v.VehicleId) + 1;
        }

        public void Delete(Vehicle vehicle)
        {
            _vehicles = _serializer.FromCSV(FilePath);
            Vehicle founded = _vehicles.Find(v => v.VehicleId == vehicle.VehicleId);
            _vehicles.Remove(founded);
            _serializer.ToCSV(FilePath, _vehicles);

        }
        public Vehicle Update(Vehicle vehicle)
        {
            _vehicles = _serializer.FromCSV(FilePath);
            Vehicle current = _vehicles.Find(v => v.VehicleId == vehicle.VehicleId);
            int index = _vehicles.IndexOf(current);
            _vehicles.Remove(current);
            _vehicles.Insert(index, vehicle);
            _serializer.ToCSV(FilePath, _vehicles);
            return current;
        }

        public List<int> GetDriverIdsByLocationId(int locationId)
        {

            _vehicles = _serializer.FromCSV(FilePath);

            var driverIds = _vehicles
                .Where(vehicle => vehicle.LocationId.Any(id => id == locationId))
                .Select(vehicle => vehicle.DriverId)
                .Distinct() 
                .ToList();

            return driverIds;
        }




    }
}