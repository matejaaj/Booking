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
        public double DelayMinutes { get; set; }


        public DriveReservation() { }


        public DriveReservation(int id, int pickupLocationId, int dropoffLocationId, DateTime departureTime, int driverId, int touristId, int driveReservationStatusId, double delayMinutes)
        {
            Id = id;
            PickupLocationId = pickupLocationId;
            DropoffLocationid = dropoffLocationId;
            DepartureTime = departureTime;
            DriverId = driverId;
            TouristId = touristId;
            DriveReservationStatusId = driveReservationStatusId;
            DelayMinutes = delayMinutes;
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
            DelayMinutes = Convert.ToDouble(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), PickupLocationId.ToString(), DropoffLocationid.ToString(), DepartureTime.ToString("o"), DriverId.ToString(), TouristId.ToString(), DriveReservationStatusId.ToString(), DelayMinutes.ToString() };
            return csvValues;
        }

    }
}
