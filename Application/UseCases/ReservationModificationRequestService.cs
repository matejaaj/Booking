using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;

namespace BookingApp.Application.UseCases
{
    public class ReservationModificationRequestService
    {
        private readonly IReservationModificationRequestRepository _requestRepository;

        public ReservationModificationRequestService(IReservationModificationRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public List<ReservationModificationRequest> GetAll()
        {
            return _requestRepository.GetAll();
        }

        public ReservationModificationRequest Save(ReservationModificationRequest request)
        {
            return _requestRepository.Save(request);
        }

        public void Delete(ReservationModificationRequest request)
        {
            _requestRepository.Delete(request);
        }

        public ReservationModificationRequest Update(ReservationModificationRequest request)
        {
            return _requestRepository.Update(request);
        }

        public ReservationModificationRequest GetById(int id)
        {
            return _requestRepository.GetById(id);
        }

        public ReservationModificationRequest GetByReservationId(int id)
        {
            return _requestRepository.GetByReservationId(id);
        }

        public List<ReservationModificationRequest> GetAllWithReservationId(int id)
        {
            return _requestRepository.GetAllWithReservationId(id);
        }

        public List<ReservationModificationRequest> GetAllAcceptedWithReservationId(int id)
        {
            var requests = _requestRepository.GetAllWithReservationId(id);
            return requests.FindAll(r => r.Status == ReservationModificationRequest.RequestStatus.APPROVED);
        }

        public List<ReservationModificationRequest> GetByReservationIds(List<int> reservationIds)
        {
            return _requestRepository.GetByReservationIds(reservationIds);
        }

        internal List<ReservationModificationRequest> GetByReservationIds(List<AccommodationReservation> accommodationReservations)
        {
            return _requestRepository.GetByReservationIds(accommodationReservations.Select(a => a.Id).ToList());
        }

        public bool SendRequest(int reservationId, DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate == DateTime.MinValue || newEndDate == DateTime.MinValue)
                return false;

            if (newStartDate >= newEndDate)
                return false;

            var existingRequest = _requestRepository.GetByReservationId(reservationId);

            if (existingRequest != null)
            {
                existingRequest.NewStartDate = newStartDate;
                existingRequest.NewEndDate = newEndDate;
                existingRequest.Status = ReservationModificationRequest.RequestStatus.PENDING;
                existingRequest.OwnerComment = "";
                Update(existingRequest);
                return true;
            }
            else
            {
                var request = new ReservationModificationRequest(
                    reservationId, DateTime.MinValue, DateTime.MinValue, // Start and end date of existing reservation
                    newStartDate, newEndDate, ReservationModificationRequest.RequestStatus.PENDING, "");

                Save(request);
                return true;
            }
        }
    }
}
