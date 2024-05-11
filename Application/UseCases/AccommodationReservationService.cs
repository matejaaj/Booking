using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private AccommodationService accommodationService;

        public AccommodationReservationService(IAccommodationReservationRepository accommodationReservationRepository)
        {
            _accommodationReservationRepository = accommodationReservationRepository;
        }

        public AccommodationReservationService(AccommodationService accommodationService, IAccommodationReservationRepository accommodationReservationRepository)
        {
            this.accommodationService = accommodationService;
            _accommodationReservationRepository = accommodationReservationRepository;
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationRepository.GetAll();
        }

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
           return _accommodationReservationRepository.Save(accommodationReservation);
        }

        public void Delete(AccommodationReservation accommodationReservation)
        {
           _accommodationReservationRepository.Delete(accommodationReservation);
        }

        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationRepository.Update(accommodationReservation);
        }

        public List<AccommodationReservation> GetByUser(User user)
        {
           return _accommodationReservationRepository.GetByUser(user);
        }

        public List<AccommodationReservation> GetByAccommodationIds(List<int> accommodationIds)
        {
            return _accommodationReservationRepository.GetByAccommodationIds(accommodationIds);
        }

        public List<AccommodationReservation> GetByAccommodationIds(ObservableCollection<Accommodation> accommodations)
        {
            var accommodationIds = accommodations.Select(a => a.AccommodationId).ToList();
            return _accommodationReservationRepository.GetByAccommodationIds(accommodationIds);
        }

        public List<AccommodationReservation> GetByAccommodationId(int accommodationId)
        {
            return _accommodationReservationRepository.GetByAccommodationId(accommodationId);
        }

        public AccommodationReservation GetByReservationId(int id)
        {
            return _accommodationReservationRepository.GetByReservationId(id);
        }

        public List<AccommodationReservation> GetUnratedReservations(ObservableCollection<Accommodation> accommodations)
        {
            var accommodationIds = accommodations.Select(a => a.AccommodationId).ToList();
            List<AccommodationReservation> OwnerAccommodationReservations = GetByAccommodationIds(accommodationIds);
            return OwnerAccommodationReservations.Where(reservation => (DateTime.Now - reservation.EndDate).TotalDays <= 5 && 
            reservation.EndDate < DateTime.Now && reservation.IsRated == false).ToList();
        }

        public IEnumerable<BookingDTO> GetRecentReservations(Accommodation accommodation)
        {
            var bookings = new List<BookingDTO>();
            var allReservations = GetByAccommodation(accommodation);
            foreach(var reservation in allReservations)
            {
                if (IsReservationRecent(reservation, accommodation))
                {
                    bookings.Add(new BookingDTO(reservation));
                }
            }
            return bookings;
        }

        private bool IsReservationRecent(AccommodationReservation reservation, Accommodation accommodation)
        {
            return (DateTime.Now - reservation.EndDate).TotalDays <= 5 &&
                reservation.EndDate < DateTime.Now &&
                reservation.AccommodationId == accommodation.AccommodationId;
        }

        public List<AccommodationReservation> GetByAccommodation(Accommodation accommodation)
        {
            return GetByAccommodationId(accommodation.AccommodationId);
        }

        public IEnumerable<BookingDTO> GetPastReservations(Accommodation accommodation)
        {
            var bookings = new List<BookingDTO>();
            var allReservations = GetByAccommodation(accommodation);
            foreach (var reservation in allReservations)
            {
                if (IsReservationPast(reservation, accommodation))
                {
                    bookings.Add(new BookingDTO(reservation));
                }
            }
            return bookings;
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

        public IEnumerable<BookingDTO> GetOtherReservations(Accommodation accommodation)
        {
            var bookings = new List<BookingDTO>();
            var allReservations = GetByAccommodation(accommodation);
            foreach (var reservation in allReservations)
            {
                if (IsReservationOther(reservation, accommodation))
                {
                    bookings.Add(new BookingDTO(reservation));
                }
            }
            return bookings;
        }

        internal bool IsDateReserved(DateTime startDate, DateTime endDate, DateTime oldStartDate, DateTime oldEndDate, List<AccommodationReservation> accommodationReservations)
        {
            foreach (var reservation in accommodationReservations)
            {
                if (((startDate <= reservation.EndDate && endDate >= reservation.StartDate) ||
                    (startDate >= reservation.StartDate && endDate <= reservation.EndDate))
                    && !isOldReservation(reservation, oldStartDate, oldEndDate))
                {
                    return true;
                }
            }

            return false;
        }

        private bool isOldReservation(AccommodationReservation reservation, DateTime oldStartDate, DateTime oldEndDate)
        {
            return (reservation.StartDate == oldStartDate && reservation.EndDate == oldEndDate);
        }

        internal List<AccommodationReservation> GetByOwner(Owner loggedInOwner)
        {
            var accommodations = accommodationService.GetByUser(loggedInOwner);
            var accommodationIds = accommodations.Select(a => a.AccommodationId).ToList();
            return _accommodationReservationRepository.GetByAccommodationIds(accommodationIds);
        }
    }
}
