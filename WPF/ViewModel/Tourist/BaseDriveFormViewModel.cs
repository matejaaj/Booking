using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using System.Text.RegularExpressions;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class BaseDriveFormViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<KeyValuePair<int, string>> _countries = new ObservableCollection<KeyValuePair<int, string>>();
        private ObservableCollection<KeyValuePair<int, string>> _cities = new ObservableCollection<KeyValuePair<int, string>>();
        private ObservableCollection<string> _hours = new ObservableCollection<string>();
        private ObservableCollection<string> _minutes = new ObservableCollection<string>();

        private KeyValuePair<int, string> _selectedCountry;
        private KeyValuePair<int, string> _selectedCity;


        private string _startAddress;
        private string _endAddress;

        private string _selectedHour;
        private string _selectedMinute;

        private DateTime _selectedDate = DateTime.Today;

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

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }
        public string EndAddress
        {
            get { return _endAddress; }
            set
            {
                if (_endAddress != value)
                {
                    _endAddress = value;
                    OnPropertyChanged(nameof(EndAddress));
                }
            }
        }
        public string StartAddress
        {
            get { return _startAddress; }
            set
            {
                if (_startAddress != value)
                {
                    _startAddress = value;
                    OnPropertyChanged(nameof(StartAddress));
                }
            }
        }
        public ObservableCollection<string> Hours
        {
            get { return _hours; }
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    OnPropertyChanged(nameof(Hours));
                }
            }
        }
        public ObservableCollection<string> Minutes
        {
            get { return _minutes; }
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    OnPropertyChanged(nameof(Minutes));
                }
            }
        }
        public string SelectedHour
        {
            get { return _selectedHour; }
            set
            {
                _selectedHour = value;
                OnPropertyChanged(nameof(SelectedHour));
            }
        }
        public string SelectedMinute
        {
            get { return _selectedMinute; }
            set
            {
                _selectedMinute = value;
                OnPropertyChanged(nameof(SelectedMinute));
            }
        }


        public void ValidateHour(string input)
        {
            if (!Regex.IsMatch(input, @"^[0-2][0-9]$"))
            {
                SelectedHour = string.Empty;
                OnPropertyChanged(nameof(SelectedHour));
            }
        }

        public void ValidateMinute(string input)
        {
            if (!Regex.IsMatch(input, @"^[0-5][0-9]$"))
            {
                SelectedMinute = string.Empty;
                OnPropertyChanged(nameof(SelectedMinute));
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

        public BaseDriveFormViewModel()
        {


            InitializeFields();
            FillCountries();
            FillHours();
            FillMinutes();
        }

        protected void InitializeFields()
        {
            _countries = new ObservableCollection<KeyValuePair<int, string>>();
            _cities = new ObservableCollection<KeyValuePair<int, string>>();
            _hours = new ObservableCollection<string>();
            _minutes = new ObservableCollection<string>();
            _selectedDate = DateTime.Today;
        }

        protected void FillCountries()
        {
            LocationService _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            var countries = _locationService.GetAllCountries();
            Countries.Clear();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }

        public void FillCities()
        {
            LocationService _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            var cities = _locationService.GetCitiesByCountry(SelectedCountry.Value).ToList();
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        protected void FillHours()
        {
            Hours = new ObservableCollection<string>(Enumerable.Range(0, 24).Select(i => i.ToString("00")));
        }

        protected void FillMinutes()
        {
            Minutes = new ObservableCollection<string>(Enumerable.Range(0, 59).Select(i => i.ToString("00")));
        }

        public DateTime CreateDateTimeFromSelections()
        {
            DateTime date;
            if (SelectedMinute != string.Empty &&
                SelectedHour != string.Empty &&
                SelectedDate != null)
            {

                var hour = int.Parse(SelectedHour);
                var minute = int.Parse(SelectedMinute);

                date = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, hour, minute, 0);
                return date;
            }

            return DateTime.MinValue;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
