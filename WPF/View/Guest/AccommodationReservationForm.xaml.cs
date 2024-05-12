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
        private readonly AccommodationReservationFormViewModel _viewModel;

        public AccommodationReservationForm(Accommodation accommodation, User guest)
        {
            InitializeComponent();
            _viewModel = new AccommodationReservationFormViewModel(accommodation, guest);
            DataContext = _viewModel;
        }

        private void AvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CheckAvailability();
            DateRangeListBox.ItemsSource = _viewModel.DateRanges.Select(dr => $"{dr.Item1:dd.MM.yyyy} - {dr.Item2:dd.MM.yyyy}");
        }

        private void DateRangeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.HandleDateRangeSelection(DateRangeListBox.SelectedItem?.ToString());
        }
    }
}