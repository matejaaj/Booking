using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum TourRequestStatus
    {
        PENDING, ACCEPTED, CANCELED
    }
    public class TourRequestSegment : ISerializable
    {
        public int Id { get; set; }
        public int TourRequestId {  get; set; }
        public string? Description { get; set; }
        public int LocationId { get; set; }
        public int LanguageId { get; set; }
        public int Capacity { get; set; }
        public DateTime FromDate {  get; set; }
        public DateTime ToDate { get; set; }
        public DateTime AcceptedDate { get; set; }
        public TourRequestStatus IsAccepted { get; set; }
        public int GuideId { get; set; }

        public TourRequestSegment(int tourRequestId, string description, int locationId, int languageId, int capacity, DateTime fromDate, DateTime toDate) 
        {
            TourRequestId = tourRequestId;
            Description = description;
            LocationId = locationId;
            LanguageId = languageId;
            Capacity = capacity;
            GuideId = -1;
            FromDate = fromDate;
            ToDate = toDate;
            AcceptedDate = DateTime.Now;
            IsAccepted = TourRequestStatus.PENDING;
        }

        public TourRequestSegment()
        {

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourRequestId = int.Parse(values[1]);
            Description = values[2];
            LocationId = int.Parse(values[3]);
            LanguageId = int.Parse(values[4]);
            Capacity = int.Parse(values[5]);
            GuideId = int.Parse(values[6]);
            FromDate = DateTime.Parse(values[7]);
            ToDate = DateTime.Parse(values[8]);
            AcceptedDate = DateTime.Parse(values[9]);
            IsAccepted = (TourRequestStatus)Enum.Parse(typeof(TourRequestStatus), values[10]);
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                TourRequestId.ToString(),
                Description,
                LocationId.ToString(),
                LanguageId.ToString(),
                Capacity.ToString(),
                GuideId.ToString(),
                FromDate.ToString(),
                ToDate.ToString(),
                AcceptedDate.ToString(),
                IsAccepted.ToString(),
            };
        }

    }
}
