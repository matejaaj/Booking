using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.Driver
{
    /// <summary>
    /// Interaction logic for VehicleForm.xaml
    /// </summary>
    public partial class VehicleForm : Window
    {
        public User LoggedInUser { get; set; }

        private readonly VehicleRepository _repository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        public List<Location> locations { get; set; }
        public List<Language> languages { get; set; }



        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if(_selectedLocation != value)
                {
                    _selectedLocation = value;
                    OnPropertyChanged();
                }
            }


        }

       

        private int _maxPassengers;
        public int MaxPassengers
        {
            get => _maxPassengers;
            set
            {
                if(value != _maxPassengers)
                {
                    _maxPassengers = value;
                    OnPropertyChanged();
                }
            }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        public VehicleForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new VehicleRepository();
            _languageRepository = new LanguageRepository();
            languages = _languageRepository.GetAll();
            _locationRepository = new LocationRepository();
            locations = _locationRepository.GetAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Vehicle newVehicle = new Vehicle(SelectedLocation.locationId, MaxPassengers,SelectedLanguage.languageId);
            Vehicle savedVehicle = _repository.Save(newVehicle);
            DriverOverview.Vehicles.Add(savedVehicle);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
