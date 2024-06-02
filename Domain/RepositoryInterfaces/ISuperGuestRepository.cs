using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ISuperGuestRepository
    {
        List<SuperGuest> GetAll();
        SuperGuest Save(SuperGuest superGuest);
        void Delete(SuperGuest superGuest);
        SuperGuest Update(SuperGuest superGuest);
        SuperGuest GetById(int id);
        // Dodaj metode prema potrebi
    }
}
