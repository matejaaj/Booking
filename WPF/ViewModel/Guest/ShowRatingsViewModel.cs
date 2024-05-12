using BookingApp.Application.UseCases;
using BookingApp.Application;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using BookingApp.WPF.View.Guest;

namespace BookingApp.WPF.ViewModel.Guest
{
    internal class ShowRatingsViewModel
    {
        private GuestRatingService _ratingService;
        public ObservableCollection<GuestRatingsOverviewDTO> GuestsRating { get; set; }

        public ShowRatingsViewModel(User loggedInGuest)
        {
            InitializeServices();
            var ratings = _ratingService.LoadRatings(loggedInGuest);
            if(ratings.Count == 0)
            {
                MessageBox.Show("Nema ocena za prikaz.");
            }
            else
            {
                GuestsRating = new ObservableCollection<GuestRatingsOverviewDTO>(ratings);
            }
        }
        private void InitializeServices()
        {
            var _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            var _reservationService = new AccommodationReservationService(_accommodationService, Injector.CreateInstance<IAccommodationReservationRepository>());
            _ratingService = new GuestRatingService(Injector.CreateInstance<IGuestRatingRepository>(), _reservationService, _accommodationService);

        }

    }
}
