using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    class VacationTypeService
    {
        private readonly IVacationTypeRepository repository;

        public VacationTypeService()
        {
            repository = Injector.CreateInstance<IVacationTypeRepository>();
        }

        public List<VacationType> GetAll()
        {
            return repository.GetAll();
        }

        public VacationType Get(int id)
        {
            return repository.GetById(id);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public VacationType Save(VacationType vacationType)
        {
            return repository.Save(vacationType);
        }

        public VacationType Update(VacationType vacationType)
        {
            return repository.Update(vacationType);
        }
    }
}
