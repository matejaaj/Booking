using BookingApp.Application.UseCases;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestSegmentViewModel : INotifyPropertyChanged
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

        public TourRequestSegmentViewModel(LocationService location, ObservableCollection<KeyValuePair<int, string>> countries, ObservableCollection<KeyValuePair<int, string>> languages)
        {
            _locationService = location;
            _countries = countries;
            _languages = languages;


            _fromDate = DateTime.Now;
            _toDate = DateTime.Now;


            _selectedCountry = new KeyValuePair<int, string>(0, string.Empty);
            _selectedCity = new KeyValuePair<int, string>(0, string.Empty);
            _selectedLanguage = new KeyValuePair<int, string>(0, string.Empty);

        }



        private void GenerateGuestInputs(int numberOfPeople)
        {
            GuestInputs.Clear();
            for (int i = 0; i < numberOfPeople; i++)
            {
                GuestInputs.Add(new TourGuestInputViewModel());
            }
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
    }
}
