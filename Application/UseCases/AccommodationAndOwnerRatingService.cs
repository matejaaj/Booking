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
    public class AccommodationAndOwnerRatingService
    {
        private readonly IAccommodationAndOwnerRatingRepository _ratingRepository;

        public AccommodationAndOwnerRatingService()
        {
            _ratingRepository = Injector.CreateInstance<IAccommodationAndOwnerRatingRepository>();
        }

        public List<AccommodationAndOwnerRating> GetAll()
        {
            return _ratingRepository.GetAll();
        }

        public AccommodationAndOwnerRating Save(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
            return _ratingRepository.Save(accommodationAndOwnerRating);
        }

        public void Delete(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
            _ratingRepository.Delete(accommodationAndOwnerRating);
        }

        public AccommodationAndOwnerRating Update(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
           return _ratingRepository.Update(accommodationAndOwnerRating);
        }

        public AccommodationAndOwnerRating GetById(int id)
        {
           return _ratingRepository.GetById(id);
        }

        public AccommodationAndOwnerRating GetByReservationId(int id)
        {
            return _ratingRepository.GetByReservationId(id);
        }

        public List<AccommodationAndOwnerRating> GetByReservationIds(List<int> accommodationReservationIds)
        {
            return _ratingRepository.GetByReservationIds(accommodationReservationIds);
        }
    }
}
