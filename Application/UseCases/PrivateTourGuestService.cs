using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class PrivateTourGuestService
    {
        private readonly IPrivateTourGuestRepository _privateTourGuestRepository;

        public PrivateTourGuestService(IPrivateTourGuestRepository privateTourGuestRepository)
        {
            _privateTourGuestRepository = privateTourGuestRepository;
        }

        public PrivateTourGuestService()
        {
            _privateTourGuestRepository = Injector.CreateInstance<IPrivateTourGuestRepository>();
        }

        public List<PrivateTourGuest> GetAll()
        {
            return _privateTourGuestRepository.GetAll();
        }

        public PrivateTourGuest GetById(int id)
        {
            return _privateTourGuestRepository.GetById(id);
        }

        public PrivateTourGuest Save(PrivateTourGuest privateTourGuest)
        {
            return _privateTourGuestRepository.Save(privateTourGuest);
        }

        public void Delete(PrivateTourGuest privateTourGuest)
        {
            _privateTourGuestRepository.Delete(privateTourGuest);
        }

        public PrivateTourGuest Update(PrivateTourGuest privateTourGuest)
        {
            return _privateTourGuestRepository.Update(privateTourGuest);
        }
    }
}
