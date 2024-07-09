using BookingApp.Application.Events;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.LogicServices.Driver
{
    public class LocStateUpdateService
    {
        private static DriveReservationService driveReservationService = new DriveReservationService();
        private static VehicleService vehicleService = new VehicleService();
        private static LocationStateService locationStateService= new LocationStateService();
        private int driverId;
        private LocationState superLocation, badLocation;
        private List<LocationState> locations;

        public LocStateUpdateService(int driverId)
        {
            this.driverId = driverId;
            this.locations = locationStateService.GetAll();
            CheckState();
        }

        public void CheckState()
        {
            if (superLocation == null && badLocation == null && locations.Count <= 0)
            {
                List<DriveReservation> driveReservations = driveReservationService.GetAll().Where(d => d.DepartureTime >= DateTime.Now.AddYears(-1)).ToList();
                var locationRideCounts = driveReservations
                .GroupBy(r => r.PickupLocationId)
                .Select(g => new { LocationId = g.Key, Count = g.Count() })
                .OrderByDescending(l => l.Count)
                .ToList();

                if (locationRideCounts.Any())
                {
                    var mostPopularLocation = locationRideCounts.First();
                    var leastPopularLocation = locationRideCounts.Last();

                    superLocation = new LocationState { LocationId = mostPopularLocation.LocationId };
                    badLocation = new LocationState { LocationId = leastPopularLocation.LocationId };
                    locationStateService.Save(new LocationState() { LocationId = superLocation.LocationId, LocState = State.SUPER});
                    locationStateService.Save(new LocationState() { LocationId = badLocation.LocationId, LocState = State.BAD });
                    List<Vehicle> vehicles = vehicleService.GetAll();
                    foreach (var vehicle in vehicles)
                    {
                        if (vehicle.LocationId.Contains(superLocation.LocationId) && vehicle.DriverId == driverId)
                        {
                            SendNotification("Excellent location!");
                        }

                        if (vehicle.LocationId.Contains(badLocation.LocationId) && vehicle.DriverId == driverId)
                        {
                            SendNotification("Poor location!");
                        }
                    }
                }
            }
            else
            {
                badLocation = locations.Find(l => l.LocState == State.BAD);
                superLocation = locations.Find(l => l.LocState == State.SUPER);
                var vehicles = vehicleService.GetAll();
                foreach (var vehicle in vehicles)
                {
                    if (vehicle.LocationId.Contains(superLocation.LocationId) && vehicle.DriverId == driverId)
                    {
                        SendNotification("Excellent location!");
                    }

                    if (vehicle.LocationId.Contains(badLocation.LocationId) && vehicle.DriverId == driverId)
                    {
                        SendNotification("Poor location!");
                    }
                }
            }
        }
        private void SendNotification(string message)
        {
            MainWindow.EventAggregator.Publish<ShowMessageEvent>(new ShowMessageEvent(message, "Notification"));
        }
    }
}
