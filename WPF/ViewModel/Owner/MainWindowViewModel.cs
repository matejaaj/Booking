using BookingApp.Application.UseCases;
using BookingApp.Application;
using BookingApp.Commands;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guest;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BookingApp.Domain.Model;
using System.Collections.ObjectModel;
using BookingApp.DTO;
using System.Diagnostics.Eventing.Reader;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Domain.Model.Owner LoggedInOwner { get; set; }
        private string pageName { get; set; }
        public string PageName
        {
            get { return pageName; }
            set
            {
                pageName = value;
                OnPropertyChanged(nameof(PageName));
            }
        }
        private List<Notification> notifications { get; set; }
        public List<Notification> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }
        private Frame _mainFrame;
        public Frame MainFrame
        {
            get { return _mainFrame; }
            set { _mainFrame = value; OnPropertyChanged(); }
        }

        public ICommand ShowRatingsCommand { get; }
        public ICommand ShowAccommodationsCommand { get; }
        public ICommand ShowReschedulingCommand { get; }
        public ICommand ShowRenovationsCommand { get; }
        public ICommand ShowSuperOwnerCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand HideMenuCommand { get; }
        public ICommand ShowMenuCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand ShowNotificationsCommand { get; }
        public ICommand HideNotificationsCommand { get; }
        public ICommand ShowForumCommand { get; }
        public ICommand ItemClickedCommand { get; private set; }
        public ICommand HelpCommand { get; }
        private static AccommodationService _accommodationService;
        private static AccommodationReservationService _accommodationReservationService;
        private static AccommodationAndOwnerRatingService _accommodationAndOwnerRatingService;
        private static LocationService _locationService;
        private static ImageService _imageService;
        private static OwnerService _ownerService;
        private static SuggestionService _suggestionService;
        private static ForumService _forumService;
        private Visibility _sideMenuVisibility = Visibility.Collapsed;
        private OwnerMainWindow _ownerMainWindow;

        public Visibility SideMenuVisibility
        {
            get { return _sideMenuVisibility; }
            set 
            { 
                _sideMenuVisibility = value; 
                OnPropertyChanged(nameof(SideMenuVisibility)); 
            }
        }
        private Visibility _notificationMenuVisibility = Visibility.Collapsed;
        public Visibility NotificationMenuVisibility
        {
            get { return _notificationMenuVisibility; }
            set
            {
                _notificationMenuVisibility = value;
                OnPropertyChanged(nameof(NotificationMenuVisibility));
            }
        }

        private string _notificationImageSource = "../../../Resources/Images/notifications.png"; // Default image source
        public string NotificationImageSource
        {
            get { return _notificationImageSource; }
            set
            {
                _notificationImageSource = value;
                OnPropertyChanged(nameof(NotificationImageSource));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public MainWindowViewModel(Domain.Model.Owner owner, System.Windows.Controls.Frame mainFrame)
        {
            LoggedInOwner = owner;
            MainFrame = mainFrame;
            ShowRatingsCommand = new RelayCommand(ShowRatings);
            ShowAccommodationsCommand = new RelayCommand(ShowAccommodations);
            ShowReschedulingCommand = new RelayCommand(ShowRescheduling);
            ShowRenovationsCommand = new RelayCommand(ShowRenovations);
            HideMenuCommand = new RelayCommand(HideMenu);
            ShowMenuCommand = new RelayCommand(ShowMenu);
            ShowSuperOwnerCommand = new RelayCommand(ShowSuperOwner);
            LogOutCommand = new RelayCommand(LogOut);
            GoBackCommand = new RelayCommand(GoBack);
            ShowNotificationsCommand = new RelayCommand(ShowNotifications);
            HideNotificationsCommand = new RelayCommand(HideNotifications);
            ItemClickedCommand = new RelayCommand(ExecuteItemClicked);
            ShowForumCommand = new RelayCommand(ShowForum);
            HelpCommand = new RelayCommand(Help);

            Notifications = new List<Notification>();
            StartUp();            
            InitializeServices();
            NotifyMissingRatings();
            NotifySuggestions();
            NotifyNewForum();
        }

        private void Help(object obj)
        {
            PageName = "Help";
            MainFrame.Navigate(new OtherHelpPage());
        }

        private void StartUp()
        {
            if (LoggedInOwner.IsFirstLogIn)
            {
                PageName = "Accommodations";
                MainFrame.Navigate(new OwnerWizardPage(LoggedInOwner));
            }
            else
            {
                PageName = "Accommodations";
                var page = new AccommodationsPage(LoggedInOwner);
                MainFrame.Navigate(page);
            }
        }

        private void InitializeServices()
        {
            _imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(), _imageService, _locationService);
            _accommodationReservationService = new AccommodationReservationService(_accommodationService, Injector.CreateInstance<IAccommodationReservationRepository>());
            _accommodationAndOwnerRatingService = new AccommodationAndOwnerRatingService(_accommodationReservationService, Injector.CreateInstance<IAccommodationAndOwnerRatingRepository>());
            _ownerService = new OwnerService(Injector.CreateInstance<IOwnerRepository>());
            _suggestionService = new SuggestionService(_locationService, _accommodationService, _accommodationReservationService);
            _forumService = new ForumService(Injector.CreateInstance<IForumRepository>());
        }

        public void NotifyMissingRatings()
        {
            var missingRatingReservations = _accommodationReservationService.GetRecentUnratedReservations(_accommodationService.GetByUser(LoggedInOwner));
            if (missingRatingReservations.Any())
            {
                foreach(var r in missingRatingReservations)
                {
                    var accommodation = _accommodationService.GetById(r.AccommodationId);
                    Notifications.Add(new Notification("Missing Rating!", $"Reservation in {accommodation.Name} is missing a rating", DateTime.Today, accommodation.AccommodationId));
                }
                NotificationImageSource = "../../../Resources/Images/notifications1.png";
            }
            else
            {
                NotificationImageSource = "../../../Resources/Images/notifications.png";
            }
        }

        public void NotifySuggestions()
        {
            Location popularLocation = _suggestionService.GetMostPopularLocation(LoggedInOwner);
            Location unpopularLocation = _suggestionService.GetLeastPopularLocation(LoggedInOwner);
            Notifications.Add(new Notification("New Opportunity!", $"High demand detected! Consider opening a new Accommodation at {popularLocation}", DateTime.Today, -1));
            Notifications.Add(new Notification("Low Demand!", $"Low demand alert! Consider closing Accommodations at {unpopularLocation}", DateTime.Today, -1));
        }

        private void NotifyNewForum()
        {
            List<int> locationIds = _accommodationService.GetLocationIdsByOwner(LoggedInOwner);
            List<Forum> newForum = _forumService.GetNewForumsByLocationIds(locationIds);
            foreach(var f in newForum)
            {
                Location location = _locationService.GetLocationById(f.LocationId);
                Notifications.Add(new Notification("New Forum!", $"New forum alert! A user started a forum at {location}", DateTime.Today, f.Id));
            }
        }

        private void HideNotifications(object obj)
        {
            NotificationMenuVisibility = Visibility.Collapsed;
        }

        private void ShowNotifications(object obj)
        {
            if (NotificationMenuVisibility == Visibility.Collapsed)
            {
                NotificationMenuVisibility = Visibility.Visible;
            }
            else
            {
                NotificationMenuVisibility = Visibility.Collapsed;
            }
        }

        private void GoBack(object obj)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        public MainWindowViewModel(Domain.Model.Owner owner, Frame mainFrame, OwnerMainWindow ownerMainWindow) : this(owner, mainFrame)
        {
            _ownerMainWindow = ownerMainWindow;
        }

        private void ShowRatings(object parameter)
        {
            PageName = "Ratings";
            MainFrame.Navigate(new ViewRatingsPage(LoggedInOwner));
            SideMenuVisibility = Visibility.Collapsed;
        }

        private void HideMenu(object parameter)
        {
            SideMenuVisibility = Visibility.Collapsed;
        }

        private void ShowMenu(object parameter)
        {
            if (SideMenuVisibility == Visibility.Collapsed)
            {
                SideMenuVisibility = Visibility.Visible;
                NotificationMenuVisibility = Visibility.Collapsed;
            }
            else
            {
                SideMenuVisibility = Visibility.Collapsed;
            }
        }

        private void ShowAccommodations(object parameter)
        {
            PageName = "Accommodations";
            MainFrame.Navigate(new AccommodationsPage(LoggedInOwner));
            SideMenuVisibility = Visibility.Collapsed;
        }

        private void ShowRescheduling(object parameter)
        {
            PageName = "Rescheduling Requests";
            MainFrame.Navigate(new ReschedulingOverviewPage(LoggedInOwner));
            SideMenuVisibility = Visibility.Collapsed;
        }

        private void ShowRenovations(object parameter)
        {
            PageName = "Renovations";
            MainFrame.Navigate(new ViewRenovationsPage(LoggedInOwner));
            SideMenuVisibility = Visibility.Collapsed;
        }

        private void ShowSuperOwner(object parameter)
        {
            PageName = "Super-Owner";
            MainFrame.Navigate(new SuperOwnerPage(LoggedInOwner));
            SideMenuVisibility = Visibility.Collapsed;

        }
        private void ShowForum(object parameter)
        {
            PageName = "Forum";
            MainFrame.Navigate(new ViewForumsPage(LoggedInOwner));
            SideMenuVisibility = Visibility.Collapsed;
        }
            private void LogOut(object parameter)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            _ownerMainWindow.Close();
        }

        private void ExecuteItemClicked(object selectedItem)
        {
            var selectedNotification = (Notification)selectedItem;
            if(selectedNotification.TargetUserId == -1)
            {
                return;
            }else if (selectedNotification.Title.Equals("New Forum!"))
            {
                return;
            }
            AccommodationPageDTO accommodation = _accommodationService.GetDisplayDTOById(selectedNotification.TargetUserId);
            PageName = "Accommodations";
            MainFrame.Navigate(new ViewAccommodationPage(accommodation));
            NotificationMenuVisibility = Visibility.Collapsed;
        }
    }
}
