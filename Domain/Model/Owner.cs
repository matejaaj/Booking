using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public enum GENDER { Muski, Zenski };

namespace BookingApp.Domain.Model
{
    public class Owner : User, ISerializable
    {       
        public List<Accommodation> Accommodations { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string JMBG { get; set; }
        public GENDER Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool SuperOwner { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfRatings { get; set; }
        public Owner()
        {
            Accommodations = new List<Accommodation>();
        }

        public Owner(string username, string password, Role userType)
        {
            Username = username;
            Password = password;
            Role = userType;
            Accommodations = new List<Accommodation>();
        }

        public string GenderToCSV()
        {
            if (Gender == GENDER.Muski)
                return "Muski";
            else
                return "Zenski";
        }

        public GENDER GenderFromCSV(string gender)
        {
            if (string.Equals(gender, "Muski"))
                return GENDER.Muski;
            else
                return GENDER.Zenski;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                { Id.ToString(),
                Username,
                Password,
                Role.ToString(),
                SuperOwner.ToString(),
                DateOfBirth.ToString(),
                JMBG,
                GenderToCSV(),
                Address,
                PhoneNumber,
                Email
                };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Enum.TryParse(values[3], out Role roleEnum);
            Role = roleEnum;
            SuperOwner = bool.Parse(values[4]);
            DateOfBirth = DateOnly.Parse(values[5]);
            JMBG = values[6];
            Gender = GenderFromCSV(values[7]);
            Address = values[8];
            PhoneNumber = values[9];
            Email = values[10];
        }
    }
}

