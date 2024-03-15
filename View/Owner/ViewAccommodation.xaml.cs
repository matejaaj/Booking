using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for ViewAccommodation.xaml
    /// </summary>
    public partial class ViewAccommodation : Window
    {
        public static Accommodation Accommodation { get; set;}
        private static AccommodationReservationRepository _accommodationReservationRepository { get; set;}
        private static GuestRatingRepository _guestRatingRepository { get; set;}   
        private List<GuestRating> _guestRatings;
        private ObservableCollection<AccommodationReservation> RecentAccommodationReservations { get; set; }
        private ObservableCollection<AccommodationReservation> PastAccommodationReservations { get; set;}
        private ObservableCollection<AccommodationReservation> OtherAccommodationReservations { get; set; }
        private bool IsNew { get; set; }
        public ViewAccommodation(Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            Accommodation = accommodation;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _guestRatingRepository = new GuestRatingRepository();

            _guestRatings = _guestRatingRepository.GetAll();
            var allReservations = _accommodationReservationRepository.GetAll();

            RecentAccommodationReservations = new ObservableCollection<AccommodationReservation>(
                allReservations.Where(reservation => IsReservationRecent(reservation, Accommodation))
            );

            PastAccommodationReservations = new ObservableCollection<AccommodationReservation>(
                allReservations.Where(reservation => IsReservationPast(reservation,Accommodation))
            );


            OtherAccommodationReservations = new ObservableCollection<AccommodationReservation>(
                allReservations.Where(reservation => IsReservationOther(reservation, Accommodation))
            );

            RecentReservationsListBox.ItemsSource = RecentAccommodationReservations;
            PastReservationsListBox.ItemsSource = PastAccommodationReservations;
            OtherReservationsListBox.ItemsSource = OtherAccommodationReservations;
        }

        private bool IsReservationRecent(AccommodationReservation reservation, Accommodation accommodation)
        {
            return (DateTime.Now - reservation.EndDate).TotalDays <= 5 &&
                reservation.EndDate < DateTime.Now &&
                reservation.AccommodationId == Accommodation.AccommodationId;
        }

        private bool IsReservationRated(AccommodationReservation accommodationReservation)
        {
            var selectedAccommodationRating = _guestRatings.Find(rating => accommodationReservation.AccommodationId == rating.GuestRatingId);
            return !(selectedAccommodationRating == null);
        }

        private bool IsReservationPast(AccommodationReservation reservation, Accommodation accommodation)
        {
            return (DateTime.Now - reservation.EndDate).TotalDays > 5 && 
                reservation.EndDate < DateTime.Now &&
                reservation.AccommodationId == accommodation.AccommodationId;
        }

        private bool IsReservationOther(AccommodationReservation reservation, Accommodation accommodation)
        {
            return reservation.EndDate >= DateTime.Now &&
                reservation.AccommodationId == accommodation?.AccommodationId;
        }

        private void RecentReservationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecentReservationsListBox.SelectedItem != null)
            {
                var selectedReservation = (AccommodationReservation)RecentReservationsListBox.SelectedItem;
                if (!IsReservationRated(selectedReservation))
                {
                    var guestRatingFormWindow = new GuestRatingForm(selectedReservation);
                    guestRatingFormWindow.Owner = this;
                    guestRatingFormWindow.ShowDialog();
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
