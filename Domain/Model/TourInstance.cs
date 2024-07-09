using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class TourInstance : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int RemainingSlots { get; set; }
        public DateTime StartTime { get; set; }

        public int GuideId { get; set; }
        public bool IsCompleted { get; set; }

        public string CurrentCheckpoint { get; set; }

        public TourInstance() { }

        public TourInstance(int tourId, int capacity, DateTime startTime, int guideId)
        {
            TourId = tourId;
            RemainingSlots = capacity;
            GuideId = guideId;
            StartTime = startTime;
            IsCompleted = false;
            CurrentCheckpoint = "START";

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            RemainingSlots = int.Parse(values[2]);
            GuideId = int.Parse(values[3]);
            StartTime = DateTime.Parse(values[4]);
            IsCompleted = bool.Parse(values[5]);
            CurrentCheckpoint = values[6];
        }

        public string[] ToCSV()
        {
            return new string[] {
            Id.ToString(),
            TourId.ToString(),
            RemainingSlots.ToString(),
            GuideId.ToString(),
            StartTime.ToString(),
            IsCompleted.ToString(),
            CurrentCheckpoint
            };
        }
    }
}
