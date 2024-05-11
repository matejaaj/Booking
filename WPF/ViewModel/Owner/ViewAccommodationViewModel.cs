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
        private bool IsNew { get; set; }

        public ViewAccommodationViewModel(Accommodation accommodation)
        {
            Accommodation = accommodation;
            _accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            _guestRatingService = new GuestRatingService(Injector.CreateInstance<IGuestRatingRepository>());
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
            RenovationSchedulingPage page = new RenovationSchedulingPage(Accommodation);
            viewAccommodationPage.NavigationService.Navigate(page);
        }

        internal void Statistics_Click(object sender, RoutedEventArgs e, ViewAccommodationPage viewAccommodationPage)
        {
            AccommodationStatsPage page = new AccommodationStatsPage(Accommodation);
            viewAccommodationPage.NavigationService.Navigate(page);
        }
    }
}
