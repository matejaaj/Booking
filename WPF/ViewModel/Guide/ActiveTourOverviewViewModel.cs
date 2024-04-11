using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class ActiveTourOverviewViewModel
    {
        public ObservableCollection<Checkpoint> NotVisitedCheckpoints { get; set; }
        public ObservableCollection<Checkpoint> VisitedCheckpoints { get; set; }
        public ObservableCollection<TourGuest> NotPresentTourists { get; set; }
        public Checkpoint SelectedCheckpoint { get; set; }

        private List<Checkpoint> allCheckpoints;
        private List<TourGuest> tourGuests;
        private int _tourInstaceId;

        private readonly TourGuestService _tourGuestService;
        private readonly TourInstanceService _tourInstanceService;
        private readonly CheckpointService _checkpointService;

        public ActiveTourOverviewViewModel(
            int tourId,
            int tourInstanceId)
        {
            _tourInstaceId = tourInstanceId;
            _tourGuestService = new TourGuestService();
            _tourInstanceService = new TourInstanceService();
            _checkpointService = new CheckpointService();
            LoadData(tourId);
            ShowTourAttendanceOverview();
        }

        private void LoadData(int tourId)
        {
            allCheckpoints = _checkpointService.GetAll();
            tourGuests = _tourGuestService.GetAll();
            InitializeCheckpoints(tourId);
            InitializeTourGuests();
        }

        private void InitializeCheckpoints(int tourId)
        {
            NotVisitedCheckpoints = new ObservableCollection<Checkpoint>();
            VisitedCheckpoints = new ObservableCollection<Checkpoint>();
            foreach (var checkpoint in allCheckpoints)
            {
                if (checkpoint.TourId == tourId)
                    NotVisitedCheckpoints.Add(checkpoint);
            }

            var firstCheckpoint = NotVisitedCheckpoints.First();
            VisitedCheckpoints.Add(firstCheckpoint);
            NotVisitedCheckpoints.Remove(firstCheckpoint);
        }

        private void InitializeTourGuests()
        {
            NotPresentTourists = new ObservableCollection<TourGuest>();
            foreach (var tourGuest in tourGuests)
            {
                if (_tourInstaceId == tourGuest.TourReservationId)
                {
                    NotPresentTourists.Add(tourGuest);
                }
            }
        }

        private void ShowTourAttendanceOverview()
        {
            var firstCheckpointId = VisitedCheckpoints.First().Id;
            TourAttendanceOverview tourAttendanceOverview = new TourAttendanceOverview(firstCheckpointId, NotPresentTourists);
            tourAttendanceOverview.ShowDialog();
        }

        public void MarkAsVisited()
        {
            if (SelectedCheckpoint != null)
            {
                TourAttendanceOverview tourAttendanceOverview = new TourAttendanceOverview(SelectedCheckpoint.Id, NotPresentTourists);
                tourAttendanceOverview.ShowDialog();

                VisitedCheckpoints.Add(SelectedCheckpoint);
                NotVisitedCheckpoints.Remove(SelectedCheckpoint);

                if (NotVisitedCheckpoints.Count() == 0)
                {
                    var finishedTour = _tourInstanceService.GetAll().Find(tour => tour.Id == _tourInstaceId);
                    finishedTour.IsCompleted = true;
                    _tourInstanceService.Update(finishedTour);
                }
            }
        }

        public void EndTour()
        {
            var finishedTour = _tourInstanceService.GetAll().Find(tour => tour.Id == _tourInstaceId);
            finishedTour.IsCompleted = true;
            _tourInstanceService.Update(finishedTour);
        }
    }
}
