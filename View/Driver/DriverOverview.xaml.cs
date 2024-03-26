using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BookingApp.View.Driver
{
    /// <summary>
    /// Interaction logic for DriverOverview.xaml
    /// </summary>
    public partial class DriverOverview : Window
    {
        public static ObservableCollection<Vehicle> Vehicles { get; set; }
        public List<Location> locations { get; set; }

        public static ObservableCollection<DriveReservation> DriveReservations { get; set; }
        public static DriveReservation SelectedReservation { get; set; }
        public static bool canCancel { get; set; }

        private readonly DriveReservationRepository _repository;
        private readonly LocationRepository _locationRepository;
        private readonly VehicleRepository _vehicleRepository;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DriveReservation ConfirmedReservation {  get; set; }
        private int sec = 0;
        private int secTourist = 0;

        private int DriverId;

        public DriverOverview(User driver)
        {
            InitializeComponent();
            DriverId = driver.Id;
            DataContext = this;
            Vehicles = new ObservableCollection<Vehicle>();
            _repository = new DriveReservationRepository();
            DriveReservations = new ObservableCollection<DriveReservation>(_repository.GetByDriver(driver.Id));
            _locationRepository = new LocationRepository();
            _vehicleRepository = new VehicleRepository();
            locations = _locationRepository.GetAll();
            canCancel = false;
            UpdateVehicleCount();
            UpdateReservationList();
            dispatcherTimer.Interval = System.TimeSpan.Parse("00:00:01");
            dispatcherTimer.Tick += Timer_Tick;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateVehicleCount()
        {
            var allVehicles = _vehicleRepository.GetAll();
            txtVehicleCount.Text = $"Ukupno registrovanih vozila: {allVehicles.Count}";
        }

        private void ShowCreateVehicleForm(object sender, RoutedEventArgs e)
        {
            VehicleForm vehicleForm = new VehicleForm(DriverId);
            vehicleForm.VehicleAdded += VehicleForm_VehicleAdded;
            vehicleForm.Show();
        }


        private void ViewDrive_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                if (SelectedReservation.DriveReservationStatusId != 2)
                {
                    MessageBox.Show("You can't delay resevation if it's not confirmed!");
                    return;
                }
                ViewDrive vForm = new ViewDrive();
                vForm.reservation = SelectedReservation;
                vForm.ReservationConfirmed += DataGrid_Refresh;
                vForm.Repo = _repository;
                vForm.Show();
            }
            else
            {
                MessageBox.Show("You haven't selected any route!");
            }
        }

        private void VehicleForm_VehicleAdded(object? sender, EventArgs e)
        {
            UpdateVehicleCount();
            UpdateReservationList();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateReservationList()
        {
            ObservableCollection<DriveReservation> _reservations = new ObservableCollection<DriveReservation>(_repository.GetByDriver(DriverId));
            DriveReservations.Clear();
            foreach(DriveReservation reservation in _reservations)
            {
                if(reservation.DriveReservationStatusId == 2)
                {
                    DriveReservations.Clear();
                    ConfirmedReservation = reservation;
                    DriveReservations.Add(reservation);
                    return;
                }
                DriveReservations.Add(reservation);
            }
        }

        private void DataGrid_Refresh(object? sender, EventArgs e)
        {
            UpdateReservationList();
            if (sender is ViewDrive && ConfirmedReservation.DelayMinutesDriver < 0)
            {
                dispatcherTimer.Stop();
                sec = 0;
                secTourist = 0;
                dispatcherTimer.Start();
            }
            canCancel = false;
        }

        private void ViewDrive_Respond(object? sender, EventArgs e)
        {
            if(SelectedReservation != null)
            {
                if (SelectedReservation.DriveReservationStatusId != 2)
                {
                    RespondView rvForm = new RespondView(SelectedReservation);
                    rvForm.ReservationConfirmed += DataGrid_Refresh;
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

        private void ViewDrive_Cancel(object? sender, EventArgs e)
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
                }else
                    MessageBox.Show("Still can't cancel!");
            }
            else
            {
                MessageBox.Show("You haven't selected any reservation!");
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if(ConfirmedReservation.DelayMinutesTourist == 0)
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
            
            if(sec == 10)
            {
                MessageBox.Show("Client hasn't showed up!");
                canCancel = true;
            }
        }
        private void btnDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtVehicleIdInput.Text, out int vehicleId))
            {
                var allVehicles = _vehicleRepository.GetAll();
                var vehicleToDelete = allVehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
                if (vehicleToDelete != null)
                {
                    _vehicleRepository.Delete(vehicleToDelete);
                    MessageBox.Show("Vozilo je uspešno izbrisano.");
                    UpdateVehicleCount(); // Osvježava prikaz ukupnog broja vozila
                    txtVehicleIdInput.Text = ""; // Očisti tekstualno polje nakon brisanja
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
        private void txtVehicleIdInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnDrive_Click(object sender, RoutedEventArgs e)
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
            DriveOverview dForm = new DriveOverview();
            dForm.Reservation = SelectedReservation;
            dForm.Finished += VehicleForm_VehicleAdded;
            dForm.Show();
        }
    }
}
