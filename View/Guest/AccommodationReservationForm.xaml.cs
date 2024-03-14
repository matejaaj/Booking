using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for AccommodationReservationForm.xaml
    /// </summary>
    public partial class AccommodationReservationForm : Window, INotifyPropertyChanged
    {
        private int _days;

        public int Days
        {
            get { return _days; }
            set
            {
                if (_days != value)
                {
                    _days = value;
                    OnPropertyChanged("Days");
                }
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public User guest;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static List<AccommodationReservation> AccommodationsReserved { get; set; }
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private Accommodation Accommodation;


        public AccommodationReservationForm(Accommodation accommodation, User guest)
        {
            InitializeComponent();
            DataContext = this;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            AccommodationsReserved = _accommodationReservationRepository.GetByAccommodationId(accommodation.AccommodationId);
            this.Accommodation = accommodation;
            this.guest = guest;
        }

        private void AvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (Days < Accommodation.MinReservations)
            {
                MessageBox.Show($"Number of days should be at least {Accommodation.MinReservations}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<(DateTime, DateTime)> dateRanges = GenerateDateRanges(StartDate, EndDate, Days);

            if(dateRanges.Count == 0)
            {
                DateTime yearBeginning = DateTime.ParseExact("01/01/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime yearEnding = DateTime.ParseExact("31/12/2024", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateRanges = GenerateDateRanges(yearBeginning, yearEnding, Days);
            }
            foreach (var dateRange in dateRanges)
            {
                DateRangeListBox.Items.Add($"{dateRange.Item1.ToString("dd.MM.yyyy")} - {dateRange.Item2.ToString("dd.MM.yyyy")}");
            }
        }

        private void DateRangeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateRangeListBox.SelectedItem != null)
            {
                string selectedDateRange = DateRangeListBox.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show($"You have selected dates: {selectedDateRange}.", "Confirmation", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    ReservationConfirmation reservationConfirmationWindow = new ReservationConfirmation(Accommodation.AccommodationId, guest.Id, selectedDateRange, Days, Accommodation.MaxGuests);
                    reservationConfirmationWindow.Show();
                    Close();
                }
            }
        }


        private List<(DateTime, DateTime)> GenerateDateRanges(DateTime startDate, DateTime endDate, int days)
        {
            List<(DateTime, DateTime)> dateRanges = new List<(DateTime, DateTime)>();

            DateTime currentStartDate = startDate;
            DateTime currentEndDate = startDate.AddDays(days - 1);

            while (currentEndDate <= endDate)
            {
                bool isReserved = IsReserved(currentStartDate, currentEndDate);
                if (!isReserved)
                {
                    dateRanges.Add((currentStartDate, currentEndDate));
                }

                currentStartDate = currentStartDate.AddDays(1);
                currentEndDate = currentStartDate.AddDays(days - 1);
            }

            return dateRanges;
        }
        private bool IsReserved(DateTime startDate, DateTime endDate)
        {
            foreach (var reservation in AccommodationsReserved)
            {
                if ((startDate <= reservation.EndDate && endDate >= reservation.StartDate) ||
                    (startDate >= reservation.StartDate && endDate <= reservation.EndDate))
                {
                    return true;
                }
            }

            return false;
        }
    }

}

