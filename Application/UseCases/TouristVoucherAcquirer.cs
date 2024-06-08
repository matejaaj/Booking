using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.Model;
using BookingApp.Domain.Model.BookingApp.Domain.Model;

namespace BookingApp.Application.UseCases
{
    public class TouristVoucherAcquirer
    {
        private readonly TourReservationService _reservationService;
        private readonly VoucherService _voucherService;
        private readonly TourInstanceService _tourInstanceService;

        public TouristVoucherAcquirer(TourReservationService reservationService, VoucherService voucherService, TourInstanceService tourInstanceService)
        {
            _reservationService = reservationService;
            _voucherService = voucherService;
            _tourInstanceService = tourInstanceService;
        }

        public void AcquireVouchersForUser(List<TourReservation> reservations)
        {
            int toursCount = 0;
            List<TourReservation> reservationsToUpdate = new List<TourReservation>();

            foreach (var reservation in reservations)
            {
                if (reservation.VoucherAcquired) continue;

                var instance = _tourInstanceService.GetById(reservation.TourInstanceId);
                if (HasOneYearPassedSince(instance)) continue;

                reservationsToUpdate.Add(reservation);
                toursCount++;

                if (toursCount == 5)
                { 
                    CreateAndSaveVoucher(reservations.First().UserId, instance.StartTime);
                    UpdateReservations(reservationsToUpdate);
                    break;
                }
            }
        }

        private bool HasOneYearPassedSince(TourInstance instance)
        {
            return (DateTime.Now - instance.StartTime).TotalDays > 365;
        }

        private void CreateAndSaveVoucher(int userId, DateTime latestTourDate)
        {
            Voucher voucher = new Voucher(userId, latestTourDate.AddMonths(6));
            _voucherService.Save(voucher);
        }

        private void UpdateReservations(List<TourReservation> reservationsToUpdate)
        {
            foreach (var reservation in reservationsToUpdate)
            {
                reservation.VoucherAcquired = true;
                _reservationService.Update(reservation);
            }
        }

    }
}