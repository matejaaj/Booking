using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    
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

        public TourRequestSegment(int tourRequestId, string description, int locationId, int languageId, int capacity, DateTime fromDate, DateTime toDate) 
        {
            TourRequestId = tourRequestId;
            Description = description;
            LocationId = locationId;
            LanguageId = languageId;
            Capacity = capacity;
            FromDate = fromDate;
            ToDate = toDate;
            AcceptedDate = DateTime.Now;
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
            FromDate = DateTime.Parse(values[6]);
            ToDate = DateTime.Parse(values[7]);
            AcceptedDate = DateTime.Parse(values[8]);
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
                FromDate.ToString(),
                ToDate.ToString(),
                AcceptedDate.ToString(),
            };
        }
    }
}
