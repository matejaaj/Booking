using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class PreviousReservationsViewModel : INotifyPropertyChanged
    {
        private readonly AccommodationReservationService _reservationService;
        private readonly AccommodationService _accommodationService;
        private readonly ReservationModificationRequestService _modificationRequestService;
        private User _guest;
        public List<ReservationDisplayDTO> ReservationInfos { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PreviousReservationsViewModel(User guest, AccommodationReservationService reservationService, AccommodationService accommodationService, ReservationModificationRequestService modificationRequestService)
        {
            _guest = guest;
            _reservationService = reservationService;
            _accommodationService = accommodationService;
            _modificationRequestService = modificationRequestService;

            LoadReservations();
        }

        private void LoadReservations()
        {
            var reservations = _reservationService.GetByUser(_guest);
            ReservationInfos = FindReservationInfos(reservations);
            OnPropertyChanged(nameof(ReservationInfos));
        }

        private List<ReservationDisplayDTO> FindReservationInfos(List<AccommodationReservation> reservations)
        {
            List<ReservationDisplayDTO> reservationInfos = new List<ReservationDisplayDTO>();
            foreach (var reservation in reservations)
            {
                if (!reservation.IsCancelled)
                {
                    var accommodation = _accommodationService.GetById(reservation.AccommodationId);
                    var accommodationName = accommodation != null ? accommodation.Name : "Unknown";

                    var modificationRequest = _modificationRequestService.GetByReservationId(reservation.Id);

                    var reservationDto = new ReservationDisplayDTO(
                        accommodationName, reservation.StartDate, reservation.EndDate,
                        reservation, modificationRequest?.OwnerComment ?? "",
                        modificationRequest?.Status.ToString() ?? "");

                    reservationInfos.Add(reservationDto);
                }
            }
            return reservationInfos;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Rate(ReservationDisplayDTO reservationDto)
        {
            if ((DateTime.Now - reservationDto.Reservation.EndDate).TotalDays > 5)
            {
                MessageBox.Show("Reservation cannot be rated because more than 5 days have passed since the end of the stay.");
                return;
            }
            else if(reservationDto.Reservation.EndDate > DateTime.Now)
            {
                MessageBox.Show("Reservation cannot be rated because reservation did not end.");
            }
            else if (reservationDto.Reservation.IsAccommodationAndOwnerRated)
            {
                MessageBox.Show("You have already rated the accommodation!");
                return;
            }
            else
            {
                RateForm rateForm = new RateForm(reservationDto.Reservation);
                rateForm.Show();
            }
        }

        public void RequestModification(ReservationDisplayDTO reservationDto)
        {
            if (reservationDto.StartDate < DateTime.Now)
            {
                MessageBox.Show("Reservation has already started.");
            }
            else
            {
                RequestModification requestModification = new RequestModification(reservationDto.Reservation);
                requestModification.Show();
            }
        }

        public void CancelReservation(ReservationDisplayDTO reservationDto)
        {
            Accommodation accommodation = _accommodationService.GetById(reservationDto.Reservation.AccommodationId);
            int cancellationThreshold = accommodation.CancelThershold;
            int daysUntilReservationStart = (int)(reservationDto.Reservation.StartDate - DateTime.Now).TotalDays;
            if ((daysUntilReservationStart > 1 || daysUntilReservationStart > cancellationThreshold) && reservationDto.Reservation.IsCancelled == false)
            {
                reservationDto.Reservation.IsCancelled = true;
                _reservationService.Update(reservationDto.Reservation);
                MessageBox.Show("Reservation successfully canceled.");
            }
            else
            {
                MessageBox.Show("Cannot cancel reservation. The cancellation deadline has passed.");
            }
        }
    }
}