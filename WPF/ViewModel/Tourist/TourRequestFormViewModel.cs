using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestFormViewModel : INotifyPropertyChanged, IDataErrorInfo
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

        public RelayCommand AddSegmentCommand { get; private set; }
        public RelayCommand RemoveSegmentCommand { get; private set; }
        public RelayCommand SubmitCommand { get; private set; }

        public TourRequestFormViewModel(User loggedUser, LocationService locationService, LanguageService languageService, TourRequestService tourRequestService, TourRequestSegmentService tourRequestSegmentService, PrivateTourGuestService privateTourGuestService)
        {
            _user = loggedUser;
            _locationService = locationService;
            _languageService = languageService;
            _tourRequestService = tourRequestService;
            _tourRequestSegmentService = tourRequestSegmentService;
            _privateTourGuestService = privateTourGuestService;

            AddSegmentCommand = new RelayCommand(_ => AddSegment());
            RemoveSegmentCommand = new RelayCommand(segment => RemoveSegment((TourRequestSegmentViewModel)segment), segment => TourSegments.Count > 1);
            SubmitCommand = new RelayCommand(Submit, CanSubmit);

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

        public void RemoveSegment(object segment)
        {
            var segmentViewModel = segment as TourRequestSegmentViewModel;
            if (TourSegments.Count > 1 && segmentViewModel != null)
            {
                TourSegments.Remove(segmentViewModel);
            }
        }

        private bool CanSubmit(object parameter)
        {
            foreach (var segment in TourSegments)
            {
                if (!segment.IsValid())
                {
                    return false;
                }
            }
            return true;
        }

        public void Submit(object parameter)
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

                MessageBox.Show(TranslationSource.Instance["RequestSuccess"]);
            }

            if (parameter is Window window)
            {
                window.Close();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case nameof(Countries):
                        if (Countries == null || Countries.Count == 0)
                            result = "Please select a country.";
                        break;
                    case nameof(Languages):
                        if (Languages == null || Languages.Count == 0)
                            result = "Please select a language.";
                        break;
                }
                return result;
            }
        }
    }
}
