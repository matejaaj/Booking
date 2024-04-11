using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetByIds(List<int> ids)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.Where(user => ids.Contains(user.Id)).ToList();
        }
        public List<User> GetAll()
        {
            _users = _serializer.FromCSV(FilePath);
            return _users;
        }

        public User GetById(int id)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.Find(u => u.Id == id);
        }
    }
}
