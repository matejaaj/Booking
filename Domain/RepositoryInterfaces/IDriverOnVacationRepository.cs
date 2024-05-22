using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IDriverOnVacationRepository
    {
        List<DriverOnVacation> GetAll();
        DriverOnVacation GetById(int id);
        DriverOnVacation Save(DriverOnVacation vacation);
        DriverOnVacation Update(DriverOnVacation vacation);
        void Delete(int id);
    }
}
