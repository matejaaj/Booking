using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Application.UseCases
{
    public class AccommodationAndOwnerRatingService
    {
        private readonly IAccommodationAndOwnerRatingRepository _ratingRepository;
        private AccommodationReservationService accommodationReservationService;

        public AccommodationAndOwnerRatingService(IAccommodationAndOwnerRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public AccommodationAndOwnerRatingService(AccommodationReservationService accommodationReservationService, IAccommodationAndOwnerRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
            this.accommodationReservationService = accommodationReservationService;
        }

        public List<AccommodationAndOwnerRating> GetAll()
        {
            return _ratingRepository.GetAll();
        }

        public AccommodationAndOwnerRating Save(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
            return _ratingRepository.Save(accommodationAndOwnerRating);
        }

        public void Delete(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
            _ratingRepository.Delete(accommodationAndOwnerRating);
        }

        public AccommodationAndOwnerRating Update(AccommodationAndOwnerRating accommodationAndOwnerRating)
        {
           return _ratingRepository.Update(accommodationAndOwnerRating);
        }

        public AccommodationAndOwnerRating GetById(int id)
        {
           return _ratingRepository.GetById(id);
        }

        public AccommodationAndOwnerRating GetByReservationId(int id)
        {
            return _ratingRepository.GetByReservationId(id);
        }

        public List<AccommodationAndOwnerRating> GetByReservationIds(List<int> accommodationReservationIds)
        {
            return _ratingRepository.GetByReservationIds(accommodationReservationIds);
        }

        public List<AccommodationAndOwnerRating> GetByReservations(List<AccommodationReservation> accommodationReservations)
        {
            var accommodationReservationsIds = accommodationReservations.Select(a => a.Id).ToList();
            var ratings  = GetByReservationIds(accommodationReservationsIds);
            return ratings;
        }

        public List<double> CalculateIndividualAverages(List<AccommodationAndOwnerRating> accommodationAndOwnerRatings)
        {
            List<double> individualAverages = new List<double>();
            foreach (var r in accommodationAndOwnerRatings)
            {
                individualAverages.Add((double)(r.Cleanliness + r.OwnershipEthics) / 2);
            }
            return individualAverages;
        }

        public double CalculateAverageScore(List<double> individualAverages, int ratingsNumber)
        {
            return (double)individualAverages.Sum(a => a) / ratingsNumber;
        }
        internal List<OwnerPdfDTO> GetAverageScores(List<Accommodation> accommodations)
        {
            List<OwnerPdfDTO> retVal = new List<OwnerPdfDTO> ();
            foreach (var accommodation in accommodations)
            {
                var reservations = accommodationReservationService.GetByAccommodation(accommodation);
                int ratingsNumber = GetByReservations(reservations).Count();
                var individualAverages = CalculateIndividualAverages(GetByReservations(reservations));
                double averageScore = CalculateAverageScore(individualAverages, ratingsNumber);
                retVal.Add(new OwnerPdfDTO(accommodation, averageScore));
            }
            return retVal;
        }
    }
}
