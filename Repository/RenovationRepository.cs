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
    public class RenovationRepository : IRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";
        private readonly Serializer<Renovation> _serializer;
        private List<Renovation> _renovations;

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = _serializer.FromCSV(FilePath);
        }

        public List<Renovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Renovation Save(Renovation renovation)
        {
            renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return renovation;
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(a => a.Id) + 1;
        }

        public void Delete(Renovation renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation founded = _renovations.Find(a => a.Id == renovation.Id);
            _renovations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public Renovation Update(Renovation renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation current = _renovations.Find(a => a.Id == renovation.Id);
            int index = _renovations.IndexOf(current);
            _renovations.Remove(current);
            _renovations.Insert(index, renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return renovation;
        }
    }
}
