using BookingApp.Application.UseCases;
using System;
using System.Windows.Input;
using BookingApp.Domain.Model;
using System.Windows;
using BookingApp.Commands;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class FastDriveFormViewModel : BaseDriveFormViewModel
    {
        private readonly DetailedLocationService _detailedLocationService;
        private readonly DriveReservationService _driveReservationService;
        private readonly User _tourist;
        public ICommand CloseWindowCommand { get; }
        public ICommand ReserveCommand { get; }

        public FastDriveFormViewModel(User user, DetailedLocationService detailedLocationService, DriveReservationService driveReservationService, ICommand closeCommand)
        {
            _detailedLocationService = detailedLocationService;
            _driveReservationService = driveReservationService;
            _tourist = user;
            CloseWindowCommand = closeCommand;
            ReserveCommand = new RelayCommand(ReserveFastDrive);
        }

        public void ReserveFastDrive(object parameter)
        {
            DateTime departure = CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(SelectedCountry.Key, StartAddress);
            DetailedLocation end = new DetailedLocation(SelectedCountry.Key, EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            DriveReservation reservation = new DriveReservation(start.Id, end.Id, departure, 0, _tourist.Id, 12, 0, 0);
            _driveReservationService.Save(reservation);
            MessageBox.Show("Fast drive reservation successful!");

            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}