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
    public class SuperGuideRepository : ISuperGuideRepository
    {

        private const string FilePath = "../../../Resources/Data/superGuide.csv";
        private readonly Serializer<SuperGuide> _serializer;
        private List<SuperGuide> _superGuides;

        public SuperGuideRepository()
        {
            _serializer = new Serializer<SuperGuide>();
            _superGuides = _serializer.FromCSV(FilePath);
        }

        public List<SuperGuide> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public SuperGuide Save(SuperGuide superGuide)
        {
            superGuide.Id = NextId();
            _superGuides = _serializer.FromCSV(FilePath);
            _superGuides.Add(superGuide);
            _serializer.ToCSV(FilePath, _superGuides);
            return superGuide;
        }

        public int NextId()
        {
            _superGuides = _serializer.FromCSV(FilePath);
            if (_superGuides.Count < 1)
            {
                return 1;
            }
            return _superGuides.Max(g => g.Id) + 1;
        }

        public void Delete(SuperGuide superGuide)
        {
            _superGuides = _serializer.FromCSV(FilePath);
            SuperGuide founded = _superGuides.Find(g => g.Id == superGuide.Id);
            _superGuides.Remove(founded);
            _serializer.ToCSV(FilePath, _superGuides);
        }

        public SuperGuide Update(SuperGuide superGuide)
        {
            _superGuides = _serializer.FromCSV(FilePath);
            SuperGuide current = _superGuides.Find(g => g.Id == superGuide.Id);
            int index = _superGuides.IndexOf(current);
            _superGuides.Remove(current);
            _superGuides.Insert(index, superGuide);
            _serializer.ToCSV(FilePath, _superGuides);
            return superGuide;
        }

        public SuperGuide GetById(int id)
        {
            _superGuides = _serializer.FromCSV(FilePath);
            return _superGuides.Find(g => g.Id == id);
        }

    }
}
