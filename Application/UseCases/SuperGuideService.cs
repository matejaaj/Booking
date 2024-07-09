using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;

namespace BookingApp.Application.UseCases
{
    public class SuperGuideService
    {

        private readonly ISuperGuideRepository _superGuideRepository;

        public SuperGuideService(ISuperGuideRepository superGuideRepository)
        {
            _superGuideRepository = superGuideRepository;
        }

        public List<SuperGuide> GetAll()
        {
            return _superGuideRepository.GetAll();
        }

        public SuperGuide Save(SuperGuide superGuide)
        {
            return _superGuideRepository.Save(superGuide);
        }

        public void Delete(SuperGuide superGuide)
        {
            _superGuideRepository.Delete(superGuide);
        }

        public SuperGuide Update(SuperGuide superGuide)
        {
            return _superGuideRepository.Update(superGuide);
        }

        public SuperGuide CheckIsSuperGuide(int guideId, int languageId)
        {
            return GetAll().FirstOrDefault(g => g.GuideId == guideId && g.LanguageId == languageId);
        }

    }
}
