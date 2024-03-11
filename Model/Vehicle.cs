using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Model
{
    internal class Vehicle : ISerializable
    {
        public int Id { get; set; }
        Location Location { get; set; }

        public int MaxPassengers { get; set; }

        Language Language { get; set; }

        
        public Vehicle() { }

        public Vehicle( Location location, int maxPassengers, Language language)
        {
            Location = location;
            MaxPassengers = maxPassengers;
            Language = language;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = new Location() { locationId = Convert.ToInt32(values[1]) };
            MaxPassengers = Convert.ToInt32(values[2]);
            Language = new Language() { languageId = Convert.ToInt32(values[3]) };
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Location.locationId.ToString(), MaxPassengers.ToString(), Language.languageId.ToString() };
            return csvValues;
        }
    }
}
