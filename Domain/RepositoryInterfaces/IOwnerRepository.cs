using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IOwnerRepository
    {
        public Owner GetByUsername(string username);
        public Owner Update(Owner owner);
        public List<Owner> GetByIds(List<int> ids);
        public List<Owner> GetAll();
        public Owner GetById(int id);
    }
}
