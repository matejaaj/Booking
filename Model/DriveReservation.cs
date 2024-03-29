using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Model
{
    public class DriveReservation : ISerializable
    {
        public int Id { get; set; }
        public int PickupLocationId { get; set; }
        public int DropoffLocationid { get; set; }
        public DateTime DepartureTime { get; set; }
        public int DriverId { get; set; }
        public int TouristId { get; set; }
        public int DriveReservationStatusId { get; set; }
        public double DelayMinutesDriver { get; set; }

        public double DelayMinutesTourist { get; set; }

        public DriveReservation() {
            
        }

        public User Tourist { get; set; }
        public DriveReservationStatus Status { get; set; }

        public DriveReservation(int pickupLocationId, int dropoffLocationId, DateTime departureTime, int driverId, int touristId, int driveReservationStatusId, double delayMinutesDriver, double delayMinutesTourist)
        {
            PickupLocationId = pickupLocationId;
            DropoffLocationid = dropoffLocationId;
            DepartureTime = departureTime;
            DriverId = driverId;
            TouristId = touristId;
            DriveReservationStatusId = driveReservationStatusId;
            DelayMinutesDriver = delayMinutesDriver;
            DelayMinutesTourist = delayMinutesTourist;
            Tourist = new UserRepository().GetAll().Where(r => r.Id == touristId).First();
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            PickupLocationId = Convert.ToInt32(values[1]);
            DropoffLocationid = Convert.ToInt32(values[2]);
            DepartureTime = DateTime.Parse(values[3]);
            DriverId = Convert.ToInt32(values[4]);
            TouristId = Convert.ToInt32(values[5]);
            DriveReservationStatusId = Convert.ToInt32(values[6]);
            DelayMinutesDriver = Convert.ToDouble(values[7]);
            DelayMinutesTourist = Convert.ToDouble(values[8]);
        }

        public void UpdateTourist()
        {
            Tourist = new UserRepository().GetAll().Where(r => r.Id == TouristId).First();
            Status = new DriveReservationStatusRepository().GetAll().Where(r => r.Id == DriveReservationStatusId).First();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), PickupLocationId.ToString(), DropoffLocationid.ToString(), DepartureTime.ToString("o"), DriverId.ToString(), TouristId.ToString(), DriveReservationStatusId.ToString(), DelayMinutesDriver.ToString(), DelayMinutesTourist.ToString() };
            return csvValues;
        }

    }
}
