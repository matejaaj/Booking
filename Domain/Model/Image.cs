using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ImageResourceType
{
    ACCOMMODATION, TOUR, VEHICLE, TOUR_REVIEW
}


namespace BookingApp.Domain.Model
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int EntityId { get; set; }

        public int UserId {get; set; }
        public ImageResourceType Type { get; set; }

        public Image() { }

        public Image(string path, int entityId, ImageResourceType type, int userId)
        {
            Path = path;
            EntityId = entityId;
            Type = type;
            UserId = userId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Path = values[1];
            EntityId = int.Parse(values[2]);
            Type = (ImageResourceType)Enum.Parse(typeof(ImageResourceType), values[3]);
            UserId = int.Parse(values[4]);
        }
        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), Path, EntityId.ToString(), Type.ToString(), UserId.ToString() };
        }
    }
}
