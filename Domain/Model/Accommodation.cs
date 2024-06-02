using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Domain.Model
{
    public enum Type { APARTMENT, HOUSE, COTTAGE };
    public class Accommodation : ISerializable
    {
        public int AccommodationId { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Type Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservations { get; set; }
        public int CancelThershold { get; set; }
        public List<int> ImageIds;
        public int OwnerId;

        public Accommodation()
        {
            ImageIds = new List<int>();
        }

        public Accommodation(string name, int locationId, string type, int maxGuests, int minReservations, int cancelThershold, int ownerId)
        {
            Name = name;
            LocationId = locationId;
            Enum.TryParse(type, out Type typeEnum);
            Type = typeEnum;
            MinReservations = minReservations;
            MaxGuests = maxGuests;
            CancelThershold = cancelThershold;
            OwnerId = ownerId;
            ImageIds = new List<int>();
        }

        public Accommodation(string name, int locationId, string type, int maxGuests, int minReservations, int cancelThershold, int ownerId, List<int> ids)
        {
            Name = name;
            LocationId = locationId;
            Enum.TryParse(type, out Type typeEnum);
            Type = typeEnum;
            MinReservations = minReservations;
            MaxGuests = maxGuests;
            CancelThershold = cancelThershold;
            OwnerId = ownerId;
            ImageIds = ids;
        }

        public void FromCSV(string[] values)
        {
            AccommodationId = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Enum.TryParse(values[3], out Type typeEnum);
            Type = typeEnum;
            MaxGuests = Convert.ToInt32(values[4]);
            MinReservations = Convert.ToInt32(values[5]);
            CancelThershold = Convert.ToInt32(values[6]);
            OwnerId = Convert.ToInt32(values[7]);
            foreach (string s in values[8].Split(','))
            {
                if (int.TryParse(s, out _))
                    ImageIds.Add(Convert.ToInt32(s));
            }
        }

        public string[] ToCSV()
        {
            string images = string.Join(",", ImageIds);
            string[] csvValues = { AccommodationId.ToString(),
                                   Name, LocationId.ToString(),
                                   Type.ToString(),
                                   MaxGuests.ToString(),
                                   MinReservations.ToString(),
                                   CancelThershold.ToString(),
                                   OwnerId.ToString(),
                                   images};
            return csvValues;
        }
    }
}
