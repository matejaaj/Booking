using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ToursMainTabViewModel
    {
        private TourService _tourService;
        private TourInstanceService _tourInstanceService;
        private CheckpointService _checkpointService;
        private ImageService _imageService;
        private LocationService _locationService;
        private LanguageService _languageService;
        private TourGuestService _tourGuestService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;


        public MyToursViewModel MyToursViewModel { get;  }
        public AllToursViewModel AllToursViewModel { get;  }

        public ToursMainTabViewModel(User loggedUser, TourService tourService, TourInstanceService tourInstanceService, CheckpointService checkpointService, ImageService imageService, LocationService locationService, LanguageService languageService, TourGuestService tourGuestService, TourReservationService tourReservationService, TourReviewService tourReviewService, VoucherService _voucherService)
        {
            _tourService = tourService;
            _tourInstanceService = tourInstanceService;
            _checkpointService = checkpointService;
            _imageService = imageService;
            _locationService = locationService;
            _languageService = languageService;
            _tourGuestService = tourGuestService;
            _tourReservationService = tourReservationService;
            _tourReviewService = tourReviewService;


            MyToursViewModel = new MyToursViewModel(loggedUser, tourService, tourInstanceService, checkpointService, imageService, locationService, languageService, tourGuestService, tourReservationService, _voucherService, tourReviewService);
            AllToursViewModel = new AllToursViewModel(loggedUser, tourService, tourInstanceService, checkpointService, imageService, locationService, languageService, tourGuestService, tourReservationService, _voucherService);
        }
        
    }
}
