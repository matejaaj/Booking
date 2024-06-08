using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class ViewAccommodationViewModel : INotifyPropertyChanged
    {
        public static Accommodation Accommodation { get; set; }
        private string currentImage;
        public string CurrentImage
        {
            get { return currentImage; }
            set
            {
                currentImage = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }
        public AccommodationPageDTO SelectedAccommodation { get; set; }
        private static AccommodationReservationService _accommodationReservationService;
        private static GuestRatingService _guestRatingService;
        private List<GuestRating> _guestRatings;

        private ObservableCollection<BookingDTO> _recentAccommodationReservations;
        public ObservableCollection<BookingDTO> RecentAccommodationReservations
        {
            get { return _recentAccommodationReservations; }
            set
            {
                _recentAccommodationReservations = value;
                OnPropertyChanged(nameof(RecentAccommodationReservations));
            }
        }

        private ObservableCollection<BookingDTO> _pastAccommodationReservations;
        public ObservableCollection<BookingDTO> PastAccommodationReservations
        {
            get { return _pastAccommodationReservations; }
            set
            {
                _pastAccommodationReservations = value;
                OnPropertyChanged(nameof(PastAccommodationReservations));
            }
        }

        private ObservableCollection<BookingDTO> _otherAccommodationReservations;
        public ObservableCollection<BookingDTO> OtherAccommodationReservations
        {
            get { return _otherAccommodationReservations; }
            set
            {
                _otherAccommodationReservations = value;
                OnPropertyChanged(nameof(OtherAccommodationReservations));
            }
        }
        private static AccommodationService _accommodationService;
        private bool IsNew { get; set; }

        private int _currentImageIndex;
        public int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            set
            {
                if (_currentImageIndex != value)
                {
                    _currentImageIndex = value;
                    OnPropertyChanged(nameof(CurrentImageIndex));
                }
            }
        }

        public ICommand ItemClickedCommand { get; }
        public ICommand RenovationCommand { get; }
        public ICommand StatisticsCommand { get; }
        public ICommand NextImageCommand { get; }
        public ICommand PreviousImageCommand { get; }
        public ICommand DeleteCommand { get; }

        private ViewAccommodationPage _currentPage;

        public ViewAccommodationViewModel(AccommodationPageDTO accommodation, ViewAccommodationPage viewAccommodationPage)
        {
            InitializeServices();
            SelectedAccommodation = accommodation;
            _currentPage = viewAccommodationPage;
            CurrentImage = SelectedAccommodation.Images?.Count > 0 ? SelectedAccommodation.Images[CurrentImageIndex] : null;
            Accommodation = _accommodationService.GetById(SelectedAccommodation.Id);
            _guestRatings = _guestRatingService.GetAll();
            FillReservations();
            ItemClickedCommand = new RelayCommand(ItemClicked);
            RenovationCommand = new RelayCommand(Renovate);
            StatisticsCommand = new RelayCommand(ShowStatistics);
            NextImageCommand = new RelayCommand(NextImage);
            PreviousImageCommand = new RelayCommand(PreviousImage);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Delete(object obj)
        {
           // _accommodationService.Delete(Accommodation);
           // _currentPage.NavigationService.Navigate(new AccommodationsPage(logg));
        }

        private void PreviousImage(object obj)
        {
            if (SelectedAccommodation.Images == null || SelectedAccommodation.Images.Count == 0) return;
            CurrentImageIndex = (CurrentImageIndex - 1 + SelectedAccommodation.Images.Count) % SelectedAccommodation.Images.Count;
            CurrentImage = SelectedAccommodation.Images[CurrentImageIndex];
        }

        private void NextImage(object obj)
        {
            if (SelectedAccommodation.Images == null || SelectedAccommodation.Images.Count == 0) return;
            CurrentImageIndex = (CurrentImageIndex + 1) % SelectedAccommodation.Images.Count;
            CurrentImage = SelectedAccommodation.Images[CurrentImageIndex];
        }

        private void ShowStatistics(object obj)
        {
            AccommodationStatsPage page = new AccommodationStatsPage(Accommodation);
            _currentPage.NavigationService.Navigate(page);
        }

        private void Renovate(object obj)
        {
            RenovationSchedulingPage page = new RenovationSchedulingPage(Accommodation, SelectedAccommodation);
            _currentPage.NavigationService.Navigate(page);
        }

        private void ItemClicked(object selectedItem)
        {
            var selectedReservation = (BookingDTO)selectedItem;
            if (selectedReservation != null)
            {
                //var selectedReservation = (BookingDTO)RecentReservationsListBox.SelectedItem;
                if (selectedReservation.IsRated.Equals("Not Rated"))
                {
                    var reservation = _accommodationReservationService.GetByReservationId(selectedReservation.Id);
                    var guestRatingFormWindow = new GuestRatingForm(reservation);
                    guestRatingFormWindow.Owner = Window.GetWindow(_currentPage);
                    guestRatingFormWindow.ShowDialog();
                    FillReservations();
                }
                else
                {
                    MessageBox.Show("Reservation already rated",
                          "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void InitializeServices()
        {
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            _guestRatingService = new GuestRatingService(Injector.CreateInstance<IGuestRatingRepository>());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FillReservations()
        {
            RecentAccommodationReservations = new ObservableCollection<BookingDTO>(
                _accommodationReservationService.GetRecentReservations(Accommodation));

            PastAccommodationReservations = new ObservableCollection<BookingDTO>(
                _accommodationReservationService.GetPastReservations(Accommodation));

            OtherAccommodationReservations = new ObservableCollection<BookingDTO>(
                _accommodationReservationService.GetOtherReservations(Accommodation));
        }

/*        private bool IsReservationRated(AccommodationReservation accommodationReservation)
        {
            var selectedAccommodationRating = _guestRatings.Find(rating => accommodationReservation.AccommodationId == rating.GuestRatingId);
            return !(selectedAccommodationRating == null);
        }*/

        public void RecentReservationsListBox_SelectionChanged(object sender, ListBox RecentReservationsListBox, ViewAccommodationPage viewAccommodationPage)
        {
            if (RecentReservationsListBox.SelectedItem != null)
            {
                var selectedReservation = (BookingDTO)RecentReservationsListBox.SelectedItem;
                if (selectedReservation.IsRated.Equals("Not Rated"))
                {
                    var reservation = _accommodationReservationService.GetByReservationId(selectedReservation.Id);
                    var guestRatingFormWindow = new GuestRatingForm(reservation);
                    guestRatingFormWindow.Owner = Window.GetWindow(viewAccommodationPage);
                    guestRatingFormWindow.ShowDialog();
                    FillReservations();
                }
                else
                {
                    MessageBox.Show("Reservation already rated",
                          "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        internal void RenovateAccommodation_Click(object sender, RoutedEventArgs e, ViewAccommodationPage viewAccommodationPage)
        {
            RenovationSchedulingPage page = new RenovationSchedulingPage(Accommodation, SelectedAccommodation);
            viewAccommodationPage.NavigationService.Navigate(page);
        }

        internal void Statistics_Click(object sender, RoutedEventArgs e, ViewAccommodationPage viewAccommodationPage)
        {
            AccommodationStatsPage page = new AccommodationStatsPage(Accommodation);
            viewAccommodationPage.NavigationService.Navigate(page);
        }

        internal void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation.Images == null || SelectedAccommodation.Images.Count == 0) return;
            CurrentImageIndex = (CurrentImageIndex - 1 + SelectedAccommodation.Images.Count) % SelectedAccommodation.Images.Count;
            CurrentImage = SelectedAccommodation.Images[CurrentImageIndex];
        }

        internal void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation.Images == null || SelectedAccommodation.Images.Count == 0) return;
            CurrentImageIndex = (CurrentImageIndex + 1) % SelectedAccommodation.Images.Count;
            CurrentImage = SelectedAccommodation.Images[CurrentImageIndex];
        }
    }
}
