using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourInstance : ISerializable
    {
        public int Id { get; set; }
        public int TourId {  get; set; }
        public int Capacity {  get; set; }
        public DateTime StartTime { get; set; }

        public TourInstance() { }

        public TourInstance(int tourId, int capacity, DateTime startTime)
        {
            TourId = tourId;
            Capacity = capacity;
            StartTime = startTime;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            Capacity = int.Parse(values[2]);
            StartTime = DateTime.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            return new string[] {
            Id.ToString(),
            TourId.ToString(),
            Capacity.ToString(),
            StartTime.ToString()
            };
        }
    }
}
