using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class GuestRating : ISerializable
    {
        public int GuestRatingId { get; set; }
        public int GuestId { get; set; }
        public int AccommodationReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int RulesRespect { get; set; }
        public string Comment { get; set; }

        public GuestRating() { }

        public GuestRating(int guestId, int accommodationReservationId, int cleanliness, int rulesRespect, string comment)
        {
            GuestId = guestId;
            AccommodationReservationId = accommodationReservationId;
            Cleanliness = cleanliness;
            RulesRespect = rulesRespect;
            Comment = comment;
        }

        public void FromCSV(string[] values)
        {
            GuestRatingId = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            AccommodationReservationId = Convert.ToInt32(values[2]);    
            Cleanliness = Convert.ToInt32(values[3]);   
            RulesRespect = Convert.ToInt32(values[4]);
            Comment = values[5];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestRatingId.ToString(), 
                                   GuestId.ToString(), 
                                   AccommodationReservationId.ToString(), 
                                   Cleanliness.ToString(), 
                                   RulesRespect.ToString(), 
                                   Comment};
            return csvValues;
        }
    }
}
