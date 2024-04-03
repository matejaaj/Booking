using BookingApp.Serializer;
using System;
using System.Security.RightsManagement;

namespace BookingApp.Domain.Model
{
    public enum Role { OWNER, GUEST, GUIDE, TOURIST, DRIVER, NONE }
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Enum.TryParse(values[3], out Role roleEnum);
            Role = roleEnum;
        }
    }
}
