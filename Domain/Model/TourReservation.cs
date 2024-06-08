using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int TourInstanceId { get; set; }
        public int UserId { get; set; }

        public bool VoucherAcquired { get; set; }

        public TourReservation() { }
        public TourReservation(int tourInstanceId, int userId)
        {
            TourInstanceId = tourInstanceId;
            UserId = userId;
            VoucherAcquired = false;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourInstanceId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
            VoucherAcquired = bool.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                TourInstanceId.ToString(),
                UserId.ToString(),
                VoucherAcquired.ToString(),
            };
        }
    }
}
