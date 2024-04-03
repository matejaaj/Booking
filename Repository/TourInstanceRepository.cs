using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class TourInstanceRepository
    {
        private const string FilePath = "../../../Resources/Data/tourinstance.csv";
        private readonly Serializer<TourInstance> _serializer;
        private List<TourInstance> _tourStartDates;

        public TourInstanceRepository()
        {
            _serializer = new Serializer<TourInstance>();
            _tourStartDates = _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourInstance> GetAllById(int tourId)
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

        public TourInstance Save(TourInstance tourStartDate)
        {
            tourStartDate.Id = NextId();
            _tourStartDates.Add(tourStartDate);
            _serializer.ToCSV(FilePath, _tourStartDates);
            return tourStartDate;
        }

        public int NextId()
        {
           if (_tourStartDates.Count < 1)
           {
                return 1;
           }
            return _tourStartDates.Max(tsd => tsd.Id) + 1;
        }

        public void Delete(TourInstance tourStartDate)
        {
            TourInstance founded = _tourStartDates.Find(tsd => tsd.Id == tourStartDate.Id);
            _tourStartDates.Remove(founded);
            _serializer.ToCSV(FilePath, _tourStartDates);
        }

        public TourInstance Update(TourInstance tourStartDate)
        {
            TourInstance current = _tourStartDates.Find(tsd => tsd.Id == tourStartDate.Id);
            int index = _tourStartDates.IndexOf(current);
            _tourStartDates.Remove(current);
            _tourStartDates.Insert(index, tourStartDate);
            _serializer.ToCSV(FilePath, _tourStartDates);
            return tourStartDate;
        }
    }
}
