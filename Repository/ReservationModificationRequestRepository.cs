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
    public class ReservationModificationRequestRepository : IReservationModificationRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationModificationRequests.csv";
        private readonly Serializer<ReservationModificationRequest> _serializer;
        private List<ReservationModificationRequest> _requests;

        public ReservationModificationRequestRepository()
        {
            _serializer = new Serializer<ReservationModificationRequest>();
            _requests = _serializer.FromCSV(FilePath);
        }

        public List<ReservationModificationRequest> GetAll()
        {
            return _requests;
        }

        public ReservationModificationRequest Save(ReservationModificationRequest request)
        {
            request.Id = NextId();
            _requests.Add(request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }

        public void Delete(ReservationModificationRequest request)
        {
            _requests.Remove(request);
            _serializer.ToCSV(FilePath, _requests);
        }

        public ReservationModificationRequest Update(ReservationModificationRequest request)
        {
            ReservationModificationRequest current = _requests.Find(r => r.Id == request.Id);
            int index = _requests.IndexOf(current);
            _requests.Remove(current);
            _requests.Insert(index, request);
            _serializer.ToCSV(FilePath, _requests);
            return request;
        }

        public ReservationModificationRequest GetById(int id)
        {
            return _requests.Find(r => r.Id == id);
        }

        public int NextId()
        {
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(r => r.Id) + 1;
        }

        public ReservationModificationRequest GetByReservationId(int id)
        {
            return _requests.FirstOrDefault(r => r.ReservationId == id);
        }

        public List<ReservationModificationRequest> GetByReservationIds(List<int> reservationIds)
        {
            return _requests.FindAll(r => reservationIds.Contains(r.ReservationId));
        }

        public List<ReservationModificationRequest> GetAllWithReservationId(int id)
        {
            return _requests.FindAll(r => r.ReservationId == id);
        }
    }
}
