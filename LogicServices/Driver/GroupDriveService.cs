using BookingApp.Application.Events;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Driver;
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
        private static readonly object lokot = new object();
        private int driverId { get; set; }
        private HashSet<int> assignedDrivers = new HashSet<int>();

        public GroupDriveService(int DriverID, EventHandler e)
        {
            this.driverId = DriverID;
            this.timer = new DispatcherTimer();
            this.eventHandler += e;
            timer.Interval = System.TimeSpan.Parse("00:00:10");
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
            lock (lokot)
            {

                timer.Stop();
                var groups = groupDriveReservationService.GetAll().ToList();
                List<GroupDriveReservation> toUpdate = new List<GroupDriveReservation>();
                List<GroupDriveReservation> toDelete = new List<GroupDriveReservation>();

                for (int i = 0; i < groups.Count; i++)
                {
                    GroupDriveReservation g = groups[i];
                    int driver = FindDriver(g);
                    if (driver == -1 || assignedDrivers.Contains(driver))
                        continue;  // Skip this group as no driver is found
                    ProccesReservationCreation(g, driver);

                    if (g.NumberOfPeople > 0)
                        toUpdate.Add(g);
                    else
                    {
                        toDelete.Add(g);
                        i++;
                    }

                    eventHandler?.Invoke(this, EventArgs.Empty);
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("You have new group drive!\nSee it in your list of reservations!","Notification"));
                }

                ProccesDataUpdate(toUpdate, toDelete);

                timer.Start();

            }
        }

        private static void ProccesDataUpdate(List<GroupDriveReservation> toUpdate, List<GroupDriveReservation> toDelete)
        {
            foreach (var group in toUpdate)
            {
                groupDriveReservationService.Update(group);
            }
            toDelete.ForEach(groupDriveReservationService.Delete);
        }

        private void ProccesReservationCreation(GroupDriveReservation g, int driver)
        {
            assignedDrivers.Add(driver);

            DriveReservation driveReservation = new DriveReservation
            {
                DropoffLocationid = g.DropoffLocationid,
                PickupLocationId = g.PickupLocationId,
                DriveReservationStatusId = g.StatusId,
                TouristId = g.TouristId,
                DriverId = driver,
                DepartureTime = g.DepartureTime
            };

            driveReservation.UpdateTourist();
            driveReservationService.Save(driveReservation);
        }

        private int FindDriver(GroupDriveReservation g)
        {
            var vehicles = vehicleService.GetAll()
                .Where(v => v.DriverId == this.driverId && !assignedDrivers.Contains(v.DriverId)).ToList();

            foreach (Vehicle v in vehicles)
            {
                if (v.LocationId.Contains(g.PickupLocationId) && v.LanguageId.Contains(g.LanguageId))
                {
                    DeleteResevation(g, v);
                    return v.DriverId;
                }
            }
            return -1;
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
