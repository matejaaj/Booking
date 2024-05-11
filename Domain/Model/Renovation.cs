using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Renovation : ISerializable
    {
        public int Id { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }   
        public int AccommodationId { get; set; }

        public Renovation() { }

        public Renovation(DateTime startDate, DateTime endDate, int accommodationId)
        {
            StartDate = startDate;
            EndDate = endDate;
            AccommodationId = accommodationId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
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
            AccommodationId = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), StartDate.ToString("dd.MM.yyyy"), EndDate.ToString("dd.MM.yyyy"), AccommodationId.ToString() };
            return csvValues;
        }
    }
}
