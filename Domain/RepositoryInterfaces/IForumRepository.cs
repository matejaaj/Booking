using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumRepository
    {
        List<Forum> GetAll();
        Forum Save(Forum forum);
        int NextId();
        void Delete(Forum forum);
        Forum Update(Forum forum);
        List<Forum> GetByLocation(Location location);
        Forum GetById(int id);
    }
}
