using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationStateRepository
    {
        List<LocationState> GetAll();
        LocationState Get(int id);
        LocationState Update(LocationState state);
        void Delete(int id);
        LocationState Save(LocationState state);
    }
}
