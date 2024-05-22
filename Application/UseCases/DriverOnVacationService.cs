using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class DriverOnVacationService
    {
        private readonly IDriverOnVacationRepository repository;
        
        public DriverOnVacationService()
        {
            repository = Injector.CreateInstance<IDriverOnVacationRepository>();
        }

        public DriverOnVacationService(IDriverOnVacationRepository repository)
        {
            this.repository = repository;
        }

        public List<DriverOnVacation> GetAll()
        {
            return repository.GetAll();
        }

        public DriverOnVacation GetById(int id)
        {
            return repository.GetById(id);
        }

        public DriverOnVacation Update(DriverOnVacation driverOnVacation)
        {
            return repository.Update(driverOnVacation);
        }

        public DriverOnVacation Save(DriverOnVacation driverOnVacation)
        {
            return repository.Save(driverOnVacation);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
