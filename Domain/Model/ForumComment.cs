using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ForumId { get; set; }
        public string Comment { get; set; }
        public bool WasPresent { get; set; }
        public int ReportNumber { get; set; }

        public ForumComment() { }

        public ForumComment(int userId, int forumId, string comment, bool wasPresent, int reportNumber)
        {
            UserId = userId;
            ForumId = forumId;
            Comment = comment;
            WasPresent = wasPresent;
            ReportNumber = reportNumber;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            ForumId = Convert.ToInt32(values[2]);
            Comment = values[3];
            WasPresent = Convert.ToBoolean(values[4]);
            ReportNumber = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                UserId.ToString(),
                ForumId.ToString(),
                Comment,
                WasPresent.ToString(),
                ReportNumber.ToString()
            };
        }
    }
}
