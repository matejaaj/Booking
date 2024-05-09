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

        public AccommodationService()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public AccommodationService(AccommodationReservationService accommodationReservationService, ReservationModificationRequestService reservationModificationRequestService)
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _accommodationReservationService = accommodationReservationService;
            _reservationModificationRequestService = reservationModificationRequestService;
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
    }
}
