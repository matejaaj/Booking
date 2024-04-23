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
            todayDate = DateTime.Now;

            LoadTodayTours();
        }

        private void LoadTodayTours()
        {
            TodayTours = new ObservableCollection<Tour>(_tourService.LoadTodayTours());
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
