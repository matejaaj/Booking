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
        public Location Location { get; set; }

        public int locationId {  get; set; }

        public int MaxPassengers { get; set; }

        public Language Language { get; set; }
        public int languageId {  get; set; }

        public List<string> imageSource;
        
        public Vehicle() 
        {
            imageSource = new List<string>();
        }

        public Vehicle( int locationId, int maxPassengers, int languageId)
        {
            this.locationId = locationId;
            MaxPassengers = maxPassengers;
            this.languageId = languageId;
            imageSource = new List<string>();
        }

        public void FromCSV(string[] values)
        {
            VehicleId = Convert.ToInt32(values[0]);
            Location = new Location() { locationId = Convert.ToInt32(values[1]) };
            MaxPassengers = Convert.ToInt32(values[2]);
            Language = new Language() { languageId = Convert.ToInt32(values[3]) };
        }

        public string[] ToCSV()
        {
            string[] csvValues = { VehicleId.ToString(), Location.locationId.ToString(), MaxPassengers.ToString(), Language.languageId.ToString() };
            return csvValues;
        }
    }
}
