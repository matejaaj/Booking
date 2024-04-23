using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Driver;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using AddImage = BookingApp.WPF.View.Driver.AddImage;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class VehicleViewModel: INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }
        public event EventHandler VehicleAdded;

        private readonly VehicleRepository _repository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly ImageRepository _imageRepository;

        public List<Domain.Model.Image> images { get; set; }
        public ObservableCollection<Location> locations { get; set; }
        public ObservableCollection<Language> languages { get; set; }

        private int _vehicleId;
        private int _userId;


        private ObservableCollection<Location> _selectedLocations = new ObservableCollection<Location>();
        public ObservableCollection<Location> SelectedLocations
        {
            get => _selectedLocations;
            set
            {

                _selectedLocations = value;
                OnPropertyChanged(nameof(SelectedLocations));

            }
        }



        private int _maxPassengers;
        public int MaxPassengers
        {
            get => _maxPassengers;
            set
            {
                if (value != _maxPassengers)
                {
                    _maxPassengers = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Language> _selectedLanguages = new ObservableCollection<Language>();
        public ObservableCollection<Language> SelectedLanguages
        {
            get => _selectedLanguages;
            set
            {
                _selectedLanguages = value;
                OnPropertyChanged(nameof(SelectedLanguages));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public VehicleViewModel(int userId)
        {   
            _userId = userId;
            _repository = new VehicleRepository();
            _languageRepository = new LanguageRepository();
            languages = new ObservableCollection<Language>(_languageRepository.GetAll());
            _locationRepository = new LocationRepository();
            locations = new ObservableCollection<Location>(_locationRepository.GetAll());
            _vehicleId = _repository.NextId();
            _imageRepository = new ImageRepository();
            images = new List<Domain.Model.Image>();
        }

        public void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            var lista = SelectedLanguages.Select(l => l.languageId).ToList();
            var lokacija = SelectedLocations.Select(loc => loc.Id).ToList();

            images.ForEach(img => _imageRepository.Save(img));

            var newVehicle = new Vehicle(lokacija, MaxPassengers, lista, _userId);
            var savedVehicle = _repository.Save(newVehicle);
            DriverOverview.VM.Vehicles.Add(savedVehicle);
            VehicleAdded?.Invoke(this, EventArgs.Empty);
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        public void btnAddImage_Click(object sender, RoutedEventArgs e, Window owner)
        {
            AddImage addImageWindow = new AddImage(images, _vehicleId, ImageResourceType.VEHICLE);
            addImageWindow.ShowDialog();
        }

        public void btnShowImages_Click(object sender, RoutedEventArgs e, Window owner)
        {
            ShowImages showImagesWindow = new ShowImages(images);
            showImagesWindow.Owner = owner;
            showImagesWindow.ShowDialog();
        }

        public bool ValidateInputs()
        {
            if (MaxPassengers == null || SelectedLanguages.Count == 0 || SelectedLocations.Count == 0 || MaxPassengers <= 0)
            {
                MessageBox.Show("Please ensure all fields are correctly filled and at least one language and location are selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
