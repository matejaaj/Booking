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
using System.Windows;

namespace BookingApp.Application.UseCases
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private AccommodationReservationService _accommodationReservationService;
        private ReservationModificationRequestService _reservationModificationRequestService;
        private RenovationRecommendationService _renovationRecommendationService;
        private LocationService _locationService;
        private ImageService _imageService;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public AccommodationService(IAccommodationRepository accommodationRepository, ImageService imageService, LocationService locationService)
        {
            _accommodationRepository = accommodationRepository;
            _locationService = locationService;
            _imageService = imageService;
        }

        public AccommodationService(AccommodationReservationService accommodationReservationService, ReservationModificationRequestService reservationModificationRequestService, IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _accommodationReservationService = accommodationReservationService;
            _reservationModificationRequestService = reservationModificationRequestService;
        }

        public AccommodationService(AccommodationReservationService accommodationReservationService, ReservationModificationRequestService reservationModificationRequestService, RenovationRecommendationService renovationRecommendationService, IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
            _accommodationReservationService = accommodationReservationService;
            _reservationModificationRequestService = reservationModificationRequestService;
            _renovationRecommendationService = renovationRecommendationService;
        }

        public AccommodationService()
        {
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }

        public int NextId()
        {
            return _accommodationRepository.NextId();
        }

        public Accommodation Save(Accommodation accommodation)
        {
            return _accommodationRepository.Save(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return _accommodationRepository.Update(accommodation);
        }

        public List<Accommodation> GetByUser(User user)
        {
            return _accommodationRepository.GetByUser(user);
        }
        public Accommodation GetById(int id)
        {
            return _accommodationRepository.GetById(id);
        }

        public List<AccommodationStatisticsDTO> GenerateStatisticsByYear(Accommodation accommodation)
        {
            List<AccommodationStatisticsDTO> retVal = new List<AccommodationStatisticsDTO>();
            var bookings = _accommodationReservationService.GetByAccommodation(accommodation);
            for(int i = 2025; i >= 2020; i--)
            {
                int cancelations = 0;
                int bookingsNum = 0;
                int reschedulings = 0;
                int renovationSuggestions = 0;
                int totalDays = 0;
                foreach(var b in bookings)
                {
                    if (b.EndDate.Year == i)
                    {
                        bookingsNum++;
                        if (b.IsCancelled) cancelations++;
                        reschedulings += _reservationModificationRequestService.GetAllAcceptedWithReservationId(b.Id).Count();
                        renovationSuggestions += _renovationRecommendationService.GetAllWithReservationId(b.Id).Count();
                        totalDays += (int)(b.EndDate - b.StartDate).TotalDays;
                    }
                }
                retVal.Add(new AccommodationStatisticsDTO(i, bookingsNum, cancelations, reschedulings, renovationSuggestions, "N/A", totalDays));
            }
            return retVal;
        }

        internal List<AccommodationStatisticsDTO> GenerateStatisticsByMonth(AccommodationStatisticsDTO selectedYear, Accommodation accommodation)
        {
            List<AccommodationStatisticsDTO> retVal = new List<AccommodationStatisticsDTO>();
            var bookings = _accommodationReservationService.GetByAccommodation(accommodation).FindAll(b => b.EndDate.Year == selectedYear.Year);
            
            for (int i = 1; i <= 12; i++)
            {
                int cancelations = 0;
                int bookingsNum = 0;
                int reschedulings = 0;
                int renovationSuggestions = 0;
                int totalDays = 0;
                foreach (var b in bookings)
                {
                    if (b.EndDate.Month == i)
                    {
                        bookingsNum++;
                        if (b.IsCancelled) cancelations++;
                        reschedulings += _reservationModificationRequestService.GetAllAcceptedWithReservationId(b.Id).Count();
                        renovationSuggestions += _renovationRecommendationService.GetAllWithReservationId(b.Id).Count();
                        totalDays += (int)(b.EndDate - b.StartDate).TotalDays;
                    }
                }
                retVal.Add(new AccommodationStatisticsDTO(selectedYear.Year, bookingsNum, cancelations, reschedulings, renovationSuggestions, i.ToString(), totalDays));
            }
            return retVal;
        }

        internal string GetBusiestYear(ObservableCollection<AccommodationStatisticsDTO> yearlyStats)
        {
            var retValue = yearlyStats.OrderByDescending(stat => stat.Busyness).FirstOrDefault();
            return retValue.Year.ToString();
        }

        internal string GetBusiestMonth(ObservableCollection<AccommodationStatisticsDTO> monthlyStats)
        {
            var retValue = monthlyStats.OrderByDescending(stat => stat.Busyness).FirstOrDefault();
            return retValue.Month.ToString();
        }
        public List<AccommodationPageDTO> GetByUserWithLocationAndImage(User loggedInOwner)
        {
            var accommodations = _accommodationRepository.GetByUser(loggedInOwner);
            List<AccommodationPageDTO> retValue = new List<AccommodationPageDTO>();
            foreach (var a in accommodations)
            {
                var location = _locationService.GetLocationById(a.LocationId);
                List<Image> images = _imageService.GetImagesByEntityAndType(a.AccommodationId, ImageResourceType.ACCOMMODATION);
                var imagePaths = images?.Select(i => i.Path).ToList() ?? new List<string>();
                retValue.Add(new AccommodationPageDTO(a, location, imagePaths));
            }
            return retValue;
        }

        public AccommodationPageDTO GetDisplayDTOById(int targetUserId)
        {
            var accommodation = _accommodationRepository.GetById(targetUserId);
            var location = _locationService.GetLocationById(accommodation.LocationId);
            List<Image> images = _imageService.GetImagesByEntityAndType(accommodation.AccommodationId, ImageResourceType.ACCOMMODATION);
            var imagePaths = images?.Select(i => i.Path).ToList() ?? new List<string>();
            return new AccommodationPageDTO(accommodation, location, imagePaths);
        }
    }
}
