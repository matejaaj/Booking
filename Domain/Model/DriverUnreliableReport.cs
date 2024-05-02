using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class DriverUnreliableReport : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int DriverId { get; set; }

        public int DriveReservationId { get; set; }
        public DateTime DateIssued { get; set; }

        public DriverUnreliableReport() { }

        public DriverUnreliableReport(int touristId, int driverId, int reservationId, DateTime dateIssued)
        {

            TouristId = touristId;
            DriverId = driverId;
            DateIssued = dateIssued;
            DriveReservationId = reservationId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TouristId = int.Parse(values[1]);
            DriverId = int.Parse(values[2]);
            DriveReservationId = int.Parse(values[3]);
            DateIssued = DateTime.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                TouristId.ToString(),
                DriverId.ToString(),
                DriveReservationId.ToString(), 
                DateIssued.ToString("yyyy-MM-dd")
            };
        }
    }
}
