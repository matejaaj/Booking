using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class GuestRating : ISerializable
    {
        public int GuestRatingId { get; set; }
        public int AccommodationReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int RulesRespect { get; set; }
        public string Comment { get; set; }

        public GuestRating() { }

        public GuestRating(int accommodationReservationId, int cleanliness, int rulesRespect, string comment)
        {
            AccommodationReservationId = accommodationReservationId;
            Cleanliness = cleanliness;
            RulesRespect = rulesRespect;
            Comment = comment;
        }

        public void FromCSV(string[] values)
        {
            GuestRatingId = Convert.ToInt32(values[0]);
            AccommodationReservationId = Convert.ToInt32(values[1]);
            Cleanliness = Convert.ToInt32(values[2]);
            RulesRespect = Convert.ToInt32(values[3]);
            Comment = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestRatingId.ToString(),
                                   AccommodationReservationId.ToString(),
                                   Cleanliness.ToString(),
                                   RulesRespect.ToString(),
                                   Comment};
            return csvValues;
        }
    }
}
