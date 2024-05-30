using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class SuperGuide : ISerializable
    {

        public int Id { get; set; }
        public int GuideId { get; set; }
        public int LanguageId { get; set; }
        public DateTime AccuiredDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public SuperGuide() { }

        public SuperGuide(int guideId, int languageId, DateTime accuiredDate, DateTime expirationDate)
        {
            GuideId = guideId;
            AccuiredDate = accuiredDate;
            ExpirationDate = expirationDate;
            LanguageId = languageId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuideId = int.Parse(values[1]);
            LanguageId = int.Parse(values[2]);
            AccuiredDate = DateTime.Parse(values[3]);
            ExpirationDate = DateTime.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                GuideId.ToString(),
                LanguageId.ToString(),
                AccuiredDate.ToString(),
                ExpirationDate.ToString()
            };
        }
    }
}
