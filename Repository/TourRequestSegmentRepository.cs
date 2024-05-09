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
    public class TourRequestSegmentRepository : ITourRequestSegmentRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequestSegment.csv";
        private readonly Serializer<TourRequestSegment> _serializer;
        private List<TourRequestSegment> _tourRequests;

        public TourRequestSegmentRepository()
        {
            _serializer = new Serializer<TourRequestSegment>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public List<TourRequestSegment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequestSegment Save(TourRequestSegment tourRequest)
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

        public void Delete(TourRequestSegment tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequestSegment found = _tourRequests.Find(tr => tr.Id == tourRequest.Id);
            if (found != null)
            {
                _tourRequests.Remove(found);
                _serializer.ToCSV(FilePath, _tourRequests);
            }
        }

        public TourRequestSegment Update(TourRequestSegment tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequestSegment current = _tourRequests.Find(tr => tr.Id == tourRequest.Id);
            if (current != null)
            {
                int index = _tourRequests.IndexOf(current);
                _tourRequests[index] = tourRequest;
                _serializer.ToCSV(FilePath, _tourRequests);
            }
            return tourRequest;
        }

        public TourRequestSegment GetById(int tourRequestId)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.FirstOrDefault(tr => tr.Id == tourRequestId);
        }
    }
}
