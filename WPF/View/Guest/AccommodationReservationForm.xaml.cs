using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.ViewModel.Guest;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest
{
    public partial class AccommodationReservationForm : Window
    {
        private AccommodationReservationFormViewModel _viewModel;

        public AccommodationReservationForm(Accommodation accommodation, User guest)
        {
            InitializeComponent();
            _viewModel = new AccommodationReservationFormViewModel(accommodation, guest, new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>()));
            DataContext = _viewModel;
        }

        private void AvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.CheckAndGenerateDateRanges())
            {
                MessageBox.Show($"Number of days should be at least {_viewModel.Accommodation.MinReservations}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateRangeListBox.ItemsSource = _viewModel.DateRanges.Select(dr => $"{dr.Item1:dd.MM.yyyy} - {dr.Item2:dd.MM.yyyy}");
        }

        private void DateRangeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateRangeListBox.SelectedItem != null)
            {
                string selectedDateRange = DateRangeListBox.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show($"You have selected dates: {selectedDateRange}. Would you like to proceed with the reservation?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    ReservationConfirmation reservationConfirmationWindow = new ReservationConfirmation(
                        _viewModel.Accommodation.AccommodationId, _viewModel.Guest.Id, selectedDateRange, _viewModel.Days, _viewModel.Accommodation.MaxGuests);
                    reservationConfirmationWindow.Show();
                    this.Close();
                }
            }
        }
    }
}