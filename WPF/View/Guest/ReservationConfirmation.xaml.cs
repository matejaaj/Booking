using BookingApp.Application.UseCases;
using BookingApp.WPF.ViewModel.Guest;
using System.Windows;

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for ReservationConfirmation.xaml
    /// </summary>
    public partial class ReservationConfirmation : Window
    {
        private ReservationConfirmationViewModel _viewModel;

        public ReservationConfirmation(int accommodationId, int guestId, string selectedDateRange, int days, int maxCapacity)
        {
            InitializeComponent();
            _viewModel = new ReservationConfirmationViewModel(accommodationId, guestId, selectedDateRange, days, maxCapacity, new AccommodationReservationService());
            DataContext = _viewModel;
        }

        private void ConfirmReservationButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ConfirmReservation();
            this.Close();
        }
    }
}