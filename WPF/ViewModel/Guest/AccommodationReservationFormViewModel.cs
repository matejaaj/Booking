using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

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

        public List<(DateTime, DateTime)> DateRanges { get; private set; } = new List<(DateTime, DateTime)>();

        public AccommodationReservationFormViewModel(Accommodation accommodation, User guest, AccommodationReservationService reservationService)
        {
            Accommodation = accommodation;
            Guest = guest;
            _reservationService = reservationService;
            LoadReservations();
        }

        private void LoadReservations()
        {
            AccommodationsReserved = _reservationService.GetByAccommodationId(Accommodation.AccommodationId);
        }

        public bool CheckAndGenerateDateRanges()
        {
            if (Days < Accommodation.MinReservations)
                return false;

            DateRanges.Clear();
            List<(DateTime, DateTime)> dateRanges = GenerateDateRanges(StartDate, EndDate, Days);

            if (dateRanges.Count == 0)
            {
                DateTime yearBeginning = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime yearEnding = new DateTime(DateTime.Today.Year, 12, 31);
                dateRanges = GenerateDateRanges(yearBeginning, yearEnding, Days);
            }

            foreach (var dateRange in dateRanges)
                DateRanges.Add(dateRange);

            return true;
        }

        private List<(DateTime, DateTime)> GenerateDateRanges(DateTime startDate, DateTime endDate, int days)
        {
            var dateRanges = new List<(DateTime, DateTime)>();
            DateTime currentStartDate = startDate;
            DateTime currentEndDate = startDate.AddDays(days - 1);

            while (currentEndDate <= endDate)
            {
                if (!IsReserved(currentStartDate, currentEndDate))
                    dateRanges.Add((currentStartDate, currentEndDate));

                currentStartDate = currentStartDate.AddDays(1);
                currentEndDate = currentStartDate.AddDays(days - 1);
            }

            return dateRanges;
        }

        private bool IsReserved(DateTime startDate, DateTime endDate)
        {
            return AccommodationsReserved.Any(reservation =>
                (startDate <= reservation.EndDate && endDate >= reservation.StartDate) ||
                (startDate >= reservation.StartDate && endDate <= reservation.EndDate));
        }
    }
}