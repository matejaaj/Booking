using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using BookingApp.Application.UseCases;
using BookingApp.Application;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Linq;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveReservationFormViewModel
    {
        public User Tourist { get; set; }
        public RegularDriveFormViewModel RegularDriveViewModel { get; private set; }
        public FastDriveFormViewModel FastDriveViewModel { get; private set; }
        public GroupDriveFormViewModel GroupDriveViewModel { get; private set; }

        private VehicleService _vehicleService;
        private DetailedLocationService _detailedLocationService;
        private LocationService _locationService;
        private DriveReservationService _driveReservationService;
        private UserService _userService;

        public ICommand CloseWindowCommand { get; }

        public DriveReservationFormViewModel(User loggedUser)
        {
            CloseWindowCommand = new RelayCommand(CloseWindow);
            Tourist = loggedUser;
            InitializeServices();
            InitializeViewModels();
        }

        private void InitializeViewModels()
        {
            RegularDriveViewModel = new RegularDriveFormViewModel(Tourist, _userService, _vehicleService, _detailedLocationService, _locationService, _driveReservationService, CloseWindowCommand);
            FastDriveViewModel = new FastDriveFormViewModel(Tourist, _detailedLocationService, _driveReservationService, CloseWindowCommand);
            GroupDriveViewModel = new GroupDriveFormViewModel(Tourist, _detailedLocationService, _driveReservationService, CloseWindowCommand);
        }

        private void InitializeServices()
        {
            _vehicleService = new VehicleService(Injector.CreateInstance<IVehicleRepository>());
            _detailedLocationService = new DetailedLocationService(Injector.CreateInstance<IDetailedLocationRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _driveReservationService = new DriveReservationService(Injector.CreateInstance<IDriveReservationRepository>());
            _userService = new UserService(Injector.CreateInstance<IUserRepository>());
        }

        public void CloseWindow(object param)
        {
            if (param is Window window)
            {
                window.Close();
            }

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
    }
}
