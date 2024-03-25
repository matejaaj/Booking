using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class DriveReservationStatus : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void FromCSV(string[] values)
        {
            Console.WriteLine(values);
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name };
            return csvValues;
        }
    }
}
