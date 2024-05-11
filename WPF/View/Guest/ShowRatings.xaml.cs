using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
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
    /// Interaction logic for ShowRatings.xaml
    /// </summary>
    public partial class ShowRatings : Window
    {
        private readonly GuestRatingService _ratingService;
        private readonly AccommodationReservationService _reservationService;
        private readonly AccommodationService _accommodationService;
        private readonly User loggedInGuest;

        public ShowRatings(User loggedInGuest)
        {
            InitializeComponent();
            _accommodationService = new AccommodationService();
            //_ratingService = new GuestRatingService();
            //_reservationService = new AccommodationReservationService();
            this.loggedInGuest = loggedInGuest;
            LoadRatings();
        }


        private void LoadRatings()
        {
            List<GuestRating> ratings = _ratingService.GetAll();
            List<Accommodation> accommodations = _accommodationService.GetAll(); 
            List<AccommodationReservation> reservations = _reservationService.GetAll();
            List<GuestRating> filteredRatings = new List<GuestRating>();
            List<GuestRatingsOverviewDTO> ratingsDTOs = new List<GuestRatingsOverviewDTO>();

            foreach (var reservation in reservations)
            {
                if (reservation.IsRated && reservation.IsAccommodationAndOwnerRated && reservation.GuestId == loggedInGuest.Id)
                {
                    var rating = ratings.Find(r => r.AccommodationReservationId == reservation.Id);
                    var accommodation = accommodations.Find(a => a.AccommodationId == reservation.AccommodationId);
                    if (rating != null && accommodation != null)
                    {
                        var ratingDTO = new GuestRatingsOverviewDTO(rating.Cleanliness, rating.RulesRespect, rating.Comment, accommodation.Name);
                        ratingsDTOs.Add(ratingDTO);
                    }
                }
            }

            if (ratingsDTOs.Count == 0)
            {
                MessageBox.Show("Nema ocena za prikaz.");
            }
            else
            {
                RatingsDataGrid.ItemsSource = ratingsDTOs;
            }
        }

    }
}
