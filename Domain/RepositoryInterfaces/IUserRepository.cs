using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        List<User> GetByIds(List<int> ids);
        List<User> GetAll();
        User GetById(int id);
    }
}
