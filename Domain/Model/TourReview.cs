using BookingApp.Serializer;
using Syncfusion.UI.Xaml.TreeGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourReview : ISerializable
    {
        public int Id { get; set; }
        public int TourInstanceId { get; set; }
        public int TouristId { get; set; }
        public int TourGuestId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public bool IsValid { get; set; }

        public TourReview() { }

        public TourReview(int tourId, int touristId, int tourGuestId, int rating, string comment, bool isValid)
        {
            TourInstanceId = tourId;
            TouristId = touristId;
            TourGuestId = tourGuestId;
            Rating = rating;
            Comment = comment;
            IsValid = isValid;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourInstanceId = int.Parse(values[1]);
            TouristId = int.Parse(values[2]);
            TourGuestId = int.Parse(values[3]);
            Rating = int.Parse(values[4]);
            Comment = values[5];
            IsValid = bool.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                TourInstanceId.ToString(),
                TouristId.ToString(),
                TourGuestId.ToString(),
                Rating.ToString(),
                Comment,
                IsValid.ToString()
            };
        }
    }
}
