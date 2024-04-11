using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    using System;

    namespace BookingApp.Domain.Model
    {
        public class Voucher : ISerializable
        {
            public int Id { get; set; }
            public int TouristId { get; set; }
            public int GuideId { get; set; }
            public DateTime ExpiryDate { get; set; }

            public Voucher() { }


            public Voucher(int touristId, int guideId, DateTime expiryDate)
            {

                TouristId = touristId;
                GuideId = guideId;
                ExpiryDate = expiryDate;
            }

            public void FromCSV(string[] values)
            {
                Id = int.Parse(values[0]);
                TouristId = int.Parse(values[1]);
                GuideId = int.Parse(values[2]);
                ExpiryDate = DateTime.Parse(values[3]);
            }

            public string[] ToCSV()
            {
                return new string[] {
                Id.ToString(),
                TouristId.ToString(),
                GuideId.ToString(),
                ExpiryDate.ToString("yyyy-MM-dd") // Format date as ISO 8601 (Year-Month-Day)
            };
            }
        }
    }

}
