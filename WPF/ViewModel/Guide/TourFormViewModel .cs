using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TourFormViewModel : INotifyPropertyChanged
    {
        private readonly TourRepository _tourRepository;
        private readonly CheckpointRepository _checkpointRepository;
        private readonly ImageRepository _imageRepository;
        private readonly LocationRepository _locationRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly TourInstanceRepository _tourStartDateRepository;

        public List<Location> Locations { get; set; }
        public List<Language> Languages { get; set; }
        public List<Domain.Model.Image> Images { get; set; }
        public List<TourInstance> TourStartDates { get; set; }
        public List<Checkpoint> Checkpoints { get; set; }

        private int _tourId;

        public TourFormViewModel()
        {
            _tourRepository = new TourRepository();
            _checkpointRepository = new CheckpointRepository();
            _imageRepository = new ImageRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();
            _tourStartDateRepository = new TourInstanceRepository();

            _tourId = _tourRepository.NextId();

            Languages = _languageRepository.GetAll();
            Locations = _locationRepository.GetAll();
            Images = new List<Domain.Model.Image>();
            Checkpoints = new List<Checkpoint>();
            TourStartDates = new List<TourInstance>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properties for data binding
        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (_tourName != value)
                {
                    _tourName = value;
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
                if (_description != value)
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

        public int TourId
        {
            get => _tourId;
        }

        private int _capacity;
        public int Capacity
        {
            get => _capacity;
            set
            {
                if (_capacity != value)
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
                if (_durationHours != value)
                {
                    _durationHours = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ValidateFields()
        {
            return _capacity > 0 && !string.IsNullOrEmpty(_tourName) && _durationHours > 0 && _selectedLanguage != null &&
                   _selectedLocation != null && TourStartDates.Count > 0 && Checkpoints.Count > 0 && !string.IsNullOrEmpty(_description);
        }

        public void SaveTour()
        {
            if (ValidateFields())
            {
                Tour newTour = new Tour(_tourName, _description, _selectedLocation.Id, _selectedLanguage.languageId, _capacity, _durationHours);
                _tourRepository.Save(newTour);

                foreach (var image in Images)
                {
                    _imageRepository.Save(image);
                }

                foreach (var checkpoint in Checkpoints)
                {
                    _checkpointRepository.Save(checkpoint);
                }

                foreach (var startDate in TourStartDates)
                {
                    _tourStartDateRepository.Save(startDate);
                }

                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Not added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
