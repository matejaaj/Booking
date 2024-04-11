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
        public User LoggedInOwner { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _accommodationReservationService;

        public OwnerOverviewViewModel(User owner)
        {
            LoggedInOwner = owner;
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetByUser(LoggedInOwner));
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
            var accommodationIds = accommodations.Select(a => a.AccommodationId).ToList();
            var ownerAccommodationReservations = _accommodationReservationService.GetByAccommodationIds(accommodationIds);

            var MissingRatingReservations = new List<AccommodationReservation>(
                 ownerAccommodationReservations.Where(reservation => (DateTime.Now - reservation.EndDate).TotalDays <= 5 &&
                reservation.EndDate < DateTime.Now && reservation.IsRated == false)
             );

            if (MissingRatingReservations.Any())
            {
                MessageBox.Show("You have recent unrated reservations",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void ShowRatingsButton(object sender, RoutedEventArgs e)
        {
            var accommodationIds = Accommodations.Select(a => a.AccommodationId).ToList();
            var ownerAccommodationReservations = _accommodationReservationService.GetByAccommodationIds(accommodationIds);
            ViewRatings viewRatingsWindow = new ViewRatings(LoggedInOwner, ownerAccommodationReservations);
            viewRatingsWindow.Show();
        }
    }
}
