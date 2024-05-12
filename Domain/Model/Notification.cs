using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;


namespace BookingApp.Domain.Model
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateIssued { get; set; }
        public int TargetUserId { get; set; }

        public Notification() { }

        public Notification(string title, string text, DateTime dateIssued, int targetUserId)
        {
            Title = title;
            Text = text;
            DateIssued = dateIssued;
            TargetUserId = targetUserId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Title = values[1];
            Text = values[2];
            DateIssued = DateTime.Parse(values[3]);
            TargetUserId = int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                Title,
                Text,
                DateIssued.ToString("yyyy-MM-ddTHH:mm:ss"),
                TargetUserId.ToString()
            };
        }
    }
}

