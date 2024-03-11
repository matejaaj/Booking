using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int LanguageId { get; set; }
        public int Capacity { get; set; }
        public float DurationHours { get; set; }

        public Tour() { }

        public Tour(int id, string name, string description, int locationId, int languageId, int capacity, float durationHours)
        {
            Id = id;
            Name = name;
            Description = description;
            LocationId = locationId;
            LanguageId = languageId;
            Capacity = capacity;
            DurationHours = durationHours;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            LocationId = int.Parse(values[3]);
            LanguageId = int.Parse(values[4]);
            Capacity = int.Parse(values[5]);
            DurationHours = float.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            return new string[] {
            Id.ToString(),
            Name,
            Description,
            LocationId.ToString(),
            LanguageId.ToString(),
            Capacity.ToString(),
            DurationHours.ToString("F2") // Assuming you want to format the duration to two decimal places
        };
        }
    }
}
