using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public int GuestNumber { get; set; }
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }
        public AccommodationReservation()
        {
        }
        public AccommodationReservation(DateTime startDate, DateTime endDate, int days, int guestNumber, int accommodationId, int guestId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Days = days;
            GuestNumber = guestNumber;
            AccommodationId = accommodationId;
            GuestId = guestId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), StartDate.ToString("dd.MM.yyyy"), EndDate.ToString("dd.MM.yyyy"), Days.ToString(), GuestNumber.ToString(), AccommodationId.ToString(), GuestId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            DateTime startDate;
            if (DateTime.TryParseExact(values[1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                StartDate = startDate;
            }
            else
            {
                // Handle parsing failure
                Console.WriteLine("Failed to parse start date from CSV.");
            }
            DateTime endDate;
            if (DateTime.TryParseExact(values[2], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                EndDate = endDate;
            }
            else
            {
                // Handle parsing failure
                Console.WriteLine("Failed to parse start date from CSV.");
            }
            Days = Convert.ToInt32(values[3]);
            GuestNumber = Convert.ToInt32(values[4]);
            AccommodationId = Convert.ToInt32(values[5]);
            GuestId = Convert.ToInt32(values[6]);
        }

    }
}
