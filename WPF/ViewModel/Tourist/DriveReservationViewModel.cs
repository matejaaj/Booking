using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveReservationViewModel : INotifyPropertyChanged
    {
        public User Tourist;
        public int PickupLocationId { get; set; }
        public int DetailedPickupLocationId { get; set; }
        public int DetailedDropoffLocationId { get; set; }
        public int SelectedDriverId { get; set; }

        private List<string> _countries;
        private List<string> _hours;
        private List<string> _minutes;
        private List<string> _cities;
        private string _startAddress;
        private string _endAddress;
        private string _selectedHour;
        private string _selectedMinute;
        private string _selectedCountry;
        private string _selectedCity;
        private DateTime _selectedDate;

        private List<string> _drivers;

        public List<string> Drivers
        {
            get { return _drivers ?? (_drivers = new List<string>()); }
            set
            {
                if (_drivers != value)
                {
                    _drivers = value;
                    OnPropertyChanged(nameof(Drivers));
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
        public List<string> Cities
        {
            get { return _cities ?? (_cities = new List<string>()); }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }
        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }
        public List<string> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }
        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }
        public List<string> Hours
        {
            get { return _hours ?? (_hours = new List<string>()); }
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
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
        public List<string> Minutes
        {
            get { return _minutes ?? (_minutes = new List<string>()); }
            set
            {
                _minutes = value;
                OnPropertyChanged(nameof(Minutes));
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




        private VehicleService _vehicleService;
        private DetailedLocationService _detailedLocationService;
        private LocationService _locationService;
        private DriveReservationService _driveReservationService;
        private UserService _userService;


        public DriveReservationViewModel(User loggedUser)
        {
            Tourist = loggedUser;
            InitializeServices();
            FillInitialComboBoxValues();

        }
        public void InitializeServices()
        {
            _vehicleService = new VehicleService();
            _detailedLocationService = new DetailedLocationService();
            _locationService = new LocationService();
            _driveReservationService = new DriveReservationService();
            _userService = new UserService();
        }
        private void FillInitialComboBoxValues()
        {
            InitializeFields();
            FillCountries();
            FillHours();
            FillMinutes();
        }
        private void InitializeFields()
        {
            _countries = new List<string>();
            _hours = new List<string>();
            _minutes = new List<string>();
            _cities = new List<string>();
        }
        private void FillCountries()
        {
            List<Location> locations = _locationService.GetAll();
            var countries = locations.Select(loc => loc.Country).Distinct().OrderBy(c => c).ToList();

            Countries.Clear();
            foreach (var country in countries)
            {
                Countries.Add(country);
            }
        }
        private void FillHours()
        {
            Hours = Enumerable.Range(0, 24).Select(i => i.ToString("00")).ToList();
        }
        private void FillMinutes()
        {
            Minutes = new List<string> { "00", "15", "30", "45" };
        }

        public void FillCities()
        {
            Cities.Clear();
            Cities = _locationService.GetCityByCountry(SelectedCountry);
        }

        public void UpdateDriverList()
        {
            var locationId = _locationService.GetLocationIdByCity(SelectedCity);
            DateTime? date = CreateDateTimeFromSelections();

            List<int> drivers = _vehicleService.GetDriverIdsByLocationId(locationId);
            drivers = _driveReservationService.FilterAvailableDrivers(drivers, date);
            FillDrivers(drivers);
      
        }

        private void FillDrivers(List<int> driversIds)
        {
            var drivers = _userService.GetByIds(driversIds);
            Drivers.Clear();
            foreach (var driver in drivers)
            {
                Drivers.Add(driver.Username);
            }
        }

        public void Reserve()
        {
            DateTime departure = CreateDateTimeFromSelections();
            DriveReservation reservation = new DriveReservation();
        }

        private DateTime CreateDateTimeFromSelections()
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
