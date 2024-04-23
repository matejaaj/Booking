using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourReservationService
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly TourGuestService _tourGuestService;
        private readonly VoucherService _voucherService;
        private readonly TourInstanceService _tourInstanceService;


        public TourReservationService()
        {
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
        }

        public TourReservationService(ITourReservationRepository tourReservation, TourGuestService tourGuest, VoucherService voucher)
        {
            _tourReservationRepository = tourReservation;
            _tourGuestService = tourGuest;
            _voucherService = voucher;
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }

        public List<TourReservation> GetAllByTourInstanceId(int tourInstanceId)
        {
            return _tourReservationRepository.GetAllByTourInstanceId(tourInstanceId);
        }

        public List<TourReservation> GetAllByUserId(int userId)
        {
            return _tourReservationRepository.GetAllByUserId(userId);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            return _tourReservationRepository.Save(tourReservation);
        }

        public void Delete(TourReservation tourReservation)
        {
            _tourReservationRepository.Delete(tourReservation);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationRepository.Update(tourReservation);
        }

        public void CreateTourReservation(List<TourGuest> tourGuests, int voucherId,  int tourInstanceId, int numberOfPeople, int touristId)
        {
            _tourGuestService.SaveMultiple(tourGuests);
            _tourInstanceService.ReserveSlots(tourInstanceId, numberOfPeople);
            if(voucherId != -1)
            {
                _voucherService.Delete(voucherId);
            }
            TourReservation reservation = new TourReservation(tourInstanceId, touristId);
            Save(reservation);
            return;
        }

    }

}
