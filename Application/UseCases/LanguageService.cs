using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class LanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository language)
        {
            _languageRepository = language;
        }

        public LanguageService()
        {
            _languageRepository = Injector.CreateInstance<ILanguageRepository>();
        }

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public List<Language> GetAll()
        {
            return _languageRepository.GetAll();
        }

        public Language GetById(int id)
        {
            return _languageRepository.GetById(id);
        }

        public Language Save(Language language)
        {
            return _languageRepository.Save(language);
        }

        public void Delete(Language language)
        {
            _languageRepository.Delete(language);
        }

        public Language Update(Language language)
        {
            return _languageRepository.Update(language);
        }
    }

}
