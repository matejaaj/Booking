using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TodayToursOverviewViewModel
    {
        public ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }

        private readonly TourService _tourService;
        private readonly TourInstanceService _tourInstanceService;

        private DateTime todayDate;

        public TodayToursOverviewViewModel()
        {
            _tourService = new TourService();
            _tourInstanceService = new TourInstanceService();
            TodayTours = new ObservableCollection<Tour>();
            todayDate = DateTime.Now;

            LoadTodayTours();
        }

        private void LoadTodayTours()
        {
            var tours = _tourService.GetAll();
            var tourInstances = _tourInstanceService.GetAll();

            foreach (var tourInstance in tourInstances)
            {
                if (tourInstance.StartTime.Date == todayDate.Date && !tourInstance.IsCompleted)
                {
                    var matchingTour = tours.FirstOrDefault(tour => tour.Id == tourInstance.TourId);
                    if (matchingTour != null)
                        TodayTours.Add(matchingTour);
                }
            }
        }

        public void StartTour()
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Select tour first.");
            }
            else
            {
                var tourInstance = _tourInstanceService.GetAll()
                    .FirstOrDefault(tourInstance => tourInstance.TourId == SelectedTour.Id &&
                                                    tourInstance.StartTime.Date == todayDate.Date &&
                                                    !tourInstance.IsCompleted);

                if (tourInstance != null)
                {
                    var activeTourOverview = new ActiveTourOverview(SelectedTour.Id, tourInstance.Id);
                    activeTourOverview.ShowDialog();
                    UpdateTodayTours();
                }
            }
        }

        private void UpdateTodayTours()
        {
            TodayTours.Clear();
            LoadTodayTours();
        }
    }
}
