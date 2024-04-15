using Accessibility;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class OwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService()
        {
            _ownerRepository = Injector.CreateInstance<IOwnerRepository>();
        }

        public Owner GetByUsername(string username)
        {
            return _ownerRepository.GetByUsername(username);
        }

        public Owner Update(Owner owner)
        {
            return _ownerRepository.Update(owner);
        }

        public List<Owner> GetByIds(List<int> ids)
        {
            return _ownerRepository.GetByIds(ids);
        }
        public List<Owner> GetAll()
        {
            return _ownerRepository.GetAll();
        }

        public Owner GetById(int id)
        {
            return _ownerRepository.GetById(id);
        }
    }
}
