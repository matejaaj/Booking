using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class AccommodationReservationFormViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Accommodation Accommodation { get; private set; }
        public User Guest { get; private set; }
        private List<AccommodationReservation> AccommodationsReserved { get; set; }

        private AccommodationReservationService _reservationService;

        public AccommodationReservationFormViewModel(Accommodation accommodation, User guest)
        {
            Accommodation = accommodation;
            Guest = guest;
            InitializeService();
            LoadReservations();
        }

        private void InitializeService()
        {
            var accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _reservationService = new AccommodationReservationService(accommodationService, Injector.CreateInstance<IAccommodationReservationRepository>());
        }

        private void LoadReservations()
        {
            AccommodationsReserved = _reservationService.GetByAccommodationId(Accommodation.AccommodationId);
        }

        public void CheckAvailability()
        {
            if (Days < Accommodation.MinReservations)
            {
                MessageBox.Show($"Number of days should be at least {Accommodation.MinReservations}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dateRanges = _reservationService.GenerateDateRanges(StartDate, EndDate, Days, AccommodationsReserved);
            if (dateRanges.Count == 0)
            {
                DateTime yearBeginning = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime yearEnding = new DateTime(DateTime.Today.Year, 12, 31);
                dateRanges = _reservationService.GenerateDateRanges(yearBeginning, yearEnding, Days, AccommodationsReserved);
            }
            DateRanges = dateRanges;

        }

        public void HandleDateRangeSelection(string selectedDateRange)
        {
            if (selectedDateRange != null)
            {
                MessageBoxResult result = MessageBox.Show($"You have selected dates: {selectedDateRange}. Would you like to proceed with the reservation?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    ReservationConfirmation reservationConfirmationWindow = new ReservationConfirmation(
                        Accommodation.AccommodationId, Guest.Id, selectedDateRange, Days, Accommodation.MaxGuests);
                    reservationConfirmationWindow.Show();
                }
            }
        }

        private int _days;
        public int Days
        {
            get => _days;
            set
            {
                if (_days != value)
                {
                    _days = value;
                    OnPropertyChanged(nameof(Days));
                }
            }
        }

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateTime _endDate = DateTime.Today;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public List<(DateTime, DateTime)> DateRanges
        {
            get => _dateRanges;
            private set
            {
                if (_dateRanges != value)
                {
                    _dateRanges = value;
                    OnPropertyChanged(nameof(DateRanges));
                }
            }
        }
        private List<(DateTime, DateTime)> _dateRanges = new List<(DateTime, DateTime)>();
    }
}
