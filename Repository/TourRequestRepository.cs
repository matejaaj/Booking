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
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequests.csv";
        private readonly Serializer<TourRequest> _serializer;
        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public List<TourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests = _serializer.FromCSV(FilePath);
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

        public int NextId()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(tr => tr.Id) + 1;
        }

        public void Delete(TourRequest tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest found = _tourRequests.Find(tr => tr.Id == tourRequest.Id);
            if (found != null)
            {
                _tourRequests.Remove(found);
                _serializer.ToCSV(FilePath, _tourRequests);
            }
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest current = _tourRequests.Find(tr => tr.Id == tourRequest.Id);
            if (current != null)
            {
                int index = _tourRequests.IndexOf(current);
                _tourRequests[index] = tourRequest;
                _serializer.ToCSV(FilePath, _tourRequests);
            }
            return tourRequest;
        }

        public TourRequest GetById(int tourRequestId)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.FirstOrDefault(tr => tr.Id == tourRequestId);
        }
    }
}
