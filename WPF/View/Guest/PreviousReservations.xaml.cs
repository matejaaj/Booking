using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Guest;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest
{
    public partial class PreviousReservations : Page
    {
        PreviousReservationsViewModel _viewModel;
        public PreviousReservations(User guest)
        {
            InitializeComponent();
            _viewModel = new PreviousReservationsViewModel(guest);
            DataContext = _viewModel;
            ReservationsListBox.ItemsSource = _viewModel.ReservationInfos;
        }

        private void RequestModificationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reservationDto = button.DataContext as ReservationDisplayDTO;
            if (reservationDto != null)
            {
                _viewModel?.RequestModification(reservationDto);
            }
        }
        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reservationDto = button.DataContext as ReservationDisplayDTO;
            _viewModel.Rate(reservationDto);
        }
        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reservationDto = button.DataContext as ReservationDisplayDTO;
            if (reservationDto != null)
            {
                _viewModel?.CancelReservation(reservationDto);
            }
        }
    }
}
