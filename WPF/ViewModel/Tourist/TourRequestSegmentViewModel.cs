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
using System.Windows.Input;
using BookingApp.Commands;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestSegmentViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private LocationService _locationService;

        private ObservableCollection<KeyValuePair<int, string>> _countries = new ObservableCollection<KeyValuePair<int, string>>();
        private ObservableCollection<KeyValuePair<int, string>> _cities = new ObservableCollection<KeyValuePair<int, string>>();
        private ObservableCollection<KeyValuePair<int, string>> _languages = new ObservableCollection<KeyValuePair<int, string>>();

        public ObservableCollection<TourGuestInputViewModel> GuestInputs { get; } = new ObservableCollection<TourGuestInputViewModel>();

        private KeyValuePair<int, string> _selectedCountry;
        private KeyValuePair<int, string> _selectedCity;
        private KeyValuePair<int, string> _selectedLanguage;
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _description;
        private int _numberOfPeople;

        public ObservableCollection<KeyValuePair<int, string>> Countries
        {
            get { return _countries; }
            set
            {
                if (_countries != value)
                {
                    _countries = value;
                    OnPropertyChanged(nameof(Countries));
                }
            }
        }

        public KeyValuePair<int, string> SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                if (!Equals(_selectedCountry, value))
                {
                    _selectedCountry = value;
                    FillCities();
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> Cities
        {
            get { return _cities; }
            set
            {
                if (_cities != value)
                {
                    _cities = value;
                    OnPropertyChanged(nameof(Cities));
                    FillCities();
                }
            }
        }

        public KeyValuePair<int, string> SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (!Equals(_selectedCity, value))
                {
                    _selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }
        }

        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                if (_fromDate != value)
                {
                    _fromDate = value;
                    OnPropertyChanged(nameof(FromDate));
                    OnPropertyChanged(nameof(ToDate));
                }
            }
        }

        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                if (_toDate != value)
                {
                    _toDate = value;
                    OnPropertyChanged(nameof(ToDate));
                    OnPropertyChanged(nameof(FromDate));
                }
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> Languages
        {
            get => _languages;
            set
            {
                if (_languages != value)
                {
                    _languages = value;
                    OnPropertyChanged(nameof(Languages));
                }
            }
        }

        public KeyValuePair<int, string> SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (!Equals(_selectedLanguage, value))
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public int NumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                if (_numberOfPeople != value)
                {
                    _numberOfPeople = value;
                    GenerateGuestInputs(value);
                    OnPropertyChanged(nameof(NumberOfPeople));
                }
            }
        }

        private bool _isExpanded;

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        public void ValidateCountry(string input)
        {
            if (!Countries.Any(c => c.Value == input))
            {
                SelectedCountry = new KeyValuePair<int, string>();
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        public void ValidateCity(string input)
        {
            if (!Cities.Any(c => c.Value == input))
            {
                SelectedCity = new KeyValuePair<int, string>();
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        public TourRequestSegmentViewModel(LocationService location, ObservableCollection<KeyValuePair<int, string>> countries, ObservableCollection<KeyValuePair<int, string>> languages)
        {
            _locationService = location;
            _countries = countries;
            _languages = languages;

            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;

            _selectedCountry = new KeyValuePair<int, string>();
            _selectedCity = new KeyValuePair<int, string>();
            SelectedLanguage = new KeyValuePair<int, string>(-1, string.Empty);
        }

        private void GenerateGuestInputs(int numberOfPeople)
        {
            GuestInputs.Clear();
            for (int i = 0; i < numberOfPeople; i++)
            {
                GuestInputs.Add(new TourGuestInputViewModel());
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(this[nameof(SelectedCountry)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(SelectedCity)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(SelectedLanguage)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(Description)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(FromDate)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(ToDate)]) &&
                   string.IsNullOrWhiteSpace(this[nameof(NumberOfPeople)]) &&
                   GuestInputs.All(guest => string.IsNullOrWhiteSpace(guest.Error)) &&
                   FromDate <= ToDate;
        }

        public void FillCities()
        {
            var cities = _locationService.GetCitiesByCountry(SelectedCountry.Value).ToList();
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
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
                    case nameof(SelectedCountry):
                        if (SelectedCountry.Key == 0)
                            result = TranslationSource.Instance["ValidationCountry"];
                        break;
                    case nameof(SelectedCity):
                        if (SelectedCity.Key == 0)
                            result = TranslationSource.Instance["ValidationCity"];
                        break;
                    case nameof(SelectedLanguage):
                        if (SelectedLanguage.Key == -1)
                            result = TranslationSource.Instance["ValidationLanguage"];
                        break;
                    case nameof(Description):
                        if (string.IsNullOrWhiteSpace(Description))
                            result = TranslationSource.Instance["ValidationDescription"];
                        break;
                    case nameof(FromDate):
                        if (FromDate < DateTime.Today)
                            result = TranslationSource.Instance["ValidationDate"];
                        break;
                    case nameof(ToDate):
                        if (ToDate < FromDate)
                            result = TranslationSource.Instance["ValidationDateBefore"];
                        break;
                    case nameof(NumberOfPeople):
                        if (NumberOfPeople <= 0)
                            result = TranslationSource.Instance["ValidationNumberOfPeople"];
                        break;
                }
                return result;
            }
        }
    }


}
