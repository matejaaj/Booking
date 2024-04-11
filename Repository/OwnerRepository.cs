using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/owners.csv";

        private readonly Serializer<Owner> _serializer;

        private List<Owner> _owners;

        public OwnerRepository()
        {
            _serializer = new Serializer<Owner>();
            _owners = _serializer.FromCSV(FilePath);
        }

        public Owner GetByUsername(string username)
        {
            _owners = _serializer.FromCSV(FilePath);
            return _owners.FirstOrDefault(u => u.Username == username);
        }

        public Owner Update(Owner owner)
        {
            _owners = _serializer.FromCSV(FilePath);
            Owner current = _owners.Find(a => a.Id == owner.Id);
            int index = _owners.IndexOf(current);
            _owners.Remove(current);
            _owners.Insert(index, owner);
            _serializer.ToCSV(FilePath, _owners);
            return owner;
        }

        public List<Owner> GetByIds(List<int> ids)
        {
            _owners = _serializer.FromCSV(FilePath);
            return _owners.Where(owner => ids.Contains(owner.Id)).ToList();
        }
        public List<Owner> GetAll()
        {
            _owners = _serializer.FromCSV(FilePath);
            return _owners;
        }

        public Owner GetById(int id)
        {
            _owners = _serializer.FromCSV(FilePath);
            return _owners.Find(u => u.Id == id);
        }
    }
}
