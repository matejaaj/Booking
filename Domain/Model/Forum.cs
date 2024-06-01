using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public ForumComment Comment {getset}
        public string Comment { get; set; }
        public int LocationId { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsUsefull { get; set; }
        public DateTime DateOpened { get; set; }
        public Forum() { }

        public Forum(int userId, string comment, int locationId, bool isCancelled, bool isUsefull)
        {
            UserId = userId;
            Comment = comment;
            LocationId = locationId;
            IsCancelled = isCancelled;
            IsUsefull = isUsefull;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Comment = values[2];
            LocationId = Convert.ToInt32(values[3]);
            IsCancelled = Convert.ToBoolean(values[4]);
            IsUsefull = Convert.ToBoolean(values[5]);
            DateTime dateOpened;
            if (DateTime.TryParseExact(values[6], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dateOpened))
            {
                DateOpened = dateOpened;
            }
            else
            {
                Console.WriteLine("Failed to parse start date from CSV.");
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                UserId.ToString(),
                Comment,
                LocationId.ToString(),
                IsCancelled.ToString(),
                IsUsefull.ToString(),
                DateOpened.ToString("dd.MM.yyyy")
            };
            return csvValues;
        }
    }
}
