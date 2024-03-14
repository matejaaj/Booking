using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Checkpoint : ISerializable
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int TourId { get; set; }

        public Checkpoint() { }

        public Checkpoint(string name, int tourId)
        {
            Name = name;
            TourId = tourId;
        }


        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), Name, TourId.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            TourId = int.Parse(values[2]);
        }
    }
}
