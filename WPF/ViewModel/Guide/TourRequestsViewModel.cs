using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Application.UseCases;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Linq;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TourRequestsViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }

        private TourRequestDTO _selectedRequest;
        public string DateRange { get; set; }
        public TourRequestDTO SelectedRequest 
        {
            get { return _selectedRequest; }
            set
            {
                if (_selectedRequest != value)
                {
                    _selectedRequest = value;
                    OnPropertyChanged(nameof(SelectedRequest));
                    UpdateDateRange();
                }
            }
        }
        private int _capacity;
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if(_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }

        private void UpdateDateRange()
        {
            if (_selectedRequest != null)
            {
                DateRange = $"From {_selectedRequest.FromDate.ToString("dd.MM.yyyy")} to {_selectedRequest.ToDate.ToString("dd.MM.yyyy")}";
                OnPropertyChanged("DateRange");
            }
            SelectedDate = SelectedRequest.FromDate;
        }

        private DateTime _selectedFirstDate;
        private DateTime _selectedSecondDate;

        public DateTime SelectedFirstDate
        {
            get { return _selectedFirstDate; }
            set
            {
                if (_selectedFirstDate != value)
                {
                    _selectedFirstDate = value;
                    OnPropertyChanged("SelectedFirstDate");
                }
            }
        }

        public DateTime SelectedSecondDate
        {
            get { return _selectedSecondDate; }
            set
            {
                if (_selectedSecondDate != value)
                {
                    _selectedSecondDate = value;
                    OnPropertyChanged("SelectedSecondDate");
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
                    OnPropertyChanged("SelectedLocation");
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
                    OnPropertyChanged("SelectedLanguage");
                }
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if(_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }
        public List<Location> Locations { get; set; }
        public List<Language> Languages { get; set; }

        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _segmentService;
        private LocationService _locationService;
        private LanguageService _languageService;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourRequestsViewModel()
        {
            InitializeServices();
            Languages = _languageService.GetAll();
            Locations = _locationService.GetAll();
            TourRequests = new ObservableCollection<TourRequestDTO>();

            SelectedFirstDate = DateTime.Now.Date;
            SelectedSecondDate = DateTime.Now.Date;

            LoadRequests();
        }
        
        public void LoadRequests()
        {
            List<TourRequest> simpleRequests = _tourRequestService.GetSimpleRequests();
            foreach(var request in simpleRequests)
            {
                TourRequests.Add(new TourRequestDTO(_segmentService.GetByRequestId(request.Id)));
            }
        }
        public void InitializeServices()
        {
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>());
            _segmentService = new TourRequestSegmentService(Injector.CreateInstance<ITourRequestSegmentRepository>());
        }

        public void Filter()
        {
            var filteredRequests = _tourRequestService.GetSimpleRequests()
                      .Select(req => new TourRequestDTO(_segmentService.GetByRequestId(req.Id)))
                      .Where(req =>
                          (SelectedLanguage == null || req.LanguageId == SelectedLanguage.Id) &&
                          (SelectedLocation == null || req.LocationId == SelectedLocation.Id) &&
                          (req.FromDate.Date >= SelectedFirstDate.Date && req.ToDate.Date <= SelectedSecondDate.Date) &&
                          req.Capacity <= Capacity
                      ).ToList();

            TourRequests.Clear();
            foreach (var request in filteredRequests)
            {
                TourRequests.Add(request);
            }

            if (TourRequests.Count == 0)
            {
                MessageBox.Show("No matching records found.");
            }
        }

        public void Accept()
        {
            if(SelectedDate != null)
            {
                TourRequestSegment request = SelectedRequest.ToRequest();
                request.Id = SelectedRequest.Id;
                request.AcceptedDate = SelectedDate;
                _segmentService.MarkAsAccepted(request);
            }
        }
    }
}
