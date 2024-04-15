using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class ReservationModificationRequest : ISerializable
    {
        public enum RequestStatus { PENDING, APPROVED, REJECTED }
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public RequestStatus Status { get; set; }
        public string OwnerComment { get; set; }
        public ReservationModificationRequest()
        {

        }
        public ReservationModificationRequest(int reservationId, DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate, RequestStatus status, string comment)
        {
            ReservationId = reservationId;
            OldStartDate = oldStartDate;
            OldEndDate = oldEndDate;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            Status = status;
            OwnerComment = comment;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            OldStartDate = DateTime.Parse(values[2]);
            OldEndDate = DateTime.Parse(values[3]);
            NewStartDate = DateTime.Parse(values[4]);
            NewEndDate = DateTime.Parse(values[5]);
            Enum.TryParse(values[6], out RequestStatus statusEnum);
            Status = statusEnum;
            OwnerComment = values[7];
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                ReservationId.ToString(),
                OldStartDate.ToString("dd.MM.yyyy"),
                OldEndDate.ToString("dd.MM.yyy"),
                NewStartDate.ToString("dd.MM.yyy"),
                NewEndDate.ToString("dd.MM.yyy"),
                Status.ToString(),
                OwnerComment
            };
            return csvValues;
        }

    }
}
