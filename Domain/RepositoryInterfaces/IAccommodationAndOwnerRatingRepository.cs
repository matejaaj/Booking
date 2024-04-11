using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationAndOwnerRatingRepository
    {
        public List<AccommodationAndOwnerRating> GetAll();
        public AccommodationAndOwnerRating Save(AccommodationAndOwnerRating accommodationAndOwnerRating);
        public void Delete(AccommodationAndOwnerRating accommodationAndOwnerRating);
        public AccommodationAndOwnerRating Update(AccommodationAndOwnerRating accommodationAndOwnerRating);
        public AccommodationAndOwnerRating GetById(int id);
        public AccommodationAndOwnerRating GetByReservationId(int id);
    }
}
