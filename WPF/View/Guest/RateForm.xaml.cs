using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Guide;
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
    /// Interaction logic for RateForm.xaml
    /// </summary>
    public partial class RateForm : Window
    {
        public List<Domain.Model.Image> images { get; set; }

        private AccommodationReservation _reservationRepository;
        private static AccommodationAndOwnerRatingRepository _accommodationAndOwnerRatingRepository { get; set; }
        private static AccommodationReservationRepository _accommodationReservationRepository { get; set; }
        private int accommodationId;
        public RateForm(AccommodationReservation reservation)
        {
            InitializeComponent();
            images = new List<Domain.Model.Image>();
            DataContext = this;
            _accommodationAndOwnerRatingRepository = new AccommodationAndOwnerRatingRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _reservationRepository = reservation;
            accommodationId = reservation.AccommodationId;
        }
        private void AddPictures_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(images, accommodationId, ImageResourceType.ACCOMMODATION);
            addImageWindow.Owner = this;
            addImageWindow.ShowDialog();
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            int cleanliness = (int)CleanlinessSlider.Value;
            int ownersCorrectness = (int)OwnersCorrectnessSlider.Value;
            string comment = CommentsTextBox.Text;

            AccommodationAndOwnerRating newAccommodationAndOwnerRating = new AccommodationAndOwnerRating(_reservationRepository.ReservationId, cleanliness, ownersCorrectness, comment);
            _accommodationAndOwnerRatingRepository.Save(newAccommodationAndOwnerRating);
            _reservationRepository.IsAccommodationAndOwnerRated = true;
            _accommodationReservationRepository.Update(_reservationRepository);
            Close();
            MessageBox.Show("Accommodation and owner successfully rated.");
            this.Close();
        }
    }
}
