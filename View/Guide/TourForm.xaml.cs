using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Owner;
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

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly CheckpointRepository _checkpointRepository;
        private readonly ImageRepository _imageRepository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly TourStartDateRepository _tourStartDateRepository;
        public List<Location> locations { get; set; }
        public List<Language> languages { get; set; }

        public List<BookingApp.Model.Image> images { get; set; }
        public List<TourStartDate> tourStartDates { get; set; }
        public List<Checkpoint> checkpoints { get; set; }

        private int _tourId;



        public TourForm()
        {
            InitializeComponent();
            DataContext = this;
            
            _tourRepository = new TourRepository();
            _checkpointRepository = new CheckpointRepository();
            _imageRepository = new ImageRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();
            _tourStartDateRepository = new TourStartDateRepository();

            languages = new List<Language>();
            languages = _languageRepository.GetAll();
            locations = new List<Location>();
            locations = _locationRepository.GetAll();

            _tourId = _tourRepository.NextId();
            images = new List<Model.Image>();
            checkpoints = new List<Checkpoint>();
            tourStartDates = new List<TourStartDate>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;
        public string TourName
        {
            get => _name;
            set
            {
                if(value != _name)
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

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if(_description != value)
                {
                    _description = value;
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

        private int _capacity;
        public int Capacity
        {
            get => _capacity;
            set
            {
                if(_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _durationHours;
        public float DurationHours
        {
            get => _durationHours;
            set
            {
                if(_durationHours != value)
                {
                    _durationHours = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool ValidateFields()
        {
            return _capacity > 0  && _name != null && _durationHours > 0 && _selectedLanguage != null && _selectedLocation != null
                && tourStartDates.Count() > 0 && checkpoints.Count() > 0 && _description != null;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Tour newTour = new Tour(_name, _description, _selectedLocation.locationId, _selectedLanguage.languageId, _capacity, _durationHours);
                _tourRepository.Save(newTour);

                foreach(var image in images)
                {
                    _imageRepository.Save(image);
                }
                foreach(var checkpoint in checkpoints)
                {
                    _checkpointRepository.Save(checkpoint);
                }
                foreach(var startDate in tourStartDates)
                {
                    _tourStartDateRepository.Save(startDate);
                }

                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Not added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnAddCheckpoints_Click(object sender, RoutedEventArgs e)
        {
            AddCheckpoint addCheckpointWindow = new AddCheckpoint(checkpoints, _tourId);
            addCheckpointWindow.Owner = this;
            addCheckpointWindow.ShowDialog();
        }

        private void btnShowCheckpoints_Click(object sender, RoutedEventArgs e)
        {
            ShowCheckpoints showCheckpointswindow = new ShowCheckpoints(checkpoints);
            showCheckpointswindow.Owner = this;
            showCheckpointswindow.ShowDialog();
        }

        private void btnAddStartDate_Click(object sender, RoutedEventArgs e)
        {
            if(_capacity > 0)
            {
                AddStartDate addStartDateWindow = new AddStartDate(tourStartDates, _tourId, _capacity);
                addStartDateWindow.Owner = this;
                addStartDateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Enter capacity", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnShowStartDates_Click(object sender, RoutedEventArgs e)
        {
            ShowStartDates showStartDatesWindow = new ShowStartDates(tourStartDates);
            showStartDatesWindow.Owner = this;
            showStartDatesWindow.ShowDialog();
        }

        private void btnShowImages_Click(object sender, RoutedEventArgs e)
        {
            ShowImages showImagesWindow = new ShowImages(images);
            showImagesWindow.Owner = this;
            showImagesWindow.ShowDialog();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(images, _tourId);
            addImageWindow.Owner = this;
            addImageWindow.ShowDialog(); 
        }
    }
}
