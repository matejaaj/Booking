using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class StatisticsViewModel : INotifyPropertyChanged
    {
        private readonly List<Tour> _tours;
        private readonly List<TourInstance> _tourInstances;
        private readonly List<TourGuest> _tourGuests;

        private readonly TourInstanceService _tourInstanceService;
        private readonly TourService _tourService;
        private readonly TourGuestService _tourGuestService;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private TourDTO _mostVisitedTour;
        public TourDTO MostVisitedTour
        {
            get { return _mostVisitedTour; }
            set
            {
                if (_mostVisitedTour != value)
                {
                    _mostVisitedTour = value;
                    OnPropertyChanged(nameof(MostVisitedTour));
                }
            }
        }
        public List<object> Years { get; set; }
        public object SelectedYear { get; set; }


        public StatisticsViewModel()
        {
            _tourGuestService = new TourGuestService();
            _tourService = new TourService();
            _tourInstanceService = new TourInstanceService();

            _tours = _tourService.GetAll();
            _tourInstances = _tourInstanceService.GetAll();
            _tourGuests = _tourGuestService.GetAll();

            Years = new List<object>();
            Years.Add("All time");
            for (int i = 0; i < 20; i++)
            {
                Years.Add((DateTime.Now.Year - i).ToString());
            }
        }

        public void Search()
        {
            Tour mostVisitedTour = FindMostVisitedTour();
            MostVisitedTour = new TourDTO(mostVisitedTour);
        }

        private Tour FindMostVisitedTour()
        {
            int mostTourist = 0;
            Tour mostVisitedTour = _tours[0];

            foreach (var tour in _tours)
            {
                int currentTourTourists = CalculateTotalTouristsForTour(tour);
                if (currentTourTourists > mostTourist)
                {
                    mostTourist = currentTourTourists;
                    mostVisitedTour = tour;
                }
            }

            return mostVisitedTour;
        }

        private int CalculateTotalTouristsForTour(Tour tour)
        {
            return _tourInstances
                .Where(instance => ShouldIncludeInstance(instance))
                .Where(instance => instance.TourId == tour.Id)
                .Sum(instance => CalculateTotalTouristsForInstance(instance));
        }

        private bool ShouldIncludeInstance(TourInstance instance)
        {
            return (SelectedYear.Equals("All time") || instance.StartTime.Year.ToString().Equals(SelectedYear)) && instance.IsCompleted;
        }

        private int CalculateTotalTouristsForInstance(TourInstance instance)
        {
            return _tourGuests.Count(guest => guest.TourReservationId == instance.Id);
        }

    }
}