using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum RenovationRecommendationLevel { LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5 }
    public class RenovationRecommendation : ISerializable
    {
        public int Id { get; set; }
        public string RecommendationInfo { get; set; }
        public RenovationRecommendationLevel RenovationLevel { get; set; }
        public int ReservationId { get; set; }

        public RenovationRecommendation()
        {
        }
        public RenovationRecommendation(int id, string recommendationInfo, RenovationRecommendationLevel level, int reservationId)
        {
            Id = id;
            RecommendationInfo = recommendationInfo;
            RenovationLevel = level;
            ReservationId = reservationId;
        }
        public void FromCSV(string[] values)
        { 
            Id = Convert.ToInt32(values[0]);
            RecommendationInfo = values[1];
            Enum.TryParse(values[2], out RenovationRecommendationLevel level);
            RenovationLevel = level;
            ReservationId = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), RecommendationInfo, RenovationLevel.ToString(), ReservationId.ToString()};
            return csvValues;
        }

    }
}
