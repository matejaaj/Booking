using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.LogicServices.Driver
{
    class VacationService
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

        public bool sendRequest(DateOnly startDate, DateOnly endDate, int typeId, bool isEmergency)
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
                if(founded >= 0) 
                {
                    List<DriveReservation> driveReservationsNew = driveReservationService.GetByDriver(founded);
                    List<DriveReservation> driveReservationsOld = driveReservationService.GetByDriver(DriverId);
                    driveReservationsOld.ForEach(driveReservation =>
                    {
                        driveReservation.DriverId = founded;
                        driveReservationService.Update(driveReservation);
                    });
                    AllowVacation(startDate, endDate, typeId);
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
                        isAccepted.Invoke(this, new EventArgs());
                        if(IsAccepted)
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
                    AllowVacation(startDate, endDate, typeId);
                    return true;
                }
            }
            return false;
        }

        private void AllowVacation(DateOnly startDate, DateOnly endDate, int typeId)
        {
            DriverOnVacation dov =  new DriverOnVacation(DriverId, startDate, endDate, typeId, 1);
            driverOnVacationService.Save(dov);
        }

        public List<VacationType> GetTypes()
        {
            return vacationTypeService.GetAll();
        }
    }
}
