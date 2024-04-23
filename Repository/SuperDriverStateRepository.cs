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
    internal class SuperDriverStateRepository : ISuperDriverStateRepository
    {
        private const string FilePath = "../../../Resources/Data/superDriverState.csv";
        private readonly Serializer<SuperDriverState> _serializer;
        private List<SuperDriverState> _superDriverStates;

        public SuperDriverStateRepository()
        {
            _serializer = new Serializer<SuperDriverState>();
            _superDriverStates = _serializer.FromCSV(FilePath);
        }

        public void Delete(SuperDriverState superDriverState)
        {
            _superDriverStates = _serializer.FromCSV(FilePath);
            _superDriverStates.Remove(superDriverState);
            _serializer.ToCSV(FilePath, _superDriverStates);
        }

        public List<SuperDriverState> GetAll()
        {
            _superDriverStates = _serializer.FromCSV(FilePath);
            return _superDriverStates;
        }

        public SuperDriverState? GetSuperDriverStateById(int driverId)
        {
            _superDriverStates = _serializer.FromCSV(FilePath);
            return _superDriverStates.FirstOrDefault(sds => sds.DriverID == driverId);
        }

        public SuperDriverState Save(SuperDriverState superDriverState)
        {
            _superDriverStates = _serializer.FromCSV(FilePath);
            _superDriverStates.Add(superDriverState);
            _serializer.ToCSV(FilePath, _superDriverStates);
            return superDriverState;
        }

        public SuperDriverState Update(SuperDriverState superDriverState)
        {
            _superDriverStates = _serializer.FromCSV(FilePath);
            SuperDriverState s = _superDriverStates.Find(s => s.DriverID == superDriverState.DriverID);
            int index = _superDriverStates.IndexOf(s);
            _superDriverStates.Remove(s);
            _superDriverStates.Insert(index, superDriverState);
            _serializer.ToCSV(FilePath, _superDriverStates);
            return superDriverState;
        }
    }
}
