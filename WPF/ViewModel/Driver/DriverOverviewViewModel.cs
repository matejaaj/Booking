using BookingApp.Application;
using BookingApp.Application.Events;
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
using System.Windows.Navigation;
using System.Windows.Threading;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class DriverOverviewViewModel : INotifyPropertyChanged
    {
        public List<Location> locations { get; set; }
        public GroupDriveService groupDriveService { get; set; }
        public NotificationService notificationService { get; set; }

        public UserService userService { get; set; }

        public DriveReservation SelectedReservation { get; set; }
        public bool canCancel { get; set; }

        private DriveReservationService driveReservationService { get; set; }
        private LocationRepository _locationRepository { get; set; }
        private VehicleService vehicleService { get; set; }

        private DispatcherTimer cancelTime = new DispatcherTimer();
        private DispatcherTimer fastDriveTimer = new DispatcherTimer();

        private DriveReservation? confirmedReservation;
        public DriveReservation? ConfirmedReservation
        {
            get { return confirmedReservation; }
            set
            {
                confirmedReservation = value;
                OnPropertyChanged(nameof(ConfirmedReservation));
                IsVisible = ConfirmedReservation == null ? Visibility.Visible : Visibility.Hidden;
            }
        }
        private int sec = 0;
        private int secTourist = 0;

        private int DriverId;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set { _vehicles = value; OnPropertyChanged(); }
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

        private Visibility isVisible;
        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged(); // Notify the view of the change
                if (IsVisible.Equals(Visibility.Visible))
                {
                    MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Hidden));
                }
                else
                {
                    MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Visible));
                }
            }
        }


        public User Korisnik { get; set; }

        public DriverOverviewViewModel(User driver)
        {
            DriverId = driver.Id;
            Korisnik = driver;
            Vehicles = new ObservableCollection<Vehicle>();
            driveReservationService = new DriveReservationService();
            groupDriveService = new GroupDriveService(DriverId, DataGrid_Refresh);
            DriveReservations = new ObservableCollection<DriveReservation>(driveReservationService.GetByDriver(driver.Id));
            userService = new UserService(Injector.CreateInstance<IUserRepository>());
            notificationService = new NotificationService(Injector.CreateInstance<INotificationRepository>());
            _locationRepository = new LocationRepository();
            vehicleService = new VehicleService();
            locations = _locationRepository.GetAll();
            canCancel = false;
            ConfirmedReservation = null;
            IsVisible = ConfirmedReservation == null ? Visibility.Visible : Visibility.Hidden;
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



        public void ViewDrive_Click(object sender, RoutedEventArgs e, Page owner)
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
            owner.NavigationService.Navigate(vForm);
        }



        public void UpdateReservationList()
        {
            ObservableCollection<DriveReservation> _reservations = new ObservableCollection<DriveReservation>(driveReservationService.GetByDriver(DriverId));
            _reservations = new ObservableCollection<DriveReservation>(_reservations.OrderBy(d => d.DriveReservationStatusId).ToList());
            DriveReservations.Clear();
            foreach (DriveReservation reservation in _reservations)
            {
                if (reservation.DriveReservationStatusId == 2 || reservation.DriveReservationStatusId == 13 || reservation.DriveReservationStatusId == 4)
                {
                    DriveReservations.Clear();
                    ConfirmedReservation = reservation;
                    DriveReservations.Add(reservation);
                    IsVisible = ConfirmedReservation == null ? Visibility.Visible : Visibility.Hidden;
                    if (IsVisible.Equals(Visibility.Visible))
                    {
                        MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Hidden));
                    }
                    else
                    {
                        MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Visible));
                    }
                    return;
                }
                DriveReservations.Add(reservation);
                IsVisible = ConfirmedReservation == null ? Visibility.Visible : Visibility.Hidden;
                if (IsVisible.Equals(Visibility.Visible))
                {
                    MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Hidden));
                }
                else
                {
                    MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Visible));
                }
            }
        }

        public void DataGrid_Refresh(object? sender, EventArgs e)
        {
            UpdateReservationList();
            if (sender is ViewDriveViewModel && ConfirmedReservation.DriveReservationStatusId == 4)
            {
                cancelTime.Stop();
                sec = 0;
                secTourist = 0;
                ConfirmedReservation.UpdateTourist();
                driveReservationService.Update(ConfirmedReservation);
                cancelTime.Start();
            }
            canCancel = false;
        }

        public void ViewDrive_Respond(object? sender, EventArgs e)
        {
            if (!ValidateInput(() => (SelectedReservation.DriveReservationStatusId == 1 || SelectedReservation.DriveReservationStatusId == 14), "You can't confirm this one!"))
            {
                return;
            }
            SelectedReservation.DriveReservationStatusId = 2;
            driveReservationService.Update(SelectedReservation);
            SendNotification(SelectedReservation);
            DataGrid_Refresh(sender, e);
        }

        private void SendNotification(DriveReservation reservation)
        {
            string title = "Pronadjen vozač";
            string text = "Pronađen vozać " +
                          userService.GetById(reservation.DriverId).Username +
                          " za  vožnju za" +
                          reservation.DepartureTime.ToString("HH:mm dd.MM.yyyy");
            Notification notification = new Notification()
            {
                DateIssued = DateTime.Now,
                Title = title,
                Text = text,
                TargetUserId = reservation.TouristId
            };
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
            ConfirmedReservation = null;
            UpdateReservationList();
            if (SuperDriverService.CanceledResevationByDriver(DriverId))
                MainWindow.EventAggregator.Publish(new ShowMessageEvent("You just lost status of Super-Driver!", "Notification"));
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
                MainWindow.EventAggregator.Publish(new ShowMessageEvent("Client hasn't showed up!", "Notification"));
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
                bool? result = ((MainWindow)System.Windows.Application.Current.MainWindow).DialogOverlayResult;
                if(result.HasValue && !result.Value)
                    MainWindow.EventAggregator.Publish(new ShowDialogEvent($"Nova brza rezervacija je dostupna!\n{dr.DepartureTime}\nDa li je prihvatate?", "Notifaction"));

                if (result.HasValue && result.Value)
                {
                    HandleFastReservationAcceptance(dr);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).DialogOverlayResult = false;
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
                MainWindow.EventAggregator.Publish(new ShowMessageEvent("Congratulations!\nYou are now Super-Driver!", "Notification"));
            if (SuperDriverService.IsReadyForBonus(DriverId))
                MainWindow.EventAggregator.Publish(new ShowMessageEvent("Congratulations!\nYou are now getting paid more!", "Notification"));
            driveReservationService.Update(reservation);
            UpdateReservationList();
            SendNotification(reservation);
        }

        public void btnDrive_Click(object sender, RoutedEventArgs e, Page owner)
        {
            if (!ValidateInput(() => !(SelectedReservation.DriveReservationStatusId != 4
                                    || SelectedReservation.DelayMinutesDriver != -1),
                              "You don't have any confirmed reservation or you aren't at location!"))
            {
                return;
            }

            DriveOverview dForm = new DriveOverview(driveReservationService);
            dForm.VM.Reservation = SelectedReservation;
            dForm.VM.Finished += VehicleForm_VehicleAdded;
            owner.NavigationService.Navigate(dForm);
        }

        public void VehicleForm_VehicleAdded(object? sender, EventArgs e)
        {
            UpdateVehicleCount();
            UpdateReservationList();
            IsVisible = Visibility.Visible;
            if (IsVisible.Equals(Visibility.Visible))
            {
                MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Hidden));
            }
            else
            {
                MainWindow.EventAggregator.Publish(new MenuItemsEvent(Visibility.Visible));
            }
        }

        private bool ValidateInput(Func<bool> condition, string errorMessage)
        {
            if (SelectedReservation == null)
            {
                MainWindow.EventAggregator.Publish(new ShowMessageEvent("You haven't selected any reservation!", "Error" ));
                return false;
            }

            if (!condition())
            {
                MainWindow.EventAggregator.Publish(new ShowMessageEvent(errorMessage, "Error"));
                return false;
            }

            return true;
        }
    }
}

