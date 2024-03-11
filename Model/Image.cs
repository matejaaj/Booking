using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ImageResourceType { ACCOMODATION, TOUR };


namespace BookingApp.Model
{

    public class Image : ISerializable
    {
        public int id;
        public String Path { get; set; }
        public int EntityId { get; set; }

        public ImageResourceType Type { get; set; }

        public Image() { }
        public Image(int id, string path, int entityId, ImageResourceType type)
        {
            this.id = id;
            Path = path;
            EntityId = entityId;
            Type = type;
        }

        public string[] ToCSV()
        {
            return new string[] { id.ToString(), Path, EntityId.ToString(), Type.ToString() };
        }
        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            Path = values[1];
            EntityId = int.Parse(values[2]);
            Type = (ImageResourceType)Enum.Parse(typeof(ImageResourceType), values[3]);
        }

    }
}
