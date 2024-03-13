using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum TripStatus
    {
        DriverArrived,
        TripStarted,
        TripEnded
    }


    public class Trip : ISerializable
    {
        public int Id { get; set; }
        public int DriveReservationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal StartPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public TripStatus Status { get; set; }

        public Trip()
        {
            StartTime = DateTime.Now; 
            Status = TripStatus.DriverArrived; 
            StartPrice = 0; 
            FinalPrice = 0; 
        }

        public Trip(int driveReservationId, DateTime startTime, decimal startPrice, TripStatus status)
        {
            DriveReservationId = driveReservationId;
            StartTime = startTime;
            StartPrice = startPrice;
            Status = status;

            EndTime = null; 
            FinalPrice = startPrice; 
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            DriveReservationId = Convert.ToInt32(values[1]);
            StartTime = DateTime.Parse(values[2]);
            EndTime = string.IsNullOrEmpty(values[3]) ? (DateTime?)null : DateTime.Parse(values[3]);
            StartPrice = decimal.Parse(values[4]);
            FinalPrice = decimal.Parse(values[5]);
            Status = (TripStatus)Enum.Parse(typeof(TripStatus), values[6]);
        }

        public string[] ToCSV()
        {
            string endTimeValue = EndTime.HasValue ? EndTime.Value.ToString("o") : "";
            string[] csvValues = {
            Id.ToString(),
            DriveReservationId.ToString(),
            StartTime.ToString("o"),
            endTimeValue,
            StartPrice.ToString(),
            FinalPrice.ToString(),
            Status.ToString()
        };
            return csvValues;
        }
    }

}
