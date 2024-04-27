using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BookingApp.LogicServices.Driver
{
    public class GroupDriveService
    {
        private static readonly GroupDriveReservationService groupDriveReservationService = new GroupDriveReservationService();
        private static readonly VehicleService vehicleService = new VehicleService();
        private static readonly DriveReservationService driveReservationService = new DriveReservationService();
        private readonly DispatcherTimer timer;
        public event EventHandler eventHandler;
        private int driverId { get; set; }

        public GroupDriveService(int DriverID, EventHandler e)
        {
            this.driverId = DriverID;
            this.timer = new DispatcherTimer();
            this.eventHandler += e;
            timer.Interval = System.TimeSpan.Parse("00:00:05");
            timer.Tick += TimerTick;
            StartChecking();
        }

        private void StartChecking()
        {
            timer.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void TimerTick(object? sender, EventArgs e)
        {
            List<GroupDriveReservation> groups = groupDriveReservationService.GetAll();
            foreach (GroupDriveReservation g in groups.ToList())
            {
                timer.Stop();
                int driver = FindDriver(g);
                if (driver == -1)
                {
                    timer.Start();
                    return;
                }
                DriveReservation driveReservation = new DriveReservation();
                driveReservation.DropoffLocationid = g.DropoffLocationid;
                driveReservation.PickupLocationId = g.PickupLocationId;
                driveReservation.DriveReservationStatusId = g.StatusId;
                driveReservation.TouristId = g.TouristId;
                driveReservation.DriverId = driver;
                driveReservation.DepartureTime = g.DepartureTime;
                driveReservation.UpdateTourist();
                driveReservationService.Save(driveReservation);
                eventHandler?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("You have new group drive!\nSee it in your list of reservations!", "Group drive notification", MessageBoxButton.OK);
                timer.Start();
            }
        }

        private int FindDriver(GroupDriveReservation g)
        {
            int driver = -1;
            List<Vehicle> vehicles = vehicleService.GetAll().Where(v => v.DriverId == this.driverId).ToList();
            foreach (Vehicle v in vehicles)
            {
                if (v.LocationId.Contains(g.PickupLocationId) && v.LanguageId.Contains(g.LanguageId))
                {
                    DeleteResevation(g, v);
                    return v.DriverId;
                }
            }
            return driver;
        }

        
        private void DeleteResevation(GroupDriveReservation g, Vehicle v)
        {
            g.NumberOfPeople -= v.MaxPassengers;
            if (g.NumberOfPeople > 0)
                groupDriveReservationService.Update(g);
            else
                groupDriveReservationService.Delete(g);
        }
    }
}
