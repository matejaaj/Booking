using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.LogicServices.Driver;
using BookingApp.Repository;
using BookingApp.WPF.View.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class DriverOverviewViewModel : INotifyPropertyChanged
    {
        public List<Location> locations { get; set; }
        public GroupDriveService groupDriveService { get; set; }

        public static DriveReservation SelectedReservation { get; set; }
        public static bool canCancel { get; set; }

        private readonly DriveReservationService driveReservationService;
        private readonly LocationRepository _locationRepository;
        private readonly VehicleService vehicleService;

        private DispatcherTimer cancelTime = new DispatcherTimer();
        private DispatcherTimer fastDriveTimer = new DispatcherTimer();
        private DriveReservation? ConfirmedReservation { get; set; }
        private int sec = 0;
        private int secTourist = 0;

        private int DriverId;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set { _vehicles = value;  OnPropertyChanged(); }
        }

        private ObservableCollection<DriveReservation> _driveReservations;
        public ObservableCollection<DriveReservation> DriveReservations
        {
            get { return _driveReservations; }
            set { _driveReservations = value; OnPropertyChanged(); }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _txtVehicleCount;
        public string TxtVehicleCount
        {
            get { return _txtVehicleCount; }
            set
            {
                _txtVehicleCount = value;
                OnPropertyChanged(nameof(TxtVehicleCount)); // Notify the view of the change
            }
        }

        private string _txtVehicleIdInput;
        public string TxtVehicleIdInput
        {
            get { return _txtVehicleIdInput; }
            set
            {
                _txtVehicleIdInput = value;
                OnPropertyChanged(nameof(TxtVehicleIdInput)); // Notify the view of the change
            }
        }

        public DriverOverviewViewModel(User driver)
        {
            DriverId = driver.Id;
            Vehicles = new ObservableCollection<Vehicle>();
            driveReservationService = new DriveReservationService();
            groupDriveService = new GroupDriveService(DriverId, DataGrid_Refresh);
            DriveReservations = new ObservableCollection<DriveReservation>(driveReservationService.GetByDriver(driver.Id));
            _locationRepository = new LocationRepository();
            vehicleService = new VehicleService();
            locations = _locationRepository.GetAll();
            canCancel = false;
            ConfirmedReservation = null;
            UpdateVehicleCount();
            UpdateReservationList();
            cancelTime.Interval = System.TimeSpan.Parse("00:00:01");
            cancelTime.Tick += CancelTime_Tick;
            fastDriveTimer.Interval = System.TimeSpan.Parse("00:00:05");
            fastDriveTimer.Tick += FastDriveTimer_Tick;
            fastDriveTimer.Start();
        }

        public void UpdateVehicleCount()
        {
            var allVehicles = vehicleService.GetAll();
            TxtVehicleCount = $"Ukupno registrovanih vozila: {allVehicles.Count}";
        }

        public void ShowCreateVehicleForm(object sender, RoutedEventArgs e)
        {
            VehicleForm vehicleForm = new VehicleForm(DriverId);
            vehicleForm.VM.VehicleAdded += VehicleForm_VehicleAdded;
            vehicleForm.Show();
        }

        public void ViewDrive_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput(() => SelectedReservation.DriveReservationStatusId == 2 || SelectedReservation.DriveReservationStatusId == 13, "You can't delay reservation if it's not confirmed or not in the specific required status!"))
            {
                return;
            }

            ViewDrive vForm = new()
            {
                VM = 
                {
                    reservation = SelectedReservation,
                    Repo = driveReservationService
                }
            };
            vForm.VM.ReservationConfirmed += DataGrid_Refresh;
            vForm.Show();
        }

        public void VehicleForm_VehicleAdded(object? sender, EventArgs e)
        {
            UpdateVehicleCount();
            UpdateReservationList();
        }

        public void UpdateReservationList()
        {
            ObservableCollection<DriveReservation> _reservations = new ObservableCollection<DriveReservation>(driveReservationService.GetByDriver(DriverId));
            _reservations = new ObservableCollection<DriveReservation>(_reservations.OrderBy(d => d.DriveReservationStatusId).ToList());
            DriveReservations.Clear();
            foreach (DriveReservation reservation in _reservations)
            {
                if (reservation.DriveReservationStatusId == 2 || reservation.DriveReservationStatusId == 13)
                {
                    DriveReservations.Clear();
                    ConfirmedReservation = reservation;
                    DriveReservations.Add(reservation);
                    return;
                }
                DriveReservations.Add(reservation);
            }
        }

        public void DataGrid_Refresh(object? sender, EventArgs e)
        {
            UpdateReservationList();
            if (sender is ViewDriveViewModel && ConfirmedReservation.DelayMinutesDriver < 0)
            {
                cancelTime.Stop();
                sec = 0;
                secTourist = 0;
                cancelTime.Start();
            }
            canCancel = false;
        }

        public void ViewDrive_Respond(object? sender, EventArgs e)
        {
            if (!ValidateInput(() => SelectedReservation.DriveReservationStatusId == 1, "You can't confirm this one!"))
            {
                return;
            }

            RespondView rvForm = new RespondView(SelectedReservation, driveReservationService);
            rvForm.VM.ReservationConfirmed += DataGrid_Refresh;
            rvForm.Show();
        }



        public void ViewDrive_Cancel(object? sender, EventArgs e)
        {
            if (!ValidateInput(() => canCancel, "Still can't cancel!"))
            {
                return;
            }
            SelectedReservation.DriveReservationStatusId = 8;
            driveReservationService.Update(SelectedReservation);
            cancelTime.Stop();
            sec = 0;
            secTourist = 0;
            UpdateReservationList();
            if (SuperDriverService.CanceledResevationByDriver(DriverId))
                MessageBox.Show("You just lost status of Super-Driver!", "Super-Driver Notification", MessageBoxButton.OK);
        }

        public void CancelTime_Tick(object? sender, EventArgs e)
        {
            if (ConfirmedReservation.DelayMinutesTourist == 0)
            {
                sec++;
            }
            else
            {
                secTourist++;
            }

            bool isClientNoShow = (ConfirmedReservation.DelayMinutesTourist == 0 && sec == 10) ||
                                  (ConfirmedReservation.DelayMinutesTourist != 0 && ConfirmedReservation.DelayMinutesTourist * 10 == secTourist);

            if (isClientNoShow)
            {
                MessageBox.Show("Client hasn't showed up!");
                canCancel = true;
                ConfirmedReservation.DelayMinutesTourist = -1;
            }
        }


        public void FastDriveTimer_Tick(object? sender, EventArgs e)
        {
            if (ConfirmedReservation != null)
            {
                return;
            }

            var fastReservations = driveReservationService.GetAll()
                .Where(dr => dr.DriveReservationStatusId == 12 &&
                             vehicleService.GetDriverIdsByLocationId(dr.PickupLocationId).Contains(DriverId))
                .ToList();

            if (!fastReservations.Any())
            {
                return;
            }

            fastDriveTimer.Stop();
            foreach (var dr in fastReservations)
            {
                MessageBoxResult result = MessageBox.Show($"Nova brza rezervacija je dostupna!\n{dr.DepartureTime}\nDa li je prihvatate?",
                                                          "Brza voznja", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    HandleFastReservationAcceptance(dr);
                    return; 
                }
            }
            fastDriveTimer.Start();
        }

        private void HandleFastReservationAcceptance(DriveReservation reservation)
        {
            DriveReservations.Clear();
            reservation.DriveReservationStatusId = 13;
            reservation.DriverId = DriverId;
            if (SuperDriverService.UpdateStateForDriver(DriverId))
                MessageBox.Show("Congratulations!\nYou are now Super-Driver!","Super-Driver Notification", MessageBoxButton.OK);
            if (SuperDriverService.IsReadyForBonus(DriverId))
                MessageBox.Show("Congratulations!\nYou are now getting paid more!", "Super-Driver Notification", MessageBoxButton.OK);
            driveReservationService.Update(reservation);
            UpdateReservationList();
        }

        public void btnDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(TxtVehicleIdInput, out int vehicleId);
            var allVehicles = vehicleService.GetAll();
            var vehicleToDelete = allVehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
            if (vehicleToDelete != null)
            {
                vehicleService.Delete(vehicleToDelete);
                MessageBox.Show("Vozilo je uspešno izbrisano.");
                UpdateVehicleCount(); 
                TxtVehicleIdInput = ""; 
            }
        }

        public void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput(() => !(SelectedReservation.DriveReservationStatusId != 2 | SelectedReservation.DriveReservationStatusId != 13
                                    || SelectedReservation.DelayMinutesDriver != -1),
                              "You don't have any confirmed reservation or you aren't at location!"))
            {
                return;
            }

            DriveOverview dForm = new DriveOverview(driveReservationService);
            dForm.VM.Reservation = SelectedReservation;
            dForm.VM.Finished += VehicleForm_VehicleAdded;
            dForm.Show();
        }

        public void btnStats_Click(object sender, RoutedEventArgs e)
        {
            Stats sForm = new Stats(DriverId, driveReservationService);
            sForm.Show();
        }

        private bool ValidateInput(Func<bool> condition, string errorMessage)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("You haven't selected any reservation!");
                return false;
            }

            if (!condition())
            {
                MessageBox.Show(errorMessage);
                return false;
            }

            return true;
        }
    }
}

