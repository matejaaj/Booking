using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVacationStatusRepository
    {
        List<VacationStatus> GetAll();
        VacationStatus GetById(int id);
        VacationStatus Update(VacationStatus vacationStatus);
        void Delete(int id);
        VacationStatus Save(VacationStatus vacationStatus);

    }
}
