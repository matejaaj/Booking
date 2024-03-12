using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Model
{
    public class Vehicle : ISerializable
    {
        public int VehicleId { get; set; }
        public int LocationId { get; set; }

        public int MaxPassengers { get; set; }

        public int LanguageId { get; set; }

        public List<string> imageSource;
        
        public Vehicle() 
        {
            imageSource = new List<string>();
        }

        public Vehicle( int locationId, int maxPassengers, int languageId)
        {
            LocationId = locationId;
            MaxPassengers = maxPassengers;
            LanguageId = languageId;
            imageSource = new List<string>();
        }

        public void FromCSV(string[] values)
        {
            VehicleId = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
            MaxPassengers = Convert.ToInt32(values[2]);
            LanguageId = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { VehicleId.ToString(), LocationId.ToString(), MaxPassengers.ToString(), LanguageId.ToString() };
            return csvValues;
        }
    }
}
