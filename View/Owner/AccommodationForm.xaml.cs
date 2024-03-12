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
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationForm.xaml
    /// </summary>
    public partial class AccommodationForm : Window
    {
        public User LoggedInOwner { get; set; }

        private readonly AccommodationRepository _repository;
        private readonly LocationRepository _locationRepository;
        public List<Location> locations { get; set; }

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

        public AccommodationForm(User owner)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInOwner = owner;
            _repository = new AccommodationRepository();
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
            Accommodation newAccommodation = new Accommodation(AccommodationName, SelectedLocation.locationId, Type.ToUpper(), MaxGuests, MinReservations, CancelThershold, LoggedInOwner);
            Accommodation savedAccommodation = _repository.Save(newAccommodation);
            OwnerOverview.Accommodations.Add(savedAccommodation);
      

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(txtName.Text) &&
                   !string.IsNullOrWhiteSpace(txtMinimumReservationDays.Text) &&
                   cmbLocation.SelectedItem != null &&
                   cmbType.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtCapacity.Text) &&
                   !string.IsNullOrWhiteSpace(txtCancelThreshold.Text);
        }
    }
}
