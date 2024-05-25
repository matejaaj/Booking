using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.DTO.Factories;
using BookingApp.WPF.View.Tourist;


namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestsViewModel
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
        public TourRequestsViewModel(User user, LocationService locationService, LanguageService languageService, TourRequestService tourRequestService, TourRequestSegmentService tourSegmentService, PrivateTourGuestService tourGuestService, PrivateTourGuestService guestService)
        {
            _locationService = locationService;
            _languageService = languageService;
            _tourRequestService = tourRequestService;
            _tourSegmentService = tourSegmentService;
            _tourGuestService = tourGuestService;
            _user = user;

            dtoFactory = new TourRequestDTOFactory(locationService, languageService, guestService, tourSegmentService);
            _expirationChecker = new TourRequestExpirationChecker(_tourRequestService, tourSegmentService);
            SimpleRequests = new ObservableCollection<TourRequestDTO>();
            ComplexRequests = new ObservableCollection<ComplexTourRequestDTO>();

            CheckForExpiredRequests();
            UpdateTourRequests();
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


        public void OpenFormWindow()
        {
            var tourRequestFormWindow = new TourRequestFormWindow(_user, _locationService, _languageService, _tourRequestService, _tourSegmentService, _tourGuestService);
            tourRequestFormWindow.Closed += (sender, args) => UpdateTourRequests();
            tourRequestFormWindow.Show();
        }


        public void OpenStatisticsWindow()
        {
            var tourRequestStatsWindow =
                new TourRequestStatisticsWindow(_user, _tourRequestService, _tourSegmentService);
            tourRequestStatsWindow.Show();
        }
    }
}
