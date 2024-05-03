using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestsViewModel
    {
        private User _user;
        public ObservableCollection<TourRequestDTO> Requests { get; private set; }

        private LocationService _locationService;
        private LanguageService _languageService;
        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _tourSegmentService;
        private PrivateTourGuestService _tourGuestService;
        public TourRequestsViewModel(User user, LocationService locationService, LanguageService languageService, TourRequestService tourRequestService, TourRequestSegmentService tourSegmentService, PrivateTourGuestService tourGuestService)
        {
            _locationService = locationService;
            _languageService = languageService;
            _tourRequestService = tourRequestService;
            _tourSegmentService = tourSegmentService;
            _tourGuestService = tourGuestService;
            _user = user;
        }

        public void OpenWindow()
        {
            var tourRequestFormWindow = new TourRequestFormWindow(_user, _locationService, _languageService, _tourRequestService, _tourSegmentService, _tourGuestService);
            tourRequestFormWindow.Show();
        }
        
    }
}
