using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestRatingRepository
    {
        List<GuestRating> GetAll();
        GuestRating Save(GuestRating guestRating);
        void Delete(GuestRating guestRating);
        GuestRating Update(GuestRating guestRating);
    }
}
