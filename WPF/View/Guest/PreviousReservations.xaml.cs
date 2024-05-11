using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.View.Guest
{
    public partial class PreviousReservations : Window
    {
        private PreviousReservationsViewModel _viewModel;

        public PreviousReservations(User guest)
        {
            InitializeComponent();
            _viewModel = new PreviousReservationsViewModel(guest,
                new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>()),
                new AccommodationService(Injector.CreateInstance<IAccommodationRepository>()),
                new ReservationModificationRequestService(Injector.CreateInstance<IReservationModificationRequestRepository>()));
            DataContext = _viewModel;
            ReservationsListBox.ItemsSource = _viewModel.ReservationInfos;
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reservationDto = button.Tag as ReservationDisplayDTO;
            _viewModel.Rate(reservationDto);
        }

        private void RequestModificationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reservationDto = button.Tag as ReservationDisplayDTO;
            _viewModel.RequestModification(reservationDto);
        }

        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reservationDto = button.Tag as ReservationDisplayDTO;
            _viewModel.CancelReservation(reservationDto);
        }
    }
}