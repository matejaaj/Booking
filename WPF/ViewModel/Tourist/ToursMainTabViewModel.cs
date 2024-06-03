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
        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _tourRequestSegmentService;
        private PrivateTourGuestService _privateTourGuestService;
        private TouristVoucherAcquirer _voucherAcquirer;


        public MyToursViewModel MyToursViewModel { get;  }
        public AllToursViewModel AllToursViewModel { get;  }

        public TourRequestsViewModel TourRequestsViewModel { get; }

        public ToursMainTabViewModel(User loggedUser, TourService tourService, TourInstanceService tourInstanceService, CheckpointService checkpointService, ImageService imageService, LocationService locationService, LanguageService languageService, TourGuestService tourGuestService, TourReservationService tourReservationService, TourReviewService tourReviewService, VoucherService _voucherService, TourRequestService request, TourRequestSegmentService segment, PrivateTourGuestService privateguest )
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
            _tourRequestService = request;
            _tourRequestSegmentService = segment;
            _privateTourGuestService = privateguest;


            _voucherAcquirer =
                new TouristVoucherAcquirer(_tourReservationService, _voucherService, _tourInstanceService);

            MyToursViewModel = new MyToursViewModel(loggedUser, tourService, tourInstanceService, checkpointService, imageService, locationService, languageService, tourGuestService, tourReservationService, _voucherService, tourReviewService);
            AllToursViewModel = new AllToursViewModel(loggedUser, tourService, tourInstanceService, checkpointService, imageService, locationService, languageService, tourGuestService, tourReservationService, _voucherService);
            TourRequestsViewModel = new TourRequestsViewModel(loggedUser,_locationService, languageService, _tourRequestService, _tourRequestSegmentService, _privateTourGuestService, _privateTourGuestService);


            _voucherAcquirer.AcquireVouchersForUser(_tourReservationService.GetAllByUserId(loggedUser.Id));
        }
        
    }
}
