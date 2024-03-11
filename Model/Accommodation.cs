using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Model
{
    public enum Type { APARTMENT, HOUSE, COTTAGE };
    public class Accommodation : ISerializable
    {
        public int AccommodationId { get; set; }   
        public string Name { get; set; }
        public Location Location {  get; set; }
        public Type Type { get; set; }
        public int maxGuests { get; set; }
        public int minReservations { get; set; }
        public int cancelThershold { get; set; }
        public List<string> imageSource;
        public User Owner;

        public Accommodation()
        {
            imageSource = new List<string>();
        }

        public Accommodation(string name, int maxGuests, int minReservations, int cancelThershold)
        {
            Name = name;
            this.maxGuests = maxGuests;
            this.minReservations = minReservations;
            this.cancelThershold = cancelThershold;
            imageSource = new List<string>();
        }

        public void FromCSV(string[] values)
        {
            AccommodationId = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location() { locationId = Convert.ToInt32(values[2]) };
            Type accommodationType;
            if (Enum.TryParse(values[3], out accommodationType))
            {
                Type = accommodationType;
            }
            else
            {
                Console.WriteLine($"Error converting '{values[3]}' to Type enum. Setting default value.");
                Type = Type.APARTMENT;
            }
            maxGuests = Convert.ToInt32(values[4]);
            minReservations = Convert.ToInt32(values[5]);
            cancelThershold = Convert.ToInt32(values[6]);
            Owner = new User() { Id = Convert.ToInt32(values[7]) };
        }

        public string[] ToCSV()
        {
            string[] csvValues = { AccommodationId.ToString(), Name, Location.locationId.ToString(), Type.ToString(), maxGuests.ToString(), minReservations.ToString(), cancelThershold.ToString(), Owner.Id.ToString() };
            return csvValues;
        }
    }
}
