using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
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

        public DriveFormViewModel FormViewModel { get; private set; }


        private VehicleService _vehicleService;
        private DetailedLocationService _detailedLocationService;
        private LocationService _locationService;
        private DriveReservationService _driveReservationService;
        private UserService _userService;


        public DriveReservationViewModel(User loggedUser)
        {
            Tourist = loggedUser;

            FormViewModel = new DriveFormViewModel();
            InitializeServices();

        }
        private void InitializeServices()
        {
            _vehicleService = new VehicleService(Injector.CreateInstance<IVehicleRepository>());
            _detailedLocationService = new DetailedLocationService(Injector.CreateInstance<IDetailedLocationRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _driveReservationService = new DriveReservationService(Injector.CreateInstance<IDriveReservationRepository>());
            _userService = new UserService(Injector.CreateInstance<IUserRepository>());
        }

        public void CheckForDriverAssignment()
        {
            var statusesToCheck = new List<string> { "CONFIRMED_FAST", "FAST_RESERVATION" };
            var reservations = _driveReservationService.GetByTouristAndStatuses(Tourist.Id, statusesToCheck);

            foreach (var reservation in reservations)
            {
                string formattedDeparture = reservation.DepartureTime.ToString("f"); 

                if (reservation.DriveReservationStatusId == 13) 
                {
                    var driver = _userService.GetById(reservation.DriverId);
                    MessageBox.Show($"Your driver {driver.Username} has been assigned to your trip on {formattedDeparture}.");
                }
                else if (reservation.DriveReservationStatusId == 12) 
                {
                    MessageBox.Show($"A driver has not yet been found for your fast reservation scheduled for {formattedDeparture}.");
                }
            }
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
        public void Reserve(bool isFastDrive)
        {
            DateTime departure = FormViewModel.CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(FormViewModel.SelectedCountry.Key, FormViewModel.StartAddress);
            DetailedLocation end = new DetailedLocation(FormViewModel.SelectedCountry.Key, FormViewModel.EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            if (isFastDrive)
            {
                DriveReservation reservation = new DriveReservation(start.Id, end.Id, departure, 0, Tourist.Id, 12, 0, 0);
                _driveReservationService.Save(reservation);
            } else
            { 
                DriveReservation reservation = new DriveReservation(start.Id, end.Id, departure, FormViewModel.SelectedDriver.Key, Tourist.Id, 2, 0, 0);
                _driveReservationService.Save(reservation);
            }

            MessageBox.Show("Reservation successful");
        }
    }
}
