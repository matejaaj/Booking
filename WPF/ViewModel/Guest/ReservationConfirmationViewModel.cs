using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class ReservationConfirmationViewModel : INotifyPropertyChanged
    {
        private int _guestNumber;
        private readonly int _accommodationId;
        private readonly int _guestId;
        private readonly string _selectedDateRange;
        private readonly int _days;
        private readonly int _maxCapacity;
        private readonly AccommodationReservationService _accommodationReservationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ReservationConfirmationViewModel(int accommodationId, int guestId, string selectedDateRange, int days, int maxCapacity, AccommodationReservationService accommodationReservationService)
        {
            _accommodationId = accommodationId;
            _guestId = guestId;
            _selectedDateRange = selectedDateRange;
            _days = days;
            _maxCapacity = maxCapacity;
            _accommodationReservationService = accommodationReservationService;
        }

        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                if (_guestNumber != value)
                {
                    _guestNumber = value;
                    OnPropertyChanged(nameof(GuestNumber));
                }
            }
        }

        public void ConfirmReservation()
        {
            if (GuestNumber > _maxCapacity || GuestNumber < 1)
            {
                MessageBox.Show($"Number of people cannot exceed {_maxCapacity}. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Reservation successfully confirmed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            DateTime startDate, endDate;
            (startDate, endDate) = ConvertStringToDates(_selectedDateRange);

            AccommodationReservation reservation = new AccommodationReservation(startDate, endDate, _days, GuestNumber, _accommodationId, _guestId, false, false, false);
            _accommodationReservationService.Save(reservation);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private (DateTime, DateTime) ConvertStringToDates(string selectedDateRange)
        {
            string[] dateRangeParts = selectedDateRange.Split(" - ");
            DateTime selectedStartDate = DateTime.ParseExact(dateRangeParts[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime selectedEndDate = DateTime.ParseExact(dateRangeParts[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return (selectedStartDate, selectedEndDate);
        }
    }
}