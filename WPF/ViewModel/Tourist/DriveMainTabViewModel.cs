using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist.Factories;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveMainTabViewModel
    {
        public ObservableCollection<DriveReservationViewModel> Reservations { get; private set; }
        public DriveReservationViewModel SelectedReservation { get; set; }


        private User _user;

        private DriveReservationViewModelFactory factory;

        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private DetailedLocationService _detailedLocationService;
        private DriverUnreliableReportService _driverReportService;

        public DriveMainTabViewModel(User user, DriveReservationService driveReservationService, UserService userService, DetailedLocationService detailedLocationService, DriverUnreliableReportService driverReport)
        {
            _user = user;
            _driveReservationService = driveReservationService;
            _userService = userService;
            _detailedLocationService = detailedLocationService;
            _driverReportService = driverReport;

            factory = new DriveReservationViewModelFactory(_user, driveReservationService, userService,
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

        public bool CheckIfMarked(DriveReservationViewModel reservationViewModel)
        {
            var reservation = _driveReservationService.GetById(reservationViewModel.DriveReservationId);
            var driverId = reservation.DriverId;
            return _driverReportService.ReportExists(reservationViewModel.DriveReservationId, _user.Id, driverId);
        }

        public bool CheckIfDriverArrived(DriveReservationViewModel reservationViewModel)
        {
            var reservation = _driveReservationService.GetById(reservationViewModel.DriveReservationId);
            return reservation.DriveReservationStatusId == 4;
        }

        public void MarkDriverAsUnreliable(DriveReservationViewModel reservationViewModel)
        {
            var driverId = _driveReservationService.GetById(reservationViewModel.DriveReservationId).DriverId;
            DateTime time = DateTime.Now;
            DriverUnreliableReport report = new DriverUnreliableReport(_user.Id, driverId, reservationViewModel.DriveReservationId, time);
            _driverReportService.Save(report);
        }


    }
}
