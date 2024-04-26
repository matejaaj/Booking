using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Factories
{
    public class TourReservationFactory
    {
        private readonly TourGuestService _tourGuestService;
        private readonly TourInstanceService _tourInstanceService;
        private readonly VoucherService _voucherService;
        private readonly TourReservationService _tourReservationService;

        public TourReservationFactory(TourGuestService tourGuestService, TourInstanceService tourInstanceService, VoucherService voucherService, TourReservationService tourReservationService)
        {
            _tourGuestService = tourGuestService;
            _tourInstanceService = tourInstanceService;
            _voucherService = voucherService;
            _tourReservationService = tourReservationService;
        }

        public void CreateTourReservation(List<TourGuest> tourGuests, int voucherId, int tourInstanceId, int numberOfPeople, int touristId)
        {
            _tourGuestService.SaveMultiple(tourGuests); 
            _tourInstanceService.ReserveSlots(tourInstanceId, numberOfPeople);  

            if (voucherId != -1)
            {
                _voucherService.Delete(voucherId);  
            }

            TourReservation reservation = new TourReservation(tourInstanceId, touristId);
            _tourReservationService.Save(reservation); 
        }
    }
}
