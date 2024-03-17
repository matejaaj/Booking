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
        public int RemainingSlots {  get; set; }
        public DateTime StartTime { get; set; }

        public bool IsCompleted { get; set; }

        public TourInstance() { }

        public TourInstance(int tourId, int capacity, DateTime startTime)
        {
            TourId = tourId;
            RemainingSlots = capacity;
            StartTime = startTime;
            IsCompleted = false;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            RemainingSlots = int.Parse(values[2]);
            StartTime = DateTime.Parse(values[3]);
            IsCompleted = bool.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            return new string[] {
            Id.ToString(),
            TourId.ToString(),
            RemainingSlots.ToString(),
            StartTime.ToString()
            Is.Completed.ToString()
            };
        }
    }
}
