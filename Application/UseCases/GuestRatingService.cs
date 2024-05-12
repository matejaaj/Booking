using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.UseCases
{
    public class GuestRatingService
    {
        private readonly IGuestRatingRepository _guestRatingRepository;
        private AccommodationReservationService _reservationService;
        private AccommodationService _accommodationService;
        public GuestRatingService(IGuestRatingRepository guestRatingRepository)
        {
            _guestRatingRepository = guestRatingRepository;
        }
        public GuestRatingService(IGuestRatingRepository guestRatingRepository, AccommodationReservationService _reservationService, AccommodationService _accommodationService)
        {
            _guestRatingRepository = guestRatingRepository;
            this._reservationService = _reservationService;
            this._accommodationService = _accommodationService;
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatingRepository.GetAll();
        }

        public GuestRating Save(GuestRating guestRating)
        {
            return _guestRatingRepository.Save(guestRating);
        }

        public void Delete(GuestRating guestRating)
        {
            _guestRatingRepository.Delete(guestRating);
        }

        public GuestRating Update(GuestRating guestRating)
        {
            return _guestRatingRepository.Update(guestRating);
        }
        public List<GuestRatingsOverviewDTO> LoadRatings(User loggedInGuest)
        {
            List<GuestRating> ratings = GetAll();
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

            return ratingsDTOs;
        }
    }
}
