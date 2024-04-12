using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Application.UseCases;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class AdvancedStatisticsViewModel
    {
        public int TouristsUnder18 { get; set; }
        public int TouristsBetween18And50 { get; set; }
        public int TouristsOver50 { get; set; }

        ObservableCollection<KeyValuePair<string, int>> AgeGroups;

        private readonly List<TourInstance> _tourInstances;
        private readonly List<TourGuest> _tourGuests;
        private readonly List<TourReservation> _tourReservations;

        private readonly TourInstanceService _tourInstanceService;
        private readonly TourGuestService _tourGuestService;
        private readonly TourReservationService _tourReservationService;


        public AdvancedStatisticsViewModel(Tour tour)
        {
            _tourGuestService = new TourGuestService();
            _tourInstanceService = new TourInstanceService();
            _tourReservationService = new TourReservationService();

            _tourInstances = _tourInstanceService.GetAll();
            _tourGuests = _tourGuestService.GetAll();
            _tourReservations = _tourReservationService.GetAll();

            TouristsUnder18 = 0;
            TouristsBetween18And50 = 0;
            TouristsOver50 = 0;

            GetStatistics(tour);
            LoadChartData();
        }

        private void GetStatistics(Tour tour)
        {
            var relevantGuests = GetGuestsForTour(tour);

            TouristsUnder18 = CountGuestsByAge(relevantGuests, age => age < 18);
            TouristsBetween18And50 = CountGuestsByAge(relevantGuests, age => age >= 18 && age < 50);
            TouristsOver50 = CountGuestsByAge(relevantGuests, age => age >= 50);
        }

        private IEnumerable<TourGuest> GetGuestsForTour(Tour tour)
        {
            return from instance in _tourInstances
                   where instance.TourId == tour.Id
                   from reservation in _tourReservations
                   where reservation.TourInstanceId == instance.Id
                   from guest in _tourGuests
                   where guest.TourReservationId == reservation.Id
                   select guest;
        }

        private int CountGuestsByAge(IEnumerable<TourGuest> guests, Func<int, bool> agePredicate)
        {
            return guests.Count(guest => agePredicate(guest.Age));
        }


        private void LoadChartData()
        {
            AgeGroups = new ObservableCollection<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Under 18", TouristsUnder18),
                new KeyValuePair<string, int>("18 to 50", TouristsBetween18And50),
                new KeyValuePair<string, int>("Over 50", TouristsOver50)
            };
        }

    }
}
