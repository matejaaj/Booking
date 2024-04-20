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
            Tour mostVisitedTour = _tourService.FindMostVisited(SelectedYear);
            MostVisitedTour = new TourDTO(mostVisitedTour);
        }
    }
}