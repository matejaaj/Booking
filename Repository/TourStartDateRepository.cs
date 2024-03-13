using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    internal class TourStartDateRepository
    {
        private const string FilePath = "../../../Resources/Data/tourStartDate.csv";
        private readonly Serializer<TourStartDate> _serializer;
        private List<TourStartDate> _tourStartDates;

        public TourStartDateRepository()
        {
            _serializer = new Serializer<TourStartDate>();
            _tourStartDates = _serializer.FromCSV(FilePath);
        }

        public List<TourStartDate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourStartDate> GetAllById(int tourId)
        {
            var allTourStartDates = _serializer.FromCSV(FilePath);
            var filteredTourStartDates = allTourStartDates.Where(tsd => tsd.TourId == tourId).ToList();
            return filteredTourStartDates;
        }


        public TourStartDate Save(TourStartDate tourStartDate)
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

        public void Delete(TourStartDate tourStartDate)
        {
            TourStartDate founded = _tourStartDates.Find(tsd => tsd.Id == tourStartDate.Id);
            _tourStartDates.Remove(founded);
            _serializer.ToCSV(FilePath, _tourStartDates);
        }

        public TourStartDate Update(TourStartDate tourStartDate)
        {
            TourStartDate current = _tourStartDates.Find(tsd => tsd.Id == tourStartDate.Id);
            int index = _tourStartDates.IndexOf(current);
            _tourStartDates.Remove(current);
            _tourStartDates.Insert(index, tourStartDate);
            _serializer.ToCSV(FilePath, _tourStartDates);
            return tourStartDate;
        }
    }
}
