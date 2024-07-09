using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    class VacationStatusService
    {
        private readonly IVacationStatusRepository repository;

        public VacationStatusService()
        {
            repository = Injector.CreateInstance<IVacationStatusRepository>();
        }

        public List<VacationStatus> GetAll()
        {
            return repository.GetAll();
        }

        public VacationStatus Get(int id)
        {
            return repository.GetById(id);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public VacationStatus Save(VacationStatus vacationType)
        {
            return repository.Save(vacationType);
        }

        public VacationStatus Update(VacationStatus vacationType)
        {
            return repository.Update(vacationType);
        }
    }
}
