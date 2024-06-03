using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.DTO.Factories;
using BookingApp.WPF.View.Tourist;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestsViewModel : INotifyPropertyChanged
    {
        private User _user;
        public ObservableCollection<TourRequestDTO> SimpleRequests { get; private set; }
        public ObservableCollection<ComplexTourRequestDTO> ComplexRequests { get; private set; }

        private TourRequestDTOFactory dtoFactory;
        private TourRequestExpirationChecker _expirationChecker;

        private LocationService _locationService;
        private LanguageService _languageService;
        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _tourSegmentService;
        private PrivateTourGuestService _tourGuestService;

        public ICommand OpenTourRequestFormCommand { get; private set; }
        public ICommand OpenRequestStatisticsCommand { get; private set; }
        public ICommand ShowSimpleRequestDetailsCommand { get; private set; }
        public ICommand ShowComplexRequestDetailsCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TourRequestsViewModel(User user, LocationService locationService, LanguageService languageService, TourRequestService tourRequestService, TourRequestSegmentService tourSegmentService, PrivateTourGuestService tourGuestService, PrivateTourGuestService guestService)
        {
            _locationService = locationService;
            _languageService = languageService;
            _tourRequestService = tourRequestService;
            _tourSegmentService = tourSegmentService;
            _tourGuestService = tourGuestService;
            _user = user;



            InitializeFields();
            CheckForExpiredRequests();
            UpdateTourRequests();

            TranslationSource.Instance.PropertyChanged += OnLanguageChanged;
        }

        private void InitializeFields()
        {
            dtoFactory = new TourRequestDTOFactory(_locationService, _languageService, _tourGuestService, _tourSegmentService);
            _expirationChecker = new TourRequestExpirationChecker(_tourRequestService, _tourSegmentService);
            SimpleRequests = new ObservableCollection<TourRequestDTO>();
            ComplexRequests = new ObservableCollection<ComplexTourRequestDTO>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            OpenTourRequestFormCommand = new RelayCommand(_ => OpenFormWindow());
            OpenRequestStatisticsCommand = new RelayCommand(_ => OpenStatisticsWindow());
            ShowSimpleRequestDetailsCommand = new RelayCommand(ShowSimpleRequestDetails);
            ShowComplexRequestDetailsCommand = new RelayCommand(ShowComplexRequestDetails);
        }

        private void CheckForExpiredRequests()
        {
            _expirationChecker.CheckAndExpireRequests();
        }

        public void UpdateTourRequests()
        {
            ComplexRequests.Clear();
            var complexRequests = _tourRequestService.GetComplexRequestsForUser(_user.Id);
            foreach (var dto in dtoFactory.CreateComplexTourDTOs(complexRequests))
            {
                ComplexRequests.Add(dto);
            }

            SimpleRequests.Clear();
            var simpleRequests = _tourRequestService.GetSimpleRequestsForUser(_user.Id);
            foreach (var dto in dtoFactory.CreateSimpleTourDTOs(simpleRequests))
            {
                SimpleRequests.Add(dto);
            }
        }

        private void OnLanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTourRequests();
        }

        private void ShowSimpleRequestDetails(object parameter)
        {
            if (parameter is TourRequestDTO tourRequest)
            {
                SimpleTourRequestDetails detailsWindow = new SimpleTourRequestDetails(tourRequest);
                detailsWindow.Show();
            }
        }

        private void ShowComplexRequestDetails(object parameter)
        {
            if (parameter is ComplexTourRequestDTO complexTourRequest)
            {
                ComplexTourRequestDetails detailsWindow = new ComplexTourRequestDetails(complexTourRequest);
                detailsWindow.Show();
            }
        }

        public void OpenFormWindow()
        {
            var tourRequestFormWindow = new TourRequestFormWindow(_user, _locationService, _languageService, _tourRequestService, _tourSegmentService, _tourGuestService);
            tourRequestFormWindow.Closed += (sender, args) => UpdateTourRequests();
            tourRequestFormWindow.Show();
        }

        public void OpenStatisticsWindow()
        {
            var tourRequestStatsWindow = new TourRequestStatisticsWindow(_user, _tourRequestService, _tourSegmentService);
            tourRequestStatsWindow.Show();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
