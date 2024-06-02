using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVacationTypeRepository
    {
        List<VacationType> GetAll();
        VacationType Save(VacationType vacationType);
        VacationType GetById(int id);
        void Delete(int id);
        VacationType Update(VacationType vacationType);

    }
}
