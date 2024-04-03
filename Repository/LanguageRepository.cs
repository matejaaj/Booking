using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class LanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.csv";
        private readonly Serializer<Language> _serializer;
        private List<Language> _languages;

        public LanguageRepository()
        {
            _serializer = new Serializer<Language>();
            _languages = new List<Language>();
        }

        public List<Language> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Language GetById(int id)
        {
            _languages = _serializer.FromCSV(FilePath); 
            return _languages.FirstOrDefault(l => l.languageId == id);
        }

        public Language Save(Language language)
        {
            language.languageId = NextId();
            _languages = _serializer.FromCSV(FilePath);
            _languages.Add(language);
            _serializer.ToCSV(FilePath, _languages);
            return language;
        }

        public int NextId()
        {
            _languages = _serializer.FromCSV(FilePath);
            if(_languages.Count < 1)
            {
                return 1;
            }
            return _languages.Max(l => l.languageId) + 1;
        }
        public void Delete(Language language) 
        {
            _languages = _serializer.FromCSV(FilePath);
            Language founded = _languages.Find(l => l.languageId == language.languageId);
            _languages.Remove(founded);
            _serializer.ToCSV(FilePath, _languages);
        
        }

        public Language Update(Language language)
        {
            _languages = _serializer.FromCSV(FilePath);
            Language current = _languages.Find(l => l.languageId == language.languageId);
            int index = _languages.IndexOf(current);
            _languages.Remove(current);
            _languages.Insert(index, language);
            _serializer.ToCSV(FilePath, _languages);
            return language;
        }

    }
}
