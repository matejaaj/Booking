using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.View.Guide;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class AccommodationFormViewModel : INotifyPropertyChanged
    {
        private Window _ownerWindow;
        public Domain.Model.Owner LoggedInOwner { get; set; }

        private readonly AccommodationService _accommodationService;
        private readonly LocationService _locationService;
        private readonly ImageService _imageService;
        public List<Location> locations { get; set; }
        public List<Domain.Model.Image> images { get; set; }

        private string _name;
        public string AccommodationName
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if (_selectedLocation != value)
                {
                    _selectedLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _locationId;
        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value != _locationId)
                {
                    _locationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _type = "House";
        public string Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
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

        private string _picture;
        public string Picture
        {
            get => _picture;
            set
            {
                if (value != _picture)
                {
                    _picture = value;
                    OnPropertyChanged(nameof(Picture));
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _minReservations;
        public int MinReservations
        {
            get => _minReservations;
            set
            {
                if (value != _minReservations)
                {
                    _minReservations = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _cancelThershold;
        public int CancelThershold
        {
            get => _cancelThershold;
            set
            {
                if (value != _cancelThershold)
                {
                    _cancelThershold = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _accommodationId;
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddImagesCommand { get; }
        public ICommand ShowImagesCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationFormViewModel(Domain.Model.Owner owner, Window ownerWindow)
        {
            LoggedInOwner = owner;
            Pictures = new ObservableCollection<string>();
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            locations = _locationService.GetAll();
            SelectedLocation = locations[0];
            images = new List<Domain.Model.Image>();
            _accommodationId = _accommodationService.NextId();
            _ownerWindow = ownerWindow;

            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
            AddImagesCommand = new RelayCommand(AddImages);
            ShowImagesCommand = new RelayCommand(ShowImages);
        }

        private void Confirm(object obj)
        {
            Accommodation newAccommodation = new Accommodation(AccommodationName, SelectedLocation.Id, Type.ToUpper(), MaxGuests, MinReservations, CancelThershold, LoggedInOwner.Id);
            List<Image> images = _imageService.GetImagesByEntityAndType(newAccommodation.AccommodationId, ImageResourceType.ACCOMMODATION);
            var imageIds = images?.Select(i => i.Id).ToList() ?? new List<int>();
            var imagePaths = images?.Select(i => i.Path).ToList() ?? new List<string>();
            var location = _locationService.GetLocationById(newAccommodation.LocationId);
            newAccommodation.ImageIds = imageIds;
            Accommodation savedAccommodation = _accommodationService.Save(newAccommodation);

            _ownerWindow.Close();
        }

        private void Cancel(object obj)
        {
            _ownerWindow.Close();
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
                    Domain.Model.Image newImage = new Domain.Model.Image(destinationFilePath, _accommodationId, ImageResourceType.ACCOMMODATION, LoggedInOwner.Id);
                    if (_imageService.GetAll().Find(i => i.Path == newImage.Path) == null)
                    {
                        _imageService.Save(newImage);
                    }
                    //File.Copy(file, destinationFilePath);
                    Pictures.Add(file);
                }

               
            }
        }

        private void ShowImages(object obj)
        {
            throw new NotImplementedException();
        }
    }

}
