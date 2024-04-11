using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class TourInstanceRepository : ITourInstanceRepository
    {
        private const string FilePath = "../../../Resources/Data/tourinstance.csv";
        private readonly Serializer<TourInstance> _serializer;
        private List<TourInstance> _tourInstances;

        public TourInstanceRepository()
        {
            _serializer = new Serializer<TourInstance>();
            _tourInstances = _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAllByTourId(int tourId)
        {
            var allTourStartDates = _serializer.FromCSV(FilePath);
            var filteredTourStartDates = allTourStartDates.Where(tsd => tsd.TourId == tourId).ToList();
            return filteredTourStartDates;
        }


        public TourInstance GetByDateAndId(int tourId, DateTime date)
        {
            var allTourInstances = _serializer.FromCSV(FilePath);
            var matchingTourInstance = allTourInstances
                .FirstOrDefault(tourInstance => tourInstance.TourId == tourId && tourInstance.StartTime == date);
            return matchingTourInstance;
        }

        public TourInstance Save(TourInstance tourInstance)
        {
            tourInstance.Id = NextId();
            _tourInstances.Add(tourInstance);
            _serializer.ToCSV(FilePath, _tourInstances);
            return tourInstance;
        }

        public int NextId()
        {
           if (_tourInstances.Count < 1)
           {
                return 1;
           }
            return _tourInstances.Max(tsd => tsd.Id) + 1;
        }

        public void Delete(TourInstance tourStartDate)
        {
            TourInstance founded = _tourInstances.Find(tsd => tsd.Id == tourStartDate.Id);
            _tourInstances.Remove(founded);
            _serializer.ToCSV(FilePath, _tourInstances);
        }

        public TourInstance Update(TourInstance tourStartDate)
        {
            TourInstance current = _tourInstances.Find(tsd => tsd.Id == tourStartDate.Id);
            int index = _tourInstances.IndexOf(current);
            _tourInstances.Remove(current);
            _tourInstances.Insert(index, tourStartDate);
            _serializer.ToCSV(FilePath, _tourInstances);
            return tourStartDate;
        }

        public TourInstance GetById(int id)
        {
            return _tourInstances.FirstOrDefault(tourInstance => tourInstance.Id == id);
        }

    }
}
