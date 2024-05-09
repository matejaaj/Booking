using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRepository
    {
        List<Renovation> GetAll();
        Renovation Save(Renovation renovation); 
        void Delete(Renovation renovation);
        Renovation Update(Renovation renovation);   
    }
}
