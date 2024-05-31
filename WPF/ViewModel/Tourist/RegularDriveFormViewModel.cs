

using BookingApp.Application.UseCases;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class RegularDriveFormViewModel : BaseDriveFormViewModel
    {
        private readonly VehicleService _vehicleService;
        private readonly DetailedLocationService _detailedLocationService;
        private readonly LocationService _locationService;
        private readonly DriveReservationService _driveReservationService;
        private readonly UserService _userService;
        private readonly User _tourist;


        private ObservableCollection<KeyValuePair<int, string>> _drivers = new ObservableCollection<KeyValuePair<int, string>>();
        private KeyValuePair<int, string> _selectedDriver;

        public ObservableCollection<KeyValuePair<int, string>> Drivers => _drivers;

        [Required(ErrorMessage = "Izaberite vozača.")]
        public KeyValuePair<int, string> SelectedDriver
        {
            get => _selectedDriver;
            set
            {
                _selectedDriver = value;
                OnPropertyChanged(nameof(SelectedDriver));
            }
        }

        public ICommand ReserveCommand { get; }

        public RegularDriveFormViewModel(User user, UserService userService, VehicleService vehicleService, DetailedLocationService detailedLocationService, LocationService locationService, DriveReservationService driveReservationService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _detailedLocationService = detailedLocationService;
            _locationService = locationService;
            _driveReservationService = driveReservationService;
            _tourist = user;

            ReserveCommand = new RelayCommand(ReserveRegularDrive);
        }

        public void FillDrivers(List<int> driverIds)
        {
            var drivers = _userService.GetByIds(driverIds);
            Drivers.Clear();
            foreach (var driver in drivers)
            {
                Drivers.Add(new KeyValuePair<int, string>(driver.Id, driver.Username));
            }
        }

        public void UpdateDriverList()
        {
            DateTime? date = CreateDateTimeFromSelections();
            List<int> drivers = _vehicleService.GetDriverIdsByLocationId(SelectedCity.Key);
            drivers = _driveReservationService.FilterAvailableDrivers(drivers, date);
            FillDrivers(drivers);
        }

        public void ReserveRegularDrive(object parameter)
        {

            DateTime departure = CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(SelectedCountry.Key, StartAddress);
            DetailedLocation end = new DetailedLocation(SelectedCountry.Key, EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            DriveReservation reservation = new DriveReservation(start.Id, end.Id, departure, SelectedDriver.Key, _tourist.Id, 2, 0, 0);
            _driveReservationService.Save(reservation);
            MessageBox.Show("Rezervacija uspešno izvršena!");

            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}