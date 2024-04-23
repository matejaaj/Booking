using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    internal interface ISuperDriverStateRepository
    {
        List<SuperDriverState> GetAll();

        SuperDriverState Save(SuperDriverState superDriverState);
        void Delete(SuperDriverState superDriverState);
        SuperDriverState Update(SuperDriverState superDriverState);
        SuperDriverState? GetSuperDriverStateById(int driverId);
    }
}
