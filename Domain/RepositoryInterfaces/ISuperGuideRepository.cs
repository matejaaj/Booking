using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ISuperGuideRepository
    {
        List<SuperGuide> GetAll();
        SuperGuide Save(SuperGuide superGuide);
        void Delete(SuperGuide superGuide);
        SuperGuide Update(SuperGuide superGuide);
        SuperGuide GetById(int id);

    }
}
