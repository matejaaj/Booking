using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class RenovationService
    {
        private readonly IRenovationRepository _repository;
        private AccommodationService accommodationService;
        private AccommodationReservationService accommodationReservationService;
        private LocationService locationService;

        public RenovationService(IRenovationRepository repository)
        {
            _repository = repository;
        }

        public RenovationService(AccommodationService accommodationService, AccommodationReservationService accommodationReservationService, LocationService locationService, IRenovationRepository repository)
        {
            _repository = repository;
            this.accommodationService = accommodationService;
            this.accommodationReservationService = accommodationReservationService;
            this.locationService = locationService;
        }

        public RenovationService(AccommodationService accommodationService, AccommodationReservationService accommodationReservationService, IRenovationRepository repository)
        {
            _repository = repository;
            this.accommodationService = accommodationService;
            this.accommodationReservationService = accommodationReservationService;
            this.locationService = new LocationService();
        }

        public List<Renovation> GetAll()
        {
           return _repository.GetAll();
        }

        public Renovation Save(Renovation renovation)
        {
            return _repository.Save(renovation);
        }

        public void Delete(Renovation renovation)
        {
            _repository.Delete(renovation);
        }

        public Renovation Update(Renovation renovation)
        {
            return _repository.Update(renovation);
        }

        internal List<FreeDatesDTO> FindAvailableDates(DateTime from, DateTime to, int estimatedDuration, Accommodation accommodation)
        {
            List<FreeDatesDTO> dates = new List<FreeDatesDTO>();
            var allReservations = accommodationReservationService.GetByAccommodation(accommodation);

            DateTime currentStartDate = from;
            DateTime currentEndDate = from.AddDays(estimatedDuration - 1);

            while(currentEndDate <= to)
            {
                if (!isReserved(currentStartDate, currentEndDate, allReservations))
                {
                    dates.Add(new FreeDatesDTO(currentStartDate, currentEndDate));
                }
                currentStartDate = currentStartDate.AddDays(1);
                currentEndDate = currentStartDate.AddDays(estimatedDuration - 1);
            }
            return dates;
        }

        private bool isReserved(DateTime currentStartDate, DateTime currentEndDate, List<AccommodationReservation> allReservations)
        {
            if (allReservations.Any(reservation =>
                (currentStartDate <= reservation.EndDate && currentEndDate >= reservation.StartDate) ||
                (currentStartDate >= reservation.StartDate && currentEndDate <= reservation.EndDate)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal List<RenovationDTO> GetPastRenovations(Owner loggedInOwner)
        {
            List<RenovationDTO> retValue = new List<RenovationDTO>();
            var renovations = GetAllByOwner(loggedInOwner);
            foreach (var r in renovations)
            {
                if (isPast(r)){
                    var accommodation = accommodationService.GetById(r.AccommodationId);
                    var location = locationService.GetLocationById(accommodation.LocationId);
                    string nameAndLocation = accommodation.Name + " - " + location;
                    retValue.Add(new RenovationDTO(r.Id, r.StartDate, r.EndDate, nameAndLocation));
                }
            }
            return retValue;
        }

        private bool isPast(Renovation r)
        {
            if(r.EndDate < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Renovation> GetAllByOwner(Owner loggedInOwner)
        {
            var accommodations = accommodationService.GetByUser(loggedInOwner);
            var renovations = GetAll();
            List<Renovation> retValue = renovations.Where(r => accommodations.Any(a => a.AccommodationId == r.AccommodationId)).ToList();
            return retValue;
        }

        internal List<RenovationDTO> GetCurrentAndFutureRenovations(Owner loggedInOwner)
        {
            List<RenovationDTO> retValue = new List<RenovationDTO>();
            var renovations = GetAllByOwner(loggedInOwner);
            foreach (var r in renovations)
            {
                if (!isPast(r))
                {
                    var accommodation = accommodationService.GetById(r.AccommodationId);
                    var location = locationService.GetLocationById(accommodation.LocationId);
                    string nameAndLocation = accommodation.Name + " - " + location;
                    retValue.Add(new RenovationDTO(r.Id, r.StartDate, r.EndDate, nameAndLocation));
                }
            }
            return retValue;
        }

        internal void DeleteById(int id)
        {
            var renovations = GetAll();
            var renovation = renovations.Find(r => r.Id == id);
            _repository.Delete(renovation);
        }
    }
}
