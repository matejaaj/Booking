using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for ReservationConfirmation.xaml
    /// </summary>
    public partial class ReservationConfirmation : Window, INotifyPropertyChanged
    {
        private int _guestNumber;
        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                if (_guestNumber != value)
                {
                    _guestNumber = value;
                    OnPropertyChanged("GuestNumber");
                }
            }
        }
        private int accommodationId;
        private int guestId;
        private string selectedDateRange;
        private int days;
        private int maxCapacity;
        public ReservationConfirmation(int accommodationId, int guestId, string selectedDateRange, int days,int maxCapacity)
        {
            InitializeComponent();
            DataContext = this;
            this.maxCapacity = maxCapacity;
            this.accommodationId = accommodationId;
            this.guestId = guestId;
            this.selectedDateRange = selectedDateRange;
            this.days = days;
            this.maxCapacity = maxCapacity;
        }

        private void ConfirmReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestNumber > maxCapacity || GuestNumber < 1)
            {
                MessageBox.Show($"Number of people cannot exceed {maxCapacity}. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Reservation successfully confirmed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            AccommodationReservationRepository _accommodationReservationRepository = new AccommodationReservationRepository();
            DateTime startDate, endDate;
            (startDate, endDate) = ConvertStringToDates(selectedDateRange);
            _accommodationReservationRepository.Save(new AccommodationReservation(startDate, endDate, days, GuestNumber, accommodationId, guestId, false, false, false));
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private (DateTime, DateTime) ConvertStringToDates(string slectedDateRange)
        {
            string[] dateRangeParts = selectedDateRange.Split(" - ");
            DateTime selectedStartDate = DateTime.ParseExact(dateRangeParts[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime selectedEndDate = DateTime.ParseExact(dateRangeParts[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return (selectedStartDate, selectedEndDate);
        }
    }
}
