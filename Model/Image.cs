using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ImageResourceType 
{
    ACCOMMODATION, TOUR
}


namespace BookingApp.Model
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int EntityId { get; set; }
        public ImageResourceType Type { get; set; }

        public Image() { }

        public Image(string path, int entityId, ImageResourceType type)
        {
            Path = path;
            EntityId = entityId;
            Type = type;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Path = values[1];
            EntityId = int.Parse(values[2]);
            Type = (ImageResourceType)Enum.Parse(typeof(ImageResourceType), values[3]);
        }
        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), Path, EntityId.ToString(), Type.ToString() };
        }
    }
}
