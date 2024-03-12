using BookingApp.Serializer;
using System;
using System.Security.RightsManagement;

namespace BookingApp.Model
{
    public enum UserType { HOST, GUEST, TOURGUIDE, DRIVER, TOURIST}

    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserType Type { get; set; }

        public User() { }

        public User(string username, string password, UserType type)
        {
            Username = username;
            Password = password;
            Type = type;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Type.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Type = (UserType)Enum.Parse(typeof(UserType), values[3]);
        }
    }
}
