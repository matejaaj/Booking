using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for PreviousReservations.xaml
    /// </summary>
    public partial class PreviousReservations : Window
    {
        private User guest;
        private AccommodationReservationRepository _accommodationReservationRepository = new AccommodationReservationRepository();
        private List<AccommodationReservation> reservations;

        public PreviousReservations(User guest)
        {
            InitializeComponent();
            this.guest = guest;
            reservations = _accommodationReservationRepository.GetByUser(guest);
            ReservationsListBox.ItemsSource = reservations;
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AccommodationReservation reservation = button.Tag as AccommodationReservation;

            if ((DateTime.Now - reservation.EndDate).TotalDays > 5)
            {
                MessageBox.Show("Reservation cannot be rated because more than 5 days have passed since the end of the stay.");
                return;
            }
            else if (reservation.IsAccommodationAndOwnerRated == true)
            {
                MessageBox.Show("You have already rated the accommodation!");
                return;
            }
            else 
            {
                RateForm rateForm = new RateForm(reservation);
                rateForm.Show();
            }
        }
    }
}
