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
    public class VacationService
    {
        private readonly DriverOnVacationService driverOnVacationService = new DriverOnVacationService();
        private readonly VacationTypeService vacationTypeService = new VacationTypeService();
        private readonly VehicleService vehicleService = new VehicleService();
        private readonly DriveReservationService driveReservationService = new DriveReservationService();

        private int DriverId;
        private Vehicle? vehicle;

        event EventHandler isAccepted;
        public bool IsAccepted { get; set; }

        public VacationService(int driverId, EventHandler e)
        {
            DriverId = driverId;
            isAccepted = e;
            vehicle = vehicleService.GetAll().Find(v => v.DriverId == driverId);
        }

        public async Task<bool> sendRequest(DateOnly startDate, DateOnly endDate, int typeId, bool isEmergency)
        {
            int[] allowed =
            {
                1, 6, 8, 14
            };
            int founded = -1;
            if(isEmergency)
            {
                List<Vehicle> vehicles = vehicleService.GetAll().Where(v => v.LocationId.Intersect(vehicle.LocationId).Any() && v.DriverId != vehicle?.DriverId).ToList();
                foreach(Vehicle v in vehicles)
                {
                    bool found = true;
                    List<DriveReservation> driveReservations = driveReservationService.GetByDriver(v.DriverId);
                    foreach(DriveReservation driveReservation in driveReservations)
                    {
                        if(!allowed.Contains(driveReservation.DriveReservationStatusId))
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        
                        founded = v.DriverId;
                        break;
                    }
                }
                if (founded >= 0)
                {
                    List<DriveReservation> driveReservationsNew = driveReservationService.GetByDriver(founded);
                    List<DriveReservation> driveReservationsOld = driveReservationService.GetByDriver(DriverId);
                    driveReservationsOld.ForEach(driveReservation =>
                    {
                        driveReservation.DriverId = founded;
                        driveReservationService.Update(driveReservation);
                    });
                    AllowVacation(startDate, endDate, typeId, 1);
                    return true;
                }
            }
            else
            {
                List<Vehicle> vehicles = vehicleService.GetAll().Where(v => v.LocationId.Intersect(vehicle.LocationId).Any() && v.DriverId != vehicle?.DriverId).ToList();
                foreach (Vehicle v in vehicles)
                {
                    bool found = true;
                    List<DriveReservation> driveReservations = driveReservationService.GetByDriver(v.DriverId);
                    foreach (DriveReservation driveReservation in driveReservations)
                    {
                        if (!allowed.Contains(driveReservation.DriveReservationStatusId))
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        var task = MainWindow.EventAggregator.PublishAsync<ShowDialogEvent>(new ShowDialogEvent("Do you accept to do for other guy?", "Vacation request"));
                        await task;
                        if (task != null && task.IsCompleted && ((MainWindow)System.Windows.Application.Current.MainWindow).DialogOverlayResult!.Value)
                        {
                            founded = v.DriverId;
                            break;
                        }
                    }
                }
                if (founded >= 0)
                {
                    List<DriveReservation> driveReservationsNew = driveReservationService.GetByDriver(founded);
                    List<DriveReservation> driveReservationsOld = driveReservationService.GetByDriver(DriverId);
                    driveReservationsOld.ForEach(driveReservation =>
                    {
                        driveReservation.DriverId = founded;
                        driveReservationService.Update(driveReservation);
                    });
                    AllowVacation(startDate, endDate, typeId,1);
                    return true;
                }
            }
            return false;
        }

        public void AllowVacation(DateOnly startDate, DateOnly endDate, int typeId, int statusId)
        {
            DriverOnVacation dov =  new DriverOnVacation(DriverId, startDate, endDate, typeId, statusId);
            driverOnVacationService.Save(dov);
        }

        public List<VacationType> GetTypes()
        {
            return vacationTypeService.GetAll();
        }

        public List<DriverOnVacation> getVacationsForDriver(int driverId)
        {
            return driverOnVacationService.GetAll().Where(d => d.DriverId == driverId).ToList();
        }
    }
}
