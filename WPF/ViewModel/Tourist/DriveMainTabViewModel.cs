using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Tourist;
using BookingApp.WPF.ViewModel.Tourist.Factories;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveMainTabViewModel
    {
        public ObservableCollection<DriveReservationViewModel> Reservations { get; private set; }
        public DriveReservation SelectedReservation { get; private set; }


        public User Tourist { get;  }

        private DriveReservationViewModelFactory factory;

        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private DetailedLocationService _detailedLocationService;
        private DriverUnreliableReportService _driverReportService;

        public DriveMainTabViewModel(User user, DriveReservationService driveReservationService, UserService userService, DetailedLocationService detailedLocationService, DriverUnreliableReportService driverReport)
        {
            Tourist = user;
            _driveReservationService = driveReservationService;
            _userService = userService;
            _detailedLocationService = detailedLocationService;
            _driverReportService = driverReport;

            factory = new DriveReservationViewModelFactory(Tourist, driveReservationService, userService,
                detailedLocationService);


            Reservations = new ObservableCollection<DriveReservationViewModel>();
            UpdateView();

        }

        private void UpdateView()
        {
            Reservations.Clear();
            var newItems = factory.CreateViewModels();
            foreach (var item in newItems)
            {
                Reservations.Add(item);
            }
        }


        public void AddDelay(DriveReservationViewModel selectedReservationViewModel, int delay)
        {
            var reservation = _driveReservationService.GetById(selectedReservationViewModel.DriveReservationId);
            reservation.DelayMinutesTourist = delay;
            _driveReservationService.Update(reservation);
            UpdateView();
        }

        public bool CheckIfMarked()
        {
            var driverId = SelectedReservation.DriverId;
            return _driverReportService.ReportExists(SelectedReservation.Id, Tourist.Id, driverId);
        }

        public bool CheckIfDriverArrived(DriveReservationViewModel reservationViewModel)
        {
            SelectedReservation = _driveReservationService.GetById(reservationViewModel.DriveReservationId);
            return SelectedReservation.DriveReservationStatusId == 4;
        }

        public void MarkDriverAsUnreliable()
        {
            var driverId = SelectedReservation.DriverId;
            DateTime time = DateTime.Now;
            DriverUnreliableReport report = new DriverUnreliableReport(Tourist.Id, driverId, SelectedReservation.Id, time);
            _driverReportService.Save(report);
        }


        public void OpenReservationWindos()
        {
            DriveReservationWindow requestDrive = new DriveReservationWindow(Tourist);
            requestDrive.Show();
        }
    }
}
