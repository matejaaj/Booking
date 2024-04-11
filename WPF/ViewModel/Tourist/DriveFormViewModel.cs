using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveFormViewModel
    {
        private ObservableCollection<KeyValuePair<int, string>> _countries;
        private ObservableCollection<KeyValuePair<int, string>> _cities;
        private ObservableCollection<KeyValuePair<int, string>> _drivers;
        private ObservableCollection<string> _hours;
        private ObservableCollection<string> _minutes;

        private KeyValuePair<int, string> _selectedCountry;
        private KeyValuePair<int, string> _selectedCity;
        private KeyValuePair<int, string> _selectedDriver;

        private string _startAddress;
        private string _endAddress;

        private string _selectedHour;
        private string _selectedMinute;
        private DateTime _selectedDate;

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
        public ObservableCollection<KeyValuePair<int, string>> Drivers
        {
            get { return _drivers ?? (_drivers = new ObservableCollection<KeyValuePair<int, string>>()); }
            set
            {
                _drivers = value;
                OnPropertyChanged(nameof(Drivers));
            }
        }
        public KeyValuePair<int, string> SelectedDriver
        {
            get { return _selectedDriver; }
            set
            {
                if (!Equals(_selectedDriver, value))
                {
                    _selectedDriver = value;
                    OnPropertyChanged(nameof(SelectedDriver));
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


        public DriveFormViewModel()
        {
            InitializeFields();
            FillCountries();
            FillHours();
            FillMinutes();
        }



        private void InitializeFields()
        {
            _countries = new ObservableCollection<KeyValuePair<int, string>>();
            _cities = new ObservableCollection<KeyValuePair<int, string>>();
            _drivers = new ObservableCollection<KeyValuePair<int, string>>();
            _hours = new ObservableCollection<string>();
            _minutes = new ObservableCollection<string>();
            _selectedDate = DateTime.Today;
        }


        private void FillCountries()
        {
            LocationService _locationService = new LocationService();
            List<Location> locations = _locationService.GetAll();
            var countries = locations
                .GroupBy(loc => loc.Country)
                .Select(grp => grp.First())
                .Select(loc => new KeyValuePair<int, string>(loc.Id, loc.Country))
                .OrderBy(c => c.Value)
                .ToList();

            Countries.Clear();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
        private void FillHours()
        {
            Hours = new ObservableCollection<string>(Enumerable.Range(0, 24).Select(i => i.ToString("00")));
        }

        private void FillMinutes()
        {
            Minutes = new ObservableCollection<string>(new List<string> { "00", "15", "30", "45" });
        }

        public DateTime CreateDateTimeFromSelections()
        {
            if (SelectedMinute != null &&
                SelectedHour != null &&
                SelectedDate != null)
            {

                var hour = int.Parse(SelectedHour);
                var minute = int.Parse(SelectedMinute);

                return new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, hour, minute, 0);
            }
            else
            {
                MessageBox.Show("Please select a valid date and time.");
                return DateTime.MinValue;
            }
        }

        public void FillDrivers(List<int> driverIds)
        {
            UserService _userService = new UserService();
            var drivers = _userService.GetByIds(driverIds);
            Drivers.Clear();
            foreach (var driver in drivers)
            {
                Drivers.Add(new KeyValuePair<int, string>(driver.Id, driver.Username));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
