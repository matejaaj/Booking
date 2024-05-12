using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.View.Guide;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class AccommodationFormViewModel : INotifyPropertyChanged
    {
        private Window _ownerWindow;
        public Domain.Model.Owner LoggedInOwner { get; set; }

        private readonly AccommodationService _accommodationService;
        private readonly LocationService _locationService;
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

        private string _type;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationFormViewModel(Domain.Model.Owner owner, Window ownerWindow)
        {
            LoggedInOwner = owner;
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            locations = _locationService.GetAll();
            images = new List<Domain.Model.Image>();
            _accommodationId = _accommodationService.NextId();
            _ownerWindow = ownerWindow;
        }

        public void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
                Accommodation newAccommodation = new Accommodation(AccommodationName, SelectedLocation.Id, Type.ToUpper(), MaxGuests, MinReservations, CancelThershold, LoggedInOwner.Id);
                Accommodation savedAccommodation = _accommodationService.Save(newAccommodation);
                OwnerOverviewViewModel.Accommodations.Add(savedAccommodation);
        }

        public void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(images, _accommodationId, ImageResourceType.ACCOMMODATION);
            addImageWindow.Owner = _ownerWindow;
            addImageWindow.ShowDialog();
        }

        public void btnShowImages_Click(object sender, RoutedEventArgs e)
        {
            //ShowImages showImagesWindow = new ShowImages(images);
           // showImagesWindow.Owner = _ownerWindow;
//showImagesWindow.ShowDialog();
        }
    }

}
