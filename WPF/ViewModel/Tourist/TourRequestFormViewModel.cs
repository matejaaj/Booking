using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestFormViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourRequestSegmentViewModel> TourSegments { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> Countries { get; private set; }
        public ObservableCollection<KeyValuePair<int, string>> Languages { get; private set; }

        private LocationService _locationService;
        private LanguageService _languageService;

        public TourRequestFormViewModel(LocationService locationService, LanguageService languageService)
        {
            _locationService = locationService;
            _languageService = languageService;

            Countries = new ObservableCollection<KeyValuePair<int, string>>();
            Languages = new ObservableCollection<KeyValuePair<int, string>>();

            FillCountries();
            FillLanguages();

            TourSegments = new ObservableCollection<TourRequestSegmentViewModel>();
            AddSegment();
        }

        private void FillCountries()
        {
            LocationService _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            var countries = _locationService.GetAllCountries();
            Countries.Clear();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }

        private void FillLanguages()
        {
            var languages = _languageService.GetAll();
            Languages.Clear();
            foreach (var language in languages)
            {
                Languages.Add(new KeyValuePair<int, string>(language.Id, language.Name));
            }
        }


        public void AddSegment()
        {
            foreach (var segment in TourSegments)
            {
                segment.IsExpanded = false;
            }

            var newSegment = new TourRequestSegmentViewModel(_locationService, Countries, Languages);
            newSegment.IsExpanded = true;  
            TourSegments.Add(newSegment);
        }


        public void RemoveSegment(TourRequestSegmentViewModel segment)
        {
            if (TourSegments.Count > 1)
            {
                TourSegments.Remove(segment);
            }
        }


        public void Submit()
        {
            
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
