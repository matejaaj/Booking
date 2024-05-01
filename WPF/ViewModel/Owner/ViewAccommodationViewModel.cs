using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
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

namespace BookingApp.WPF.ViewModel.Owner
{
    public class ViewAccommodationViewModel : INotifyPropertyChanged
    {
        public static Accommodation Accommodation { get; set; }
        private static AccommodationReservationService _accommodationReservationService;
        private static GuestRatingService _guestRatingService;
        private List<GuestRating> _guestRatings;

        private ObservableCollection<AccommodationReservation> _recentAccommodationReservations;
        public ObservableCollection<AccommodationReservation> RecentAccommodationReservations
        {
            get { return _recentAccommodationReservations; }
            set
            {
                _recentAccommodationReservations = value;
                OnPropertyChanged(nameof(RecentAccommodationReservations));
            }
        }

        private ObservableCollection<AccommodationReservation> _pastAccommodationReservations;
        public ObservableCollection<AccommodationReservation> PastAccommodationReservations
        {
            get { return _pastAccommodationReservations; }
            set
            {
                _pastAccommodationReservations = value;
                OnPropertyChanged(nameof(PastAccommodationReservations));
            }
        }

        private ObservableCollection<AccommodationReservation> _otherAccommodationReservations;
        public ObservableCollection<AccommodationReservation> OtherAccommodationReservations
        {
            get { return _otherAccommodationReservations; }
            set
            {
                _otherAccommodationReservations = value;
                OnPropertyChanged(nameof(OtherAccommodationReservations));
            }
        }
        private bool IsNew { get; set; }

        public ViewAccommodationViewModel(Accommodation accommodation)
        {
            Accommodation = accommodation;
            _accommodationReservationService = new AccommodationReservationService();
            _guestRatingService = new GuestRatingService();
            _guestRatings = _guestRatingService.GetAll();
            FillReservations();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FillReservations()
        {
            RecentAccommodationReservations = new ObservableCollection<AccommodationReservation>(
                _accommodationReservationService.GetRecentReservations(Accommodation));

            PastAccommodationReservations = new ObservableCollection<AccommodationReservation>(
                _accommodationReservationService.GetPastReservations(Accommodation));

            OtherAccommodationReservations = new ObservableCollection<AccommodationReservation>(
                _accommodationReservationService.GetOtherReservations(Accommodation));
        }

/*        private bool IsReservationRated(AccommodationReservation accommodationReservation)
        {
            var selectedAccommodationRating = _guestRatings.Find(rating => accommodationReservation.AccommodationId == rating.GuestRatingId);
            return !(selectedAccommodationRating == null);
        }*/

        public void RecentReservationsListBox_SelectionChanged(object sender, Window owner, ListBox RecentReservationsListBox)
        {
            if (RecentReservationsListBox.SelectedItem != null)
            {
                var selectedReservation = (AccommodationReservation)RecentReservationsListBox.SelectedItem;
                if (!selectedReservation.IsRated)
                {
                    var guestRatingFormWindow = new GuestRatingForm(selectedReservation);
                    guestRatingFormWindow.Owner = owner;
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
    }
}
