using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int LanguageId { get; set; }
        public int Capacity { get; set; }
        public DateTime FromDate {  get; set; }
        public DateTime ToDate { get; set; }
        public DateTime AcceptedDate { get; set; }
        public bool IsAccepted { get; set; }

        public TourRequest(string description, int locationId, int languageId, int capacity, DateTime fromDate, DateTime toDate) 
        {
            Description = description;
            LocationId = locationId;
            LanguageId = languageId;
            Capacity = capacity;
            FromDate = fromDate;
            ToDate = toDate;
            IsAccepted = false;
            AcceptedDate = DateTime.Now;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Description = values[1];
            LocationId = int.Parse(values[2]);
            LanguageId = int.Parse(values[3]);
            Capacity = int.Parse(values[4]);
            FromDate = DateTime.Parse(values[5]);
            ToDate = DateTime.Parse(values[6]);
            AcceptedDate = DateTime.Parse(values[7]);
            IsAccepted = bool.Parse(values[8]);
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                Description,
                LocationId.ToString(),
                LanguageId.ToString(),
                Capacity.ToString(),
                FromDate.ToString(),
                ToDate.ToString(),
                AcceptedDate.ToString(),
                IsAccepted.ToString()
            };
        }
    }
}
