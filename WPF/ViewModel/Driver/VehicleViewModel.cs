using BookingApp.Application.Events;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Driver;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private readonly ImageService _imageService;

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

        private ObservableCollection<string> _pictures;
        public ObservableCollection<string> Pictures
        {
            get => _pictures;
            set
            {
                if (value != _pictures)
                {
                    _pictures = value;
                    OnPropertyChanged(nameof(Pictures));
                }
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
            _imageService = new ImageService();
            Pictures = new ObservableCollection<string>();
        }

        public void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            var lista = SelectedLanguages.Select(l => l.Id).ToList();
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

        public void btnAddImage_Click(object sender, RoutedEventArgs e, Page owner)
        {
            AddImages(sender);   
        }

        public void btnShowImages_Click(object sender, RoutedEventArgs e, Page owner)
        {
            
        }

        public bool ValidateInputs()
        {
            if (SelectedLanguages.Count == 0 || SelectedLocations.Count == 0 || MaxPassengers <= 0)
            {
                MainWindow.EventAggregator.Publish(new ShowMessageEvent("Please ensure all fields are correctly filled and at least one language and location are selected.", "Error"));
                return false;
            }
            return true;
        }

        private void AddImages(object obj)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "Image files (*.jpg;*.jpeg;*.png;*.jfif)|*.jpg;*.jpeg;*.png;*.jfif";
            dlg.Multiselect = true;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string[] selectedFiles = dlg.FileNames;

                string destinationFolder = @"../../../Resources/Images/";

                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                foreach (string file in selectedFiles)
                {
                    string destinationFilePath = Path.Combine(destinationFolder, Path.GetFileName(file));
                    Domain.Model.Image newImage = new Domain.Model.Image(destinationFilePath, _vehicleId, ImageResourceType.VEHICLE, _userId);
                    if (_imageService.GetAll().Find(i => i.Path == newImage.Path) == null)
                    {
                        _imageService.Save(newImage);
                    }
                    //File.Copy(file, destinationFilePath);
                    Pictures.Add(file);
                }
            }
        }
    }

    
}
