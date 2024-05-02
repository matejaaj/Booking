using BookingApp.Serializer;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public TourRequestStatus IsAccepted { get; set; }

        public bool IsComplex { get; set; }

        public TourRequest() { }
        public TourRequest(int id, int touristId, bool isComplex)
        {
            Id = id;
            TouristId = touristId;
            IsAccepted = TourRequestStatus.PENDING;
            IsComplex = isComplex;
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                TouristId.ToString(),
                IsAccepted.ToString(),
                IsComplex.ToString(),
            };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TouristId = int.Parse(values[1]);
            IsAccepted = (TourRequestStatus)Enum.Parse(typeof(TourRequestStatus), values[2]);
            IsComplex = bool.Parse(values[3]);
        }
    }
}
