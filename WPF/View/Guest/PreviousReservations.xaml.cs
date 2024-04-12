using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.View.Guest
{
    public partial class PreviousReservations : Window
    {
        private User guest;
        private AccommodationReservationRepository _accommodationReservationRepository = new AccommodationReservationRepository();
        private List<AccommodationReservation> reservations;
        private AccommodationRepository _accommodationRepository = new AccommodationRepository();
        private ReservationModificationRequestRepository _reservationModificationRequestRepository = new ReservationModificationRequestRepository();

        public PreviousReservations(User guest)
        {
            InitializeComponent();
            this.guest = guest;
            reservations = _accommodationReservationRepository.GetByUser(guest);
            List<ReservationDisplayDTO> reservationInfos = FindReservationInfos(reservations);
            ReservationsListBox.ItemsSource = reservationInfos;
        }

        private List<ReservationDisplayDTO> FindReservationInfos(List<AccommodationReservation> reservations)
        {
            List<ReservationDisplayDTO> reservationInfos = new List<ReservationDisplayDTO>();
            foreach (var reservation in reservations)
            {
                Accommodation accommodation = _accommodationRepository.GetById(reservation.AccommodationId);
                string accommodationName = accommodation != null ? accommodation.Name : "Unknown";

                // Dohvati zahtev za modifikaciju rezervacije ako postoji
                ReservationModificationRequest modificationRequest = _reservationModificationRequestRepository.GetByReservationId(reservation.Id);

                var reservationDto = new ReservationDisplayDTO(accommodationName, reservation.StartDate, reservation.EndDate, reservation,
                                                                modificationRequest?.OwnerComment ?? "", modificationRequest?.Status.ToString() ?? "");
                reservationInfos.Add(reservationDto);
            }
            return reservationInfos;
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ReservationDisplayDTO reservationDto = button.Tag as ReservationDisplayDTO;

            if ((DateTime.Now - reservationDto.Reservation.EndDate).TotalDays > 5)
            {
                MessageBox.Show("Reservation cannot be rated because more than 5 days have passed since the end of the stay.");
                return;
            }
            else if (reservationDto.Reservation.IsAccommodationAndOwnerRated)
            {
                MessageBox.Show("You have already rated the accommodation!");
                return;
            }
            else
            {
                RateForm rateForm = new RateForm(reservationDto.Reservation);
                rateForm.Show();
            }
        }

        private void RequestModificationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ReservationDisplayDTO reservationDto = button.Tag as ReservationDisplayDTO;
            RequestModification requestModification = new RequestModification(reservationDto.Reservation);
            requestModification.Show();
        }
    }
}
