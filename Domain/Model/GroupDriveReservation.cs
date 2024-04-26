using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class GroupDriveReservation : ISerializable
    {
        public int Id { get; set; }
        public int NumberOfPeople { get; set; }
        public int LanguageId { get; set; }
        public int PickupLocationId { get; set; }
        public int DropoffLocationid { get; set; }
        public DateTime DepartureTime { get; set; }
        public int TouristId { get; set; }

        public int StatusId { get; set; }
        public GroupDriveReservation()
        {

        }

        public GroupDriveReservation(int numberOfPeople, int languageId, int pickupLocationId, int dropoffLocationId, DateTime departureTime, int touristId, int statusId)
        {
            NumberOfPeople = numberOfPeople;
            LanguageId = languageId;
            PickupLocationId = pickupLocationId;
            DropoffLocationid = dropoffLocationId;
            DepartureTime = departureTime;
            TouristId = touristId;

            StatusId = statusId;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            NumberOfPeople = Convert.ToInt32(values[1]);
            LanguageId = Convert.ToInt32(values[2]);
            PickupLocationId = Convert.ToInt32(values[3]);
            DropoffLocationid = Convert.ToInt32(values[4]);
            DepartureTime = DateTime.Parse(values[5]);
            TouristId = Convert.ToInt32(values[6]);
            StatusId = Convert.ToInt32(values[7]);
        }


        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                NumberOfPeople.ToString(),
                LanguageId.ToString(),
                PickupLocationId.ToString(),
                DropoffLocationid.ToString(),
                DepartureTime.ToString(), 
                TouristId.ToString(),
                StatusId.ToString()
            };
        }

    }
}
