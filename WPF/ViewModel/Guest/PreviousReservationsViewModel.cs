using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class PreviousReservationsViewModel : INotifyPropertyChanged
    {
        private  AccommodationReservationService _reservationService;
        private  AccommodationService _accommodationService;
        private  ReservationModificationRequestService _modificationRequestService;
        private User _guest;
        public List<ReservationDisplayDTO> ReservationInfos { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PreviousReservationsViewModel(User guest)
        {
            _guest = guest;
            InitializeServices();

            LoadReservations();
        }

        private void InitializeServices()
        {
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _modificationRequestService = new ReservationModificationRequestService(Injector.CreateInstance<IReservationModificationRequestRepository>());
            _reservationService = new AccommodationReservationService(_modificationRequestService,_accommodationService, Injector.CreateInstance<IAccommodationReservationRepository>());
        }

        private void LoadReservations()
        {
            var reservations = _reservationService.GetByUser(_guest);
            ReservationInfos = _reservationService.FindReservationInfos(reservations);
            OnPropertyChanged(nameof(ReservationInfos));
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
            var flag = _reservationService.CancelReservation(reservationDto.Reservation);
            if (flag)
            {
                MessageBox.Show("Reservation successfully canceled.");
            }
            else
            {
                MessageBox.Show("Cannot cancel reservation. The cancellation deadline has passed.");
            }
        }
    }
}