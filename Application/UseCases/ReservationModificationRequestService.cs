using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;

namespace BookingApp.Application.UseCases
{
    public class ReservationModificationRequestService
    {
        private readonly IReservationModificationRequestRepository _requestRepository;

        public ReservationModificationRequestService()
        {
            _requestRepository = Injector.CreateInstance<IReservationModificationRequestRepository>();
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

        public List<ReservationModificationRequest> GetByReservationIds(List<int> reservationIds)
        {
            return _requestRepository.GetByReservationIds(reservationIds);
        }
    }
}
