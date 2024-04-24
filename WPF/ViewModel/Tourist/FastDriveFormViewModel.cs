using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class FastDriveFormViewModel : BaseDriveFormViewModel
    {
        private VehicleService _vehicleService;
        private DetailedLocationService _detailedLocationService;
        private LocationService _locationService;
        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private User _tourist;

        public FastDriveFormViewModel(User user, UserService userService, VehicleService vehicleService, DetailedLocationService detailedLocationService, LocationService locationService, DriveReservationService driveReservationService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _detailedLocationService = detailedLocationService;
            _locationService = locationService;
            _driveReservationService = driveReservationService;
            _tourist = user;
        }

        public void ReserveFastDrive()
        {
            DateTime departure = CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(SelectedCountry.Key, StartAddress);
            DetailedLocation end = new DetailedLocation(SelectedCountry.Key, EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            DriveReservation reservation = new DriveReservation(start.Id, end.Id, departure, 0, _tourist.Id, 12, 0, 0);
            _driveReservationService.Save(reservation);
        }
    }
}
