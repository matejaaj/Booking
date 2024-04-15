using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int LanguageId { get; set; }
        public int MaximumCapacity { get; set; }
        public float DurationHours { get; set; }

        public Tour() { }

        public Tour(string name, string description, int locationId, int languageId, int capacity, float durationHours)
        {
            Name = name;
            Description = description;
            LocationId = locationId;
            LanguageId = languageId;
            MaximumCapacity = capacity;
            DurationHours = durationHours;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            LocationId = int.Parse(values[3]);
            LanguageId = int.Parse(values[4]);
            MaximumCapacity = int.Parse(values[5]);
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
            MaximumCapacity.ToString(),
            DurationHours.ToString("F2") 
        };
        }
    }
}
