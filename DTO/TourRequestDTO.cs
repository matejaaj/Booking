using BookingApp.Application.UseCases;
using BookingApp.Application;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.View.Guide;
using Syncfusion.Windows.Shared;

namespace BookingApp.DTO
{
    public class TourRequestDTO
    {

        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;

        public int Id { get; set; }

        private int _tourRequestId;
        public int TourRequestId
        {
            get { return _tourRequestId; }
            set
            {
                if( _tourRequestId != value )
                {
                    _tourRequestId = value;
                    OnPropertyChanged("TourRequestId");
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

        private int _capacity;
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged("MaximumCapacity");
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

        private DateTime _fromDate;
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                if(_fromDate != value)
                {
                    _fromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }
        private DateTime _toDate;
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                if(_toDate != value)
                {
                    _toDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }
        private DateTime _acceptedDate;
        public DateTime AcceptedDate
        {
            get { return _acceptedDate; }
            set
            {
                if(_acceptedDate != value)
                {
                    _acceptedDate = value;
                    OnPropertyChanged("AcceptedDate");
                }
            }
        }
        private TourRequestStatus _isAccepted;
        public TourRequestStatus IsAccepted
        {
            get { return _isAccepted; } 
            set
            {
                if(_isAccepted != value)
                {
                    _isAccepted = value;
                    OnPropertyChanged("IsAccepted");
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

        public TourRequestDTO()
        {
            Language = "not set";
            Location = "not set";
        }

        public TourRequestDTO(TourRequestSegment tourRequest)
        {
            _locationService = new LocationService();
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());

            Id = tourRequest.Id;
            Description = tourRequest.Description;
            LocationId = tourRequest.LocationId;
            LanguageId = tourRequest.LanguageId;
            Capacity = tourRequest.Capacity;

            Language language = _languageService.GetById(LanguageId);
            Location location = _locationService.GetLocationById(LocationId);

            Language = language.Name;
            Location = location.City + " " + location.Country;

            FromDate = tourRequest.FromDate;
            ToDate = tourRequest.ToDate;
            AcceptedDate = tourRequest.AcceptedDate;
            TourRequestId = tourRequest.TourRequestId;
        }
        public TourRequestSegment ToTour()
        {
            return new TourRequestSegment(TourRequestId, Description, LocationId, LanguageId, Capacity, FromDate, ToDate);
        }



    }
}
