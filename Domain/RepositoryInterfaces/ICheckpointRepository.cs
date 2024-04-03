using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository
    {
        List<Checkpoint> GetAll();
        Checkpoint Save(Checkpoint checkpoint);
        void Delete(Checkpoint checkpoint);
        Checkpoint Update(Checkpoint checkpoint);
    }
}
