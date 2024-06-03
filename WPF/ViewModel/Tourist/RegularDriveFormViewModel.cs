using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System;
using System.Windows.Input;
using BookingApp.Commands;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class RegularDriveFormViewModel : BaseDriveFormViewModel, IDataErrorInfo
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

        public ICommand ReserveCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }

        public RegularDriveFormViewModel(User user, UserService userService, VehicleService vehicleService, DetailedLocationService detailedLocationService, LocationService locationService, DriveReservationService driveReservationService, ICommand closeCommand)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _detailedLocationService = detailedLocationService;
            _locationService = locationService;
            _driveReservationService = driveReservationService;
            _tourist = user;
            CloseWindowCommand = closeCommand;

            SelectedCountry = new KeyValuePair<int, string>(-1, string.Empty);
            SelectedCity = new KeyValuePair<int, string>(-1, string.Empty);
            ReserveCommand = new RelayCommand(ReserveRegularDrive, CanReserveDrive);


            ValidateAllProperties();
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



        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case nameof(SelectedCountry):
                        if (SelectedCountry.Value == string.Empty || !Countries.Any(c => c.Value == SelectedCountry.Value))
                            result = "Izaberite validnu državu.";
                        break;
                    case nameof(SelectedCity):
                        if (SelectedCity.Value == string.Empty || !Cities.Any(c => c.Value == SelectedCity.Value))
                            result = "Izaberite validan grad.";
                        break;
                    case nameof(StartAddress):
                        if (string.IsNullOrWhiteSpace(StartAddress) || !Regex.IsMatch(StartAddress, @"^[a-zA-Z0-9\s]+$"))
                            result = "Unesite validnu početnu adresu.";
                        break;
                    case nameof(EndAddress):
                        if (string.IsNullOrWhiteSpace(EndAddress) || !Regex.IsMatch(EndAddress, @"^[a-zA-Z0-9\s]+$"))
                            result = "Unesite validnu krajnju adresu.";
                        break;
                    case nameof(SelectedDate):
                        if (SelectedDate < DateTime.Today)
                            result = "Datum ne sme biti u prošlosti.";
                        break;
                    case nameof(SelectedHour):
                        if (string.IsNullOrWhiteSpace(SelectedHour))
                            result = "Izaberite sat.";
                        break;
                    case nameof(SelectedMinute):
                        if (string.IsNullOrWhiteSpace(SelectedMinute))
                            result = "Izaberite minut.";
                        break;
                    case nameof(SelectedDriver):
                        if (SelectedDriver.Key == 0)
                            result = "Izaberite vozača.";
                        break;
                }
                return result;
            }
        }


        public string Error => null;

        private bool CanReserveDrive(object parameter)
        {
            return Countries.Any(c => c.Value == SelectedCountry.Value) &&
                   Cities.Any(c => c.Value == SelectedCity.Value) &&
                   !string.IsNullOrWhiteSpace(StartAddress) &&
                   !string.IsNullOrWhiteSpace(EndAddress) &&
                   SelectedDate >= DateTime.Today &&
                   !string.IsNullOrWhiteSpace(SelectedHour) &&
                   !string.IsNullOrWhiteSpace(SelectedMinute) &&
                   SelectedDriver.Key != 0;
        }


        private void ValidateAllProperties()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    OnPropertyChanged(property.Name);
                }
            }
        }



        public void ValidateCountry(string input)
        {
            if (!Countries.Any(c => c.Value == input))
            {
                SelectedCountry = new KeyValuePair<int, string>(-1, string.Empty);
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        public void ValidateCity(string input)
        {
            if (!Cities.Any(c => c.Value == input))
            {
                SelectedCity = new KeyValuePair<int, string>(-1, string.Empty);
                OnPropertyChanged(nameof(SelectedCity));
            }
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