using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveReservationViewModel 
    {
        public User Tourist;

        public DriveFromViewModel FormViewModel { get; private set; }


        private VehicleService _vehicleService;
        private DetailedLocationService _detailedLocationService;
        private LocationService _locationService;
        private DriveReservationService _driveReservationService;
        private UserService _userService;


        public DriveReservationViewModel(User loggedUser)
        {
            Tourist = loggedUser;
            FormViewModel = new DriveFromViewModel();
            InitializeServices();

        }
        public void InitializeServices()
        {
            _vehicleService = new VehicleService();
            _detailedLocationService = new DetailedLocationService();
            _locationService = new LocationService();
            _driveReservationService = new DriveReservationService();
            _userService = new UserService();
        }

        public void FillCities()
        {
            var cities = _locationService.GetCitiesByCountry(FormViewModel.SelectedCountry.Value).ToList();
            FormViewModel.Cities.Clear();
            foreach (var city in cities)
            {
                FormViewModel.Cities.Add(city);
            }
        }
        public void UpdateDriverList()
        {
            DateTime? date = FormViewModel.CreateDateTimeFromSelections();
            List<int> drivers = _vehicleService.GetDriverIdsByLocationId(FormViewModel.SelectedCity.Key);
            drivers = _driveReservationService.FilterAvailableDrivers(drivers, date);
            FormViewModel.FillDrivers(drivers);
        }
        public void Reserve()
        {
            DateTime departure = FormViewModel.CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(FormViewModel.SelectedCountry.Key, FormViewModel.StartAddress);
            DetailedLocation end = new DetailedLocation(FormViewModel.SelectedCountry.Key, FormViewModel.EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            DriveReservation reservation = new DriveReservation(start.Id, end.Id, departure, FormViewModel.SelectedDriver.Key, Tourist.Id, 2, 0,0);
            _driveReservationService.Save(reservation);
            MessageBox.Show("Reservation successful");

        }
    }
}
