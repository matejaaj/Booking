using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Tourist;
using BookingApp.WPF.ViewModel.Tourist.Factories;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveMainTabViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DriveReservationViewModel> Reservations { get; private set; }
        public DriveReservation SelectedReservation { get; private set; }

        public User Tourist { get; }

        private DriveReservationViewModelFactory factory;

        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private DetailedLocationService _detailedLocationService;
        private DriverUnreliableReportService _driverReportService;

        public event PropertyChangedEventHandler PropertyChanged;

        public DriveMainTabViewModel(User user, DriveReservationService driveReservationService, UserService userService, DetailedLocationService detailedLocationService, DriverUnreliableReportService driverReport)
        {
            Tourist = user;
            _driveReservationService = driveReservationService;
            _userService = userService;
            _detailedLocationService = detailedLocationService;
            _driverReportService = driverReport;

            factory = new DriveReservationViewModelFactory(Tourist, driveReservationService, userService, detailedLocationService);

            Reservations = new ObservableCollection<DriveReservationViewModel>();
            UpdateView();

            TranslationSource.Instance.PropertyChanged += OnLanguageChanged;
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

        private void OnLanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateView();
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
            return SelectedReservation.DriveReservationStatusId == 4;
        }

        public bool CheckIfDriverAssigned(DriveReservationViewModel reservationViewModel)
        {
            SelectedReservation = _driveReservationService.GetById(reservationViewModel.DriveReservationId);
            return SelectedReservation.DriverId == 0;
        }

        public void ValidateAndMarkDriverAsUnreliable(DriveReservationViewModel reservation)
        {
            if (CheckIfDriverAssigned(reservation))
            {
                MessageBox.Show(TranslationSource.Instance["DriverNotAssignedMessage"]);
                return;
            }

            if (!reservation.CheckTimeDifference())
            {
                MessageBox.Show(TranslationSource.Instance["CannotMarkDriverMessage"]);
                return;
            }

            if (CheckIfDriverArrived(reservation))
            {
                MessageBox.Show(TranslationSource.Instance["DriverArrivedMessage"]);
                return;
            }

            if (CheckIfMarked())
            {
                MessageBox.Show(TranslationSource.Instance["DriverAlreadyMarkedMessage"]);
                return;
            }

            MarkDriverAsUnreliable();
            MessageBox.Show(TranslationSource.Instance["DriverMarkedUnreliableMessage"]);
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
            requestDrive.Closed += (sender, args) => UpdateView();
            requestDrive.Show();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
