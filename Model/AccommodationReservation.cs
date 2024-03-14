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
        public Accommodation Accommodation { get; set; }
        public User Guest { get; set; }
        public AccommodationReservation()
        {
        }
        public AccommodationReservation(DateTime startDate, DateTime endDate, int days, int guestNumber, Accommodation accommodation, User guest)
        {
            StartDate = startDate;
            EndDate = endDate;
            Days = days;
            GuestNumber = guestNumber;
            Accommodation = accommodation;
            Guest = guest;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), StartDate.ToString(), EndDate.ToString(), Days.ToString(), GuestNumber.ToString(), Accommodation.AccommodationId.ToString(), Guest.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            DateTime startDate;
            if (DateTime.TryParseExact(values[1], "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                StartDate = startDate;
            }
            else
            {
                // Handle parsing failure
                Console.WriteLine("Failed to parse start date from CSV.");
            }
            DateTime endDate;
            if (DateTime.TryParseExact(values[2], "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out endDate))
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
            Accommodation = new Accommodation() { AccommodationId = Convert.ToInt32(values[5]) };
            Guest = new User() { Id = Convert.ToInt32(values[6]) };
        }

    }
}
