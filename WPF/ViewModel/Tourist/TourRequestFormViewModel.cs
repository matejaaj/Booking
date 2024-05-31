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
using BookingApp.Application.UseCases.Factories;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestFormViewModel
    {
        private User _user;
        public ObservableCollection<TourRequestSegmentViewModel> TourSegments { get; set; } =
            new ObservableCollection<TourRequestSegmentViewModel>();

        public ObservableCollection<KeyValuePair<int, string>> Countries { get; private set; } =
            new ObservableCollection<KeyValuePair<int, string>>();
        public ObservableCollection<KeyValuePair<int, string>> Languages { get; private set; } = new ObservableCollection<KeyValuePair<int, string>>();

        private LocationService _locationService;
        private LanguageService _languageService;

        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _tourRequestSegmentService;
        private PrivateTourGuestService _privateTourGuestService;

        

        public TourRequestFormViewModel(User loggedUser, LocationService locationService, LanguageService languageService, TourRequestService tourRequestService, TourRequestSegmentService tourRequestSegmentService, PrivateTourGuestService privateTourGuestService)
        {
            _user =  loggedUser;
            _locationService = locationService;
            _languageService = languageService;
            _tourRequestService = tourRequestService;
            _tourRequestSegmentService = tourRequestSegmentService;
            _privateTourGuestService = privateTourGuestService;



            InitialieFields();
        }

        private void InitialieFields()
        {
            FillCountries();
            FillLanguages();
            AddSegment();
        }

        private void FillCountries()
        {
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
            bool isComplexRequest = TourSegments.Count != 1;
            var request = new TourRequest(_user.Id, isComplexRequest);
            _tourRequestService.Save(request);

            var tourSegments = new List<TourRequestSegment>();

            foreach (var segment in TourSegments)
            {
                var tourSegment = new TourRequestSegment(request.Id, segment.Description, segment.SelectedCity.Key,
                    segment.SelectedLanguage.Key, segment.NumberOfPeople, segment.FromDate, segment.ToDate);

                _tourRequestSegmentService.Save(tourSegment);


                foreach (var guest in segment.GuestInputs)
                {
                    var privateGuest = new PrivateTourGuest(guest.FirstName + " " + guest.LastName, guest.Age, _user.Id,
                        tourSegment.Id);
                    _privateTourGuestService.Save(privateGuest);
                }
            }
        }
    }
}
