using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View.Driver
{
    /// <summary>
    /// Interaction logic for VehicleForm.xaml
    /// </summary>
    public partial class VehicleForm : Window
    {
        public User LoggedInUser { get; set; }
        public event EventHandler VehicleAdded;

        private readonly VehicleRepository _repository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly ImageRepository _imageRepository;

        public List<BookingApp.Model.Image> images { get; set; }
        public ObservableCollection<Location> locations {  get; set; }
        public ObservableCollection<Language> languages { get; set; }

        private int _vehicleId;


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
                if(value != _maxPassengers)
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

        public VehicleForm()
        {
            InitializeComponent();
            _repository = new VehicleRepository();
            _languageRepository = new LanguageRepository();
            languages = new ObservableCollection<Language>(_languageRepository.GetAll());
            _locationRepository = new LocationRepository();
            locations = new ObservableCollection<Location>(_locationRepository.GetAll());
            DataContext = this;

            _vehicleId = _repository.NextId();
            _imageRepository = new ImageRepository();
            images = new List<BookingApp.Model.Image>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLanguages.Count == 0)
            {
                MessageBox.Show("Molimo odaberite barem jedan jezik.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedLocations.Count == 0)
            {
                MessageBox.Show("Molimo odaberite barem jednu lokaciju.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MaxPassengers <= 0)
            {
                MessageBox.Show("Unesite validan maksimalan broj putnika.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<int> lista = new List<int>();
            List<int> lokacija = new List<int>();
            foreach(Language l in SelectedLanguages)
            {
                lista.Add(l.languageId);
            }
            foreach(Location location in SelectedLocations)
            {
                lokacija.Add(location.Id);
            }
            foreach (var img in images)
            {
                _imageRepository.Save(img);
            }
            Vehicle newVehicle = new Vehicle(lokacija, MaxPassengers,lista, LoggedInUser.Id);
            Vehicle savedVehicle = _repository.Save(newVehicle);
            DriverOverview.Vehicles.Add(savedVehicle);
            VehicleAdded?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(images, _vehicleId, ImageResourceType.VEHICLE);
            addImageWindow.Owner = this;
            addImageWindow.ShowDialog();
        }

        private void btnShowImages_Click(object sender, RoutedEventArgs e)
        {
            ShowImages showImagesWindow = new ShowImages(images);
            showImagesWindow.Owner = this;
            showImagesWindow.ShowDialog();
        }
    }
}
