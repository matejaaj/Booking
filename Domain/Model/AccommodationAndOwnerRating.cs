using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class AccommodationAndOwnerRating : ISerializable
    {
        public int RatingId { get; set; }
        public int OwnerId { get; set; }
        public int AccommodationReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int OwnershipEthics { get; set; }
        public string Comment { get; set; }
        public int RenovationRecommendationId { get; set; }
        public bool IsRenovationRecommended { get; set; }

        public AccommodationAndOwnerRating() { }

        public AccommodationAndOwnerRating(int accommodationReservationId, int cleanliness, int ownershipEthics, string comment, bool isRenovationRecommended, int renovationRecommendationId)
        {
            AccommodationReservationId = accommodationReservationId;
            Cleanliness = cleanliness;
            OwnershipEthics = ownershipEthics;
            Comment = comment;
            IsRenovationRecommended = isRenovationRecommended;
            RenovationRecommendationId = renovationRecommendationId;
        }

        public void FromCSV(string[] values)
        {
            RatingId = Convert.ToInt32(values[0]);
            AccommodationReservationId = Convert.ToInt32(values[1]);
            Cleanliness = Convert.ToInt32(values[2]);
            OwnershipEthics = Convert.ToInt32(values[3]);
            Comment = values[4];
            IsRenovationRecommended = Convert.ToBoolean(values[5]);
            RenovationRecommendationId = Convert.ToInt32(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { RatingId.ToString(),
                                   AccommodationReservationId.ToString(),
                                   Cleanliness.ToString(),
                                   OwnershipEthics.ToString(),
                                   Comment,
                                   IsRenovationRecommended.ToString(),
                                   RenovationRecommendationId.ToString()};
            return csvValues;
        }
    }
}
