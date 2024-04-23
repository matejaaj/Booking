using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class SuperDriverStateService
    {
        private readonly ISuperDriverStateRepository _repository;

        public SuperDriverStateService()
        {
            _repository = Injector.CreateInstance<ISuperDriverStateRepository>();
        }

        public List<SuperDriverState> GetAll()
        {
            return _repository.GetAll();
        }

        public SuperDriverState? Get(int id)
        {
            return _repository.GetSuperDriverStateById(id);
        }

        public SuperDriverState Update(SuperDriverState newSuperDriverState)
        {
            return _repository.Update(newSuperDriverState);
        }

        public SuperDriverState Save(SuperDriverState newSuperDriverState)
        {
            return _repository.Save(newSuperDriverState);
        }

        public void Delete(SuperDriverState superDriverState)
        {
            _repository.Delete(superDriverState);
        }
    }
}
