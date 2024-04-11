using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TodayToursOverviewViewModel
    {
        public ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }

        private readonly TourRepository _tourRepository;
        private readonly TourInstanceRepository _tourInstanceRepository;

        private DateTime todayDate;

        public TodayToursOverviewViewModel()
        {
            _tourRepository = new TourRepository();
            _tourInstanceRepository = new TourInstanceRepository();
            TodayTours = new ObservableCollection<Tour>();
            todayDate = DateTime.Now;

            LoadTodayTours();
        }

        private void LoadTodayTours()
        {
            var tours = _tourRepository.GetAll();
            var tourInstances = _tourInstanceRepository.GetAll();

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
                var tourInstance = _tourInstanceRepository.GetAll()
                    .FirstOrDefault(tour => tour.TourId == SelectedTour.Id &&
                                             tour.StartTime.Date == todayDate.Date &&
                                             !tour.IsCompleted);

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
