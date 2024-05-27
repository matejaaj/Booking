using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class VacationStatus : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public VacationStatus() { }

        public VacationStatus(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = Convert.ToString(values[1]);
            Description = Convert.ToString(values[2]);
        }

        public string[] ToCSV()
        {
            string[] values =
            {
                Id.ToString(), Name.ToString(), Description.ToString()
            };
            return values;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
