using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class StatisticsViewModel : INotifyPropertyChanged
    {
        private TourService _tourService;
        private LanguageService _languageService;
        private LocationService _locationService;
        private TourInstanceService _tourInstanceService;
        public List<Location> Locations { get; set; }
        public List<Language> Languages { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private TourDTO _mostVisitedTour;
        public TourDTO MostVisitedTour
        {
            get { return _mostVisitedTour; }
            set
            {
                if (_mostVisitedTour != value)
                {
                    _mostVisitedTour = value;
                    OnPropertyChanged(nameof(MostVisitedTour));
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
        public List<object> TourYears { get; set; }
        public object SelectedTourYear { get; set; }

        public List<object> RequestYears { get; set; }
        public object SelectedRequestYear { get; set; }

        public StatisticsViewModel()
        {
            InitializeServices();

            TourYears = new List<object>();
            TourYears.Add("All time");
            for (int i = DateTime.Now.Year; i >= _tourInstanceService.GetEarliestYear(); i--)
            {
                TourYears.Add((i).ToString());
            }

            Languages = _languageService.GetAll();
            Locations = _locationService.GetAll();
        }

        public void SearchTours()
        {
            Tour mostVisitedTour = _tourService.FindMostVisited(SelectedTourYear);
            MostVisitedTour = new TourDTO(mostVisitedTour);
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _tourService = new TourService(Injector.CreateInstance<ITourRepository>(), _tourGuestService, _tourInstanceService);
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
        }
    }
}