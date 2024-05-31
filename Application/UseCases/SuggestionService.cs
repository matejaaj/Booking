using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class SuggestionService
    {
        private LocationService _locationService;
        private AccommodationService _accommodationService;
        private AccommodationReservationService _accommodationReservationService;

        public SuggestionService() { }

        public SuggestionService(LocationService locationService, AccommodationService accommodationService, AccommodationReservationService accommodationReservationService)
        {
            this._locationService = locationService;
            this._accommodationService = accommodationService;
            this._accommodationReservationService = accommodationReservationService;
        }

        public Location GetMostPopularLocation(Owner loggedInOwner)
        {
            Location retVal = null;
            double highestScore = double.MinValue;
            const double alpha = 0.5; 
            const double beta = 0.5;  

            var accommodations = _accommodationService.GetByUser(loggedInOwner);
            var reservations = _accommodationReservationService.GetByOwner(loggedInOwner);
            var locations = _locationService.GetAll();

            foreach (var location in locations)
            {
                var locationAccommodations = accommodations.Where(a => a.LocationId == location.Id).ToList();
                int reservationNumber = 0;
                double business = 0;

                foreach (var accommodation in locationAccommodations)
                {
                    var accommodationReservations = reservations.Where(r => r.AccommodationId == accommodation.AccommodationId).ToList();
                    reservationNumber += accommodationReservations.Count;
                    foreach (var reservation in accommodationReservations)
                    {
                        business += (double)reservation.GuestNumber / accommodation.MinReservations;
                    }
                }

                double score = alpha * reservationNumber + beta * business;

                if (score > highestScore)
                {
                    highestScore = score;
                    retVal = location;
                }
            }
            return retVal;
        }


        public Location GetLeastPopularLocation(Owner loggedInOwner)
        {
            Location retVal = null;
            double lowestScore = double.MaxValue;
            const double alpha = 0.5; 
            const double beta = 0.5; 

            var accommodations = _accommodationService.GetByUser(loggedInOwner);
            var reservations = _accommodationReservationService.GetByOwner(loggedInOwner);
            var locations = _locationService.GetAll();

            foreach (var location in locations)
            {
                var locationAccommodations = accommodations.Where(a => a.LocationId == location.Id).ToList();
                int reservationNumber = 0;
                double business = 0;

                foreach (var accommodation in locationAccommodations)
                {
                    var accommodationReservations = reservations.Where(r => r.AccommodationId == accommodation.AccommodationId).ToList();
                    reservationNumber += accommodationReservations.Count;
                    foreach (var reservation in accommodationReservations)
                    {
                        business += (double)reservation.GuestNumber / accommodation.MinReservations;
                    }
                }

                double score = alpha * reservationNumber + beta * business;

                if (score < lowestScore)
                {
                    lowestScore = score;
                    retVal = location;
                }
            }
            return retVal;
        }

    }
}
