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
    }
}
