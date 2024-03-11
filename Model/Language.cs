using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Language : ISerializable
    {
         public int languageId;
        
        string Name {  get; set; }

        public Language() { }

        public Language(string name)
        {
            Name = name;
        }


        public void FromCSV(string[] values)
        {
            languageId = Convert.ToInt32(values[0]);
            Name = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { languageId.ToString(), Name };
            return csvValues;
        }
    }
}
