using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
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
        private AccommodationReservationService _accommodationReservationService;
        private SuperGuestService _superGuestService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ReservationConfirmationViewModel(int accommodationId, int guestId, string selectedDateRange, int days, int maxCapacity)
        {
            _accommodationId = accommodationId;
            _guestId = guestId;
            _selectedDateRange = selectedDateRange;
            _days = days;
            _maxCapacity = maxCapacity;
            InitializeServices();
        }

        private void InitializeServices()
        {
            var accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _accommodationReservationService = new AccommodationReservationService(accommodationService,Injector.CreateInstance<IAccommodationReservationRepository>());
            _superGuestService = new SuperGuestService(Injector.CreateInstance<ISuperGuestRepository>(), _accommodationReservationService);
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
            _superGuestService.ConfirmReservation(_guestNumber, _accommodationId, _guestId, _selectedDateRange, _days, _maxCapacity);
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}