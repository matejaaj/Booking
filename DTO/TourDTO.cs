using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using Syncfusion.Windows.Shared;

namespace BookingApp.DTO
{
    public class TourDTO : INotifyPropertyChanged
    {
        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;

        public int Id { get; set; }
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _description { get; set; }
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private int _locationId;
        public int LocationId
        {
            get { return _locationId; }
            set
            {
                if (_locationId != value)
                {
                    _locationId = value;
                    OnPropertyChanged("LocationId");
                }
            }
        }

        private int _languageId;
        public int LanguageId
        {
            get { return _languageId; }
            set
            {
                if (_languageId != value)
                {
                    _languageId = value;
                    OnPropertyChanged("LanguageId");
                }
            }
        }

        private int _maximumCapacity;
        public int MaximumCapacity
        {
            get { return _maximumCapacity; }
            set
            {
                if (_maximumCapacity != value)
                {
                    _maximumCapacity = value;
                    OnPropertyChanged("MaximumCapacity");
                }
            }
        }

        private float _durationHours;
        public float DurationHours
        {
            get { return _durationHours; }
            set
            {
                if (_durationHours != value)
                {
                    _durationHours = value;
                    OnPropertyChanged("DurationHours");
                }
            }
        }
        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }
        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private List<string> _images;

        public List<string> Images
        {
            get { return _images; }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged("Images");
                }
            }
        }

        private List<Checkpoint> _checkpoints;

        public List<Checkpoint> Checkpoints
        {
            get { return _checkpoints; }
            set
            {
                if (_checkpoints != value)
                {
                    _checkpoints = value;
                    OnPropertyChanged("Checkpoints");
                }
            }
        }

        private List<DateTime> _dates;

        public List<DateTime> Dates
        {
            get { return _dates; }
            set
            {
                if (_dates != value)
                {
                    _dates = value;
                    OnPropertyChanged("Dates");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        public TourDTO()
        {
            Language = "not set";
            Location = "not set";
        }

        public TourDTO(Tour tour)
        {
            _locationService = new LocationService();
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());

            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            LocationId = tour.LocationId;
            LanguageId = tour.LanguageId;
            MaximumCapacity = tour.MaximumCapacity;
            DurationHours = tour.DurationHours;

            Language language = _languageService.GetById(LanguageId);
            Location location = _locationService.GetLocationById(LocationId);

            Language = language.Name;
            Location = location.City + " " + location.Country;
        }

        public Tour ToTour()
        {
            return new Tour(_name, _description, _locationId, _languageId, _maximumCapacity, _durationHours);
        }


    }
}
