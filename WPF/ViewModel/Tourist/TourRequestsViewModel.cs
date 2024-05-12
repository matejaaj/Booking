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
        public ObservableCollection<TourRequestDTO> Requests { get; private set; }
        private TourRequestDTOFactory dtoFactory;

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
            Requests = new ObservableCollection<TourRequestDTO>();

            CheckForExpiredRequests();
            UpdateTourRequests();
        }

        private void CheckForExpiredRequests()
        {
            _tourRequestService.CheckExpiredSimpleRequests(_tourSegmentService.GetAll());
        }

        public void UpdateTourRequests()
        {
            Requests.Clear();
            var requests = _tourRequestService.GetSimpleRequestsForUser(_user.Id);
            foreach (var dto in dtoFactory.CreateDTOs(requests))
            {
                Requests.Add(dto);
            }
        }


        public void OpenFormWindow()
        {
            var tourRequestFormWindow = new TourRequestFormWindow(_user, _locationService, _languageService, _tourRequestService, _tourSegmentService, _tourGuestService);
            tourRequestFormWindow.Show();
            UpdateTourRequests();
        }

        public void OpenStatisticsWindow()
        {
            var tourRequestStatsWindow =
                new TourRequestStatisticsWindow(_user, _tourRequestService, _tourSegmentService);
            tourRequestStatsWindow.Show();
        }
    }
}
