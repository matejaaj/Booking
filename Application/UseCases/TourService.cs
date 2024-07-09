using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourService
    {
        private ITourRepository _tourRepository;
        private readonly TourGuestService _tourGuestService;
        private readonly TourInstanceService _tourInstanceService;

        public TourService()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourGuestService = new TourGuestService();
            _tourInstanceService = new TourInstanceService();
        }
        public TourService(ITourRepository tourRepository, TourGuestService tourGuest, TourInstanceService tourInstance)
        {
            _tourRepository = tourRepository;
            _tourGuestService =  tourGuest;
            _tourInstanceService = tourInstance;
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public Tour Save(Tour tour)
        {
            return _tourRepository.Save(tour);
        }

        public void Delete(Tour tour)
        {
            _tourRepository.Delete(tour);
        }

        public Tour Update(Tour tour)
        {
            return _tourRepository.Update(tour);
        }

        public int NextId()
        {
            return _tourRepository.NextId();
        }
      
        public Tour GetById(int id)
        {
            return _tourRepository.GetById(id);
        }

        public Tour FindMostVisited(object SelectedYear)
        {
            int mostTourist = 0;
            var tours = GetAll();
            Tour mostVisitedTour = tours[0];

            foreach (var tour in tours)
            {
                int currentTourTourists = CalculateTotalTouristsForTour(tour, SelectedYear);
                if (currentTourTourists > mostTourist)
                {
                    mostTourist = currentTourTourists;
                    mostVisitedTour = tour;
                }
            }

            return mostVisitedTour;
        }

        private int CalculateTotalTouristsForTour(Tour tour, object SelectedYear)
        {
            var tourInstances = _tourInstanceService.GetAll();
            return tourInstances
                .Where(instance => ShouldIncludeInstance(instance, SelectedYear))
                .Where(instance => instance.TourId == tour.Id)
                .Sum(instance => CalculateTotalTouristsForInstance(instance));
        }

        private bool ShouldIncludeInstance(TourInstance instance, object SelectedYear)
        {
            return (SelectedYear.Equals("All time") || instance.StartTime.Year.ToString().Equals(SelectedYear)) && instance.IsCompleted;
        }

        private int CalculateTotalTouristsForInstance(TourInstance instance)
        {
            var tourGuests = _tourGuestService.GetAll();
            return tourGuests.Count(guest => guest.TourReservationId == instance.Id);
        }

        public List<Tour> LoadTodayTours()
        {
            List<Tour> todayTours = new List<Tour>();

            foreach (var tourInstance in _tourInstanceService.GetAll())
            {
                if (tourInstance.StartTime.Date == DateTime.Now.Date && !tourInstance.IsCompleted)
                {
                    var matchingTour = GetAll().FirstOrDefault(tour => tour.Id == tourInstance.TourId);
                    if (matchingTour != null)
                        todayTours.Add(matchingTour);
                }
            }

            return todayTours;
        }

        public int GetTotalGuidedTours(int guideId, int languageId)
        {
            int totalGuidedTours = 0;
            var appropriateTours = GetAll().Where(t => t.LanguageId == languageId).ToList();

            foreach (var instance in _tourInstanceService.GetAll())
            {
                if (appropriateTours.Any(t => t.Id == instance.TourId)){
                    if(instance.GuideId == guideId && instance.StartTime.AddYears(1) > DateTime.Now)
                        totalGuidedTours++;
                }
            }

            return totalGuidedTours;
        }

        public int GetTotalGuidedToursSinceSuperStatus(int guideId, int languageId, DateTime achievedDate)
        {
            int totalGuidedTours = 0;
            var appropriateTours = GetAll().Where(t => t.LanguageId == languageId).ToList();

            foreach (var instance in _tourInstanceService.GetAll())
            {
                if (appropriateTours.Any(t => t.Id == instance.TourId))
                {
                    if (instance.GuideId == guideId && instance.StartTime > achievedDate)
                        totalGuidedTours++;
                }
            }

            return totalGuidedTours;
        }

        
        public List<int> GetInstancesSinceSuperStatus(int guideId, int languageId, DateTime accuiredDate)
        {
            List<int> ids = new List<int>();
            var appropriateTours = GetAll().Where(t => t.LanguageId == languageId).ToList();

            foreach (var instance in _tourInstanceService.GetAll())
            {
                if (appropriateTours.Any(t => t.Id == instance.TourId))
                {
                    if (instance.GuideId == guideId && instance.StartTime > accuiredDate)
                        ids.Add(instance.Id);
                }
            }
            return ids;
        }

        public List<int> GetInstances(int guideId, int languageId)
        {
            List<int> ids = new List<int>();
            var appropriateTours = GetAll().Where(t => t.LanguageId == languageId).ToList();

            foreach (var instance in _tourInstanceService.GetAll())
            {
                if (appropriateTours.Any(t => t.Id == instance.TourId))
                {
                    if (instance.GuideId == guideId && instance.StartTime.AddYears(1) > DateTime.Now)
                        ids.Add(instance.Id);
                }
            }
            return ids;
        }
    }
}
