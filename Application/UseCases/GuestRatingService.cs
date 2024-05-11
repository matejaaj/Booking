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
    public class GuestRatingService
    {
        private readonly IGuestRatingRepository _guestRatingRepository;

        public GuestRatingService(IGuestRatingRepository guestRatingRepository)
        {
            _guestRatingRepository = guestRatingRepository;
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatingRepository.GetAll();
        }

        public GuestRating Save(GuestRating guestRating)
        {
            return _guestRatingRepository.Save(guestRating);
        }

        public void Delete(GuestRating guestRating)
        {
            _guestRatingRepository.Delete(guestRating);
        }

        public GuestRating Update(GuestRating guestRating)
        {
            return _guestRatingRepository.Update(guestRating);
        }
    }
}
