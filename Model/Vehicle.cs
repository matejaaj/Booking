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
        public List<int> LocationId { get; set; }

        public int MaxPassengers { get; set; }

        public List<int> LanguageId { get; set; }

        public List<string> imageSource;
        
        public int DriverId { get; set; }

        public Vehicle() 
        {
            LocationId = new List<int>();
            LanguageId = new List<int>();
            imageSource = new List<string>();
        }

        public Vehicle( List<int> locationId, int maxPassengers, List<int> languageId)
        {
            LocationId = locationId;
            MaxPassengers = maxPassengers;
            LanguageId = languageId;
            imageSource = new List<string>();
        }   

        public void FromCSV(string[] values)
        {
            VehicleId = Convert.ToInt32(values[0]);
            foreach (string l in values[1].Split(','))
            {
                if (int.TryParse(l, out _))
                    LocationId.Add(Convert.ToInt32(l));
            }
            MaxPassengers = Convert.ToInt32(values[2]);
            foreach(string s in values[3].Split(','))
            {
                if(int.TryParse(s,out _))
                LanguageId.Add(Convert.ToInt32(s));
            }
            
        }

        public string[] ToCSV()
        {
            string lang = string.Join(",", LanguageId);
            string langloc = string.Join(",", LocationId);
            string[] csvValues = { VehicleId.ToString(), langloc, MaxPassengers.ToString(), lang };
            return csvValues;
        }
    }
}
