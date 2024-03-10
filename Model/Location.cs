using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Location : ISerializable
    {
        int locationId;
        string City { get; set; }
        string Country { get; set; }

        public Location() { }
        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

        public void FromCSV(string[] values)
        {
            locationId = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { locationId.ToString(), City, Country };
            return csvValues;
        }
    }
}
