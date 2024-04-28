using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TouristTabsViewModel
    {
        public User Tourist { get; }

        public ToursMainTabViewModel ToursMainViewModel { get; }
        public DriveMainTabViewModel DriveMainViewModel { get; }


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
        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private DetailedLocationService _detailedLocationService;


        public TouristTabsViewModel(User loggedUser)
        {
            Tourist = loggedUser;


            InitializeServices();

            ToursMainViewModel = new ToursMainTabViewModel(loggedUser, _tourService, _tourInstanceService, _checkpointService, _imageService,  _locationService, _languageService, _tourGuestService, _tourReservationService, _tourReviewService, _voucherService);
            DriveMainViewModel = new DriveMainTabViewModel(loggedUser, _driveReservationService, _userService, _detailedLocationService);

        }

        private void InitializeServices()
        {
            _tourService = new TourService(Injector.CreateInstance<ITourRepository>(), _tourGuestService, _tourInstanceService);
            _tourInstanceService = new TourInstanceService();
            _checkpointService = new CheckpointService(Injector.CreateInstance<ICheckpointRepository>(), _tourInstanceService);
            _imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            _tourReviewService = new TourReviewService(Injector.CreateInstance<ITourReviewRepository>());
            _driveReservationService =
                new DriveReservationService(Injector.CreateInstance<IDriveReservationRepository>());
            _detailedLocationService =
                new DetailedLocationService(Injector.CreateInstance<IDetailedLocationRepository>());
            _userService = new UserService(Injector.CreateInstance<IUserRepository>());

        }
    }
}
