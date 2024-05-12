using BookingApp.Application;
using BookingApp.Application.UseCases;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class OwnerOverviewViewModel : INotifyPropertyChanged
    {
        public Domain.Model.Owner LoggedInOwner { get; set; }
        public bool isSuperOwner { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public static ObservableCollection<AccommodationPageDTO> AccommodationsDTOs { get; set; }
        public List<AccommodationReservation> OwnerAccommodationReservations { get; set; }  
        public AccommodationPageDTO SelectedAccommodation { get; set; }
        private static AccommodationService _accommodationService;
        private static AccommodationReservationService _accommodationReservationService;
        private static AccommodationAndOwnerRatingService _accommodationAndOwnerRatingService;
        private static LocationService _locationService;
        private static ImageService _imageService;
        private static OwnerService _ownerService;
        public int RatingsNumber { get; set; }
        public double AverageScore { get; set; }

        public OwnerOverviewViewModel(Domain.Model.Owner owner)
        {
            LoggedInOwner = owner;
            InitializeServices();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetByUser(LoggedInOwner));
            InitializeAccommodationReservaions();
            CalculateRating();
            Update();
        }

        private void Update()
        {
            AccommodationsDTOs = new ObservableCollection<AccommodationPageDTO>(_accommodationService.GetByUserWithLocationAndImage(LoggedInOwner));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitializeAccommodationReservaions()
        {
            OwnerAccommodationReservations = _accommodationReservationService.GetByAccommodationIds(Accommodations);
        }

        private void InitializeServices()
        {
            _imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(), _imageService, _locationService);
            _accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            _accommodationAndOwnerRatingService = new AccommodationAndOwnerRatingService(_accommodationReservationService, Injector.CreateInstance<IAccommodationAndOwnerRatingRepository>());
            _ownerService = new OwnerService(Injector.CreateInstance<IOwnerRepository>());
        }

        private void CalculateRating()
        {
            RatingsNumber = GetRatings().Count();
            var individualAverages = _accommodationAndOwnerRatingService.CalculateIndividualAverages(GetRatings());
            AverageScore = _accommodationAndOwnerRatingService.CalculateAverageScore(individualAverages, RatingsNumber);
            CheckSuperOwner();
        }

        private void CheckSuperOwner()
        {
            LoggedInOwner.NumberOfRatings = RatingsNumber;
            LoggedInOwner.AverageRating = AverageScore;
            if(RatingsNumber > 50 && AverageScore > 4.5)
            {
                isSuperOwner = true;
            }
            else
            {
                LoggedInOwner.SuperOwner = false;
            }
            _ownerService.Update(LoggedInOwner);
        }

        private List<AccommodationAndOwnerRating> GetRatings() {
            return _accommodationAndOwnerRatingService.GetByReservations(OwnerAccommodationReservations);
        }

        public void NewAccommodation_Click(object sender, RoutedEventArgs e, AccommodationsPage accommodationsPage)
        {
            AccommodationForm accommodationForm = new AccommodationForm(LoggedInOwner);
            accommodationForm.Owner = Window.GetWindow(accommodationsPage);
            accommodationForm.Show();
            Update();
        }

        public ViewAccommodationPage ShowViewAccommodation(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Please choose an accommodation to view!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else
            {
                return new ViewAccommodationPage(SelectedAccommodation);
            }
        }

        internal void SuperTrophyButton(object sender, RoutedEventArgs e)
        {
            if (LoggedInOwner.SuperOwner)
            {
                MessageBox.Show($"You are already a Super owner\nNumber of ratings: {RatingsNumber}\nAverage Rating: {AverageScore:F2}",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if(isSuperOwner == false) {
                   MessageBox.Show($"You do not meet the requirements to become a Super owner\nNumber of ratings: {RatingsNumber}\nAverage Rating: {AverageScore:F2}",
                      "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"You have become a Super owner\nNumber of ratings: {RatingsNumber}\nAverage Rating: {AverageScore:F2}",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    LoggedInOwner.SuperOwner = true;
                    _ownerService.Update(LoggedInOwner);
                }
            }
        }

        internal void AccommodationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e, AccommodationsPage accommodationsPage)
        {
            ViewAccommodationPage page = ShowViewAccommodation(sender, e);
            if (page != null)
            {
                accommodationsPage.NavigationService.Navigate(page);
            }
        }
    }
}
