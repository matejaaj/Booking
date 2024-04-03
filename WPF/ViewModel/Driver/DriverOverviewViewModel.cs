using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.View.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class DriverOverviewViewModel : INotifyPropertyChanged
    {
        public List<Location> locations { get; set; }

        public static DriveReservation SelectedReservation { get; set; }
        public static bool canCancel { get; set; }

        private readonly DriveReservationService _repository;
        private readonly LocationRepository _locationRepository;
        private readonly VehicleService _vehicleRepository;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DriveReservation ConfirmedReservation { get; set; }
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
            _repository = new DriveReservationService();
            DriveReservations = new ObservableCollection<DriveReservation>(_repository.GetByDriver(driver.Id));
            _locationRepository = new LocationRepository();
            _vehicleRepository = new VehicleService();
            locations = _locationRepository.GetAll();
            canCancel = false;
            UpdateVehicleCount();
            UpdateReservationList();
            dispatcherTimer.Interval = System.TimeSpan.Parse("00:00:01");
            dispatcherTimer.Tick += Timer_Tick;
        }

        public void UpdateVehicleCount()
        {
            var allVehicles = _vehicleRepository.GetAll();
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
            if (SelectedReservation != null)
            {
                if (SelectedReservation.DriveReservationStatusId != 2)
                {
                    MessageBox.Show("You can't delay resevation if it's not confirmed!");
                    return;
                }
                ViewDrive vForm = new ViewDrive();
                vForm.VM.reservation = SelectedReservation;
                vForm.VM.ReservationConfirmed += DataGrid_Refresh;
                vForm.VM.Repo = _repository;
                vForm.Show();
            }
            else
            {
                MessageBox.Show("You haven't selected any route!");
            }
        }

        public void VehicleForm_VehicleAdded(object? sender, EventArgs e)
        {
            UpdateVehicleCount();
            UpdateReservationList();
        }

        public void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void UpdateReservationList()
        {
            ObservableCollection<DriveReservation> _reservations = new ObservableCollection<DriveReservation>(_repository.GetByDriver(DriverId));
            DriveReservations.Clear();
            foreach (DriveReservation reservation in _reservations)
            {
                if (reservation.DriveReservationStatusId == 2)
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
                dispatcherTimer.Stop();
                sec = 0;
                secTourist = 0;
                dispatcherTimer.Start();
            }
            canCancel = false;
        }

        public void ViewDrive_Respond(object? sender, EventArgs e)
        {
            if (SelectedReservation != null)
            {
                if (SelectedReservation.DriveReservationStatusId != 2)
                {
                    RespondView rvForm = new RespondView(SelectedReservation, _repository);
                    rvForm.VM.ReservationConfirmed += DataGrid_Refresh;
                    rvForm.Show();
                }
                else
                {
                    MessageBox.Show("You already have confirmed reservation!");
                }
            }
            else
            {
                MessageBox.Show("You haven't selected any reservation!");
            }
        }

        public void ViewDrive_Cancel(object? sender, EventArgs e)
        {
            if (SelectedReservation != null)
            {
                if (canCancel)
                {
                    SelectedReservation.DriveReservationStatusId = 8;
                    _repository.Update(SelectedReservation);
                    dispatcherTimer.Stop();
                    sec = 0;
                    secTourist = 0;
                    UpdateReservationList();
                }
                else
                    MessageBox.Show("Still can't cancel!");
            }
            else
            {
                MessageBox.Show("You haven't selected any reservation!");
            }
        }

        public void Timer_Tick(object? sender, EventArgs e)
        {
            if (ConfirmedReservation.DelayMinutesTourist == 0)
            {
                sec++;
            }
            else
            {
                secTourist++;
                if (ConfirmedReservation.DelayMinutesTourist * 10 == secTourist)
                {
                    MessageBox.Show("Client hasn't showed up!");
                    canCancel = true;
                    ConfirmedReservation.DelayMinutesTourist = -1;
                }
            }

            if (sec == 10)
            {
                MessageBox.Show("Client hasn't showed up!");
                canCancel = true;
            }
        }
        public void btnDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtVehicleIdInput, out int vehicleId))
            {
                var allVehicles = _vehicleRepository.GetAll();
                var vehicleToDelete = allVehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
                if (vehicleToDelete != null)
                {
                    _vehicleRepository.Delete(vehicleToDelete);
                    MessageBox.Show("Vozilo je uspešno izbrisano.");
                    UpdateVehicleCount(); // Osvježava prikaz ukupnog broja vozila
                    TxtVehicleIdInput = ""; // Očisti tekstualno polje nakon brisanja
                }
                else
                {
                    MessageBox.Show("Vozilo sa unetim ID-om nije pronađeno.");
                }
            }
            else
            {
                MessageBox.Show("Unesite validan ID vozila.");
            }
        }
        public void txtVehicleIdInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("You have to select ride!");
                return;
            }
            if (SelectedReservation.DriveReservationStatusId != 2)
            {
                MessageBox.Show("Yoy don't have any confirmed reservations!");
                return;
            }
            if (SelectedReservation.DelayMinutesDriver != -1)
            {
                MessageBox.Show("You aren't at location!");
                return;
            }
            DriveOverview dForm = new DriveOverview(_repository);
            dForm.VM.Reservation = SelectedReservation;
            dForm.VM.Finished += VehicleForm_VehicleAdded;
            dForm.Show();
        }
        public void btnStats_Click(object sender, RoutedEventArgs e)
        {
            Stats sForm = new Stats(DriverId, _repository);
            sForm.Show();
        }
    }
}

