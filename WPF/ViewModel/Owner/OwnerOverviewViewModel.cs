using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class OwnerOverviewViewModel
    {
        public Domain.Model.Owner LoggedInOwner { get; set; }
        public bool isSuperOwner { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public List<AccommodationReservation> OwnerAccommodationReservations { get; set; }  
        public Accommodation SelectedAccommodation { get; set; }
        private static AccommodationService _accommodationService;
        private static AccommodationReservationService _accommodationReservationService;
        private static AccommodationAndOwnerRatingService _accommodationAndOwnerRatingService;
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
        }

        private void InitializeAccommodationReservaions()
        {
            OwnerAccommodationReservations = _accommodationReservationService.GetByAccommodationIds(Accommodations);
        }

        private void InitializeServices()
        {
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationAndOwnerRatingService = new AccommodationAndOwnerRatingService(_accommodationReservationService);
            _ownerService = new OwnerService();
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

        public void ShowCreateAccommodationForm(object sender, RoutedEventArgs e)
        {
            AccommodationForm accommodationForm = new AccommodationForm(LoggedInOwner);
            accommodationForm.Show();
        }

        public void ShowViewAccommodation(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Please choose an accommodation to view!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ViewAccommodation viewAccommodationWindow = new ViewAccommodation(SelectedAccommodation);
                viewAccommodationWindow.Show();
            }
        }

        public void OwnerOverview_Loaded(object sender, RoutedEventArgs e)
        {
            NotifyMissingRatings(Accommodations);
        }

        public void NotifyMissingRatings(ObservableCollection<Accommodation> accommodations)
        {
            var missingRatingReservations = _accommodationReservationService.GetUnratedReservations(Accommodations);

            if (missingRatingReservations.Any())
            {
                MessageBox.Show("You have recent unrated reservations",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void ShowRatingsButton(object sender, RoutedEventArgs e)
        {
            ViewRatings viewRatingsWindow = new ViewRatings(LoggedInOwner, OwnerAccommodationReservations);
            viewRatingsWindow.Show();
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

        internal void ReschedulingButton(object sender, RoutedEventArgs e)
        {
            ReschedulingOverview reschedulingOverviewWindow = new ReschedulingOverview(OwnerAccommodationReservations);
            reschedulingOverviewWindow.Show();  
        }
    }
}
