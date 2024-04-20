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

            var checkpoints = _checkpointService.InitializeCheckpoints(tourId, _tourInstaceId);
            NotVisitedCheckpoints = checkpoints.NotVisited;
            VisitedCheckpoints = checkpoints.Visited;

            NotPresentTourists = _tourGuestService.InitializeTourGuests(_tourInstaceId);
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
                string currentCheckpointName = SelectedCheckpoint.Name;
                TourAttendanceOverview tourAttendanceOverview = new TourAttendanceOverview(SelectedCheckpoint.Id, NotPresentTourists);
                tourAttendanceOverview.ShowDialog();

                VisitedCheckpoints.Add(SelectedCheckpoint);
                NotVisitedCheckpoints.Remove(SelectedCheckpoint);

                _tourInstanceService.UpdateCheckpoint(_tourInstaceId, SelectedCheckpoint.ToString());

                if (NotVisitedCheckpoints.Count() == 0)
                {
                    _tourInstanceService.FinishTour(_tourInstaceId);
                }
            }
        }

        public void EndTour()
        {
            _tourInstanceService.FinishTour(_tourInstaceId);
        }
    }
}
