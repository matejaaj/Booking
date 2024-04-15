using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        List<Accommodation> GetAll();
        Accommodation Save(Accommodation accommodation);
        int NextId();
        void Delete(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
        List<Accommodation> GetByUser(User user);
        public Accommodation GetById(int id);
    }
}
