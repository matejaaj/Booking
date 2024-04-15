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
    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationService()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }

        public int NextId()
        {
            return _accommodationRepository.NextId();
        }

        public Accommodation Save(Accommodation accommodation)
        {
            return _accommodationRepository.Save(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return _accommodationRepository.Update(accommodation);
        }

        public List<Accommodation> GetByUser(User user)
        {
            return _accommodationRepository.GetByUser(user);
        }
        public Accommodation GetById(int id)
        {
            return _accommodationRepository.GetById(id);
        }
    }
}
