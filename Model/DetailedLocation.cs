using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class DetailedLocation : ISerializable
    {
        public int Id { get; set;}
        public int LocationId { get; set;}
        public string Address { get; set;}

        public DetailedLocation() { }


        public DetailedLocation(int locationId, string address)
        {
            LocationId = locationId;
            Address = address;
        }

        public void FromCSV(string[] values)
        {

            Id = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
            Address = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), LocationId.ToString(), Address };
            return csvValues;
        }
    }


}
