using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;

using BookingApp.Repository;
using BookingApp.WPF.View;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TouristTabsViewModel
    {
        public User Tourist { get; }

        public ICommand ChangeThemeCommand { get; private set; }
        public ICommand ChangeLanguageCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public event EventHandler RequestClose;

        public ToursMainTabViewModel ToursMainViewModel { get; }
        public DriveMainTabViewModel DriveMainViewModel { get; }

        public ObservableCollection<NotificationDTO> Notifications { get; private set; }


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
        private DriverUnreliableReportService _driverUnreliableReportService;
        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _tourRequestSegmentService;
        private PrivateTourGuestService _privateTourGuestService;
        private NotificationService _notificationService;


        public TouristTabsViewModel(User loggedUser)
        {
            Tourist = loggedUser;
            Notifications = new ObservableCollection<NotificationDTO>();

            InitializeServices();
            InitializeCommands();
            UpdateNotifications();


            ToursMainViewModel = new ToursMainTabViewModel(loggedUser, _tourService, _tourInstanceService, _checkpointService, _imageService,  _locationService, _languageService, _tourGuestService, _tourReservationService, _tourReviewService, _voucherService, _tourRequestService, _tourRequestSegmentService, _privateTourGuestService);
            DriveMainViewModel = new DriveMainTabViewModel(loggedUser, _driveReservationService, _userService, _detailedLocationService, _driverUnreliableReportService);

        }

        private void InitializeCommands()
        {
            ChangeThemeCommand = new RelayCommand(_ => ChangeTheme());
            ChangeLanguageCommand = new RelayCommand(_ => ChangeLanguage());
            LogoutCommand = new RelayCommand(_ => Logout());
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
            _driverUnreliableReportService =
                new DriverUnreliableReportService(Injector.CreateInstance<IDriverUnreliableReportRepository>());
            _tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>());
            _tourRequestSegmentService =
                new TourRequestSegmentService(Injector.CreateInstance<ITourRequestSegmentRepository>());
            _privateTourGuestService =
                new PrivateTourGuestService(Injector.CreateInstance<IPrivateTourGuestRepository>());
            _notificationService = new NotificationService(Injector.CreateInstance<INotificationRepository>());

        }


        private void UpdateNotifications()
        {
            Notifications.Clear();
            var notifications = _notificationService.GetNotificationsForUser(Tourist.Id);
            foreach (var notification in notifications)
            {
                NotificationDTO dto = new NotificationDTO(notification.Id, notification.Title, notification.Text,
                    notification.DateIssued, notification.TargetUserId);
                Notifications.Add(dto);
            }
        }

        public void DeleteNotification(int notificaitonId)
        {
            _notificationService.RemoveNotification(notificaitonId);
            UpdateNotifications();
        }

        public void ChangeTheme()
        {
            var dictionaries = System.Windows.Application.Current.Resources.MergedDictionaries;
            var themeDict = dictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme"));

            if (themeDict != null)
            {
                var newTheme = themeDict.Source.OriginalString.Contains("Light") ? "Dark" : "Light";
                var newUri = new Uri($"pack://application:,,,/Themes/Tourist{newTheme}Theme.xaml");

                var newDict = new ResourceDictionary { Source = newUri };
                dictionaries.Remove(themeDict);
                dictionaries.Add(newDict);
            }
        }

        private void ChangeLanguage()
        {
            
        }

        private void Logout()
        {
            SignInForm signIn = new SignInForm();
            signIn.Show();
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

    }

}

