using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;
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

        private readonly TourGuestRepository _tourGuestRepository;
        private readonly TourInstanceRepository _tourInstanceRepository;
        private readonly CheckpointRepository _checkpointRepository;

        public ActiveTourOverviewViewModel(int tourId, int tourInstanceId)
        {
            _tourInstaceId = tourInstanceId;
            _tourGuestRepository = new TourGuestRepository();
            _tourInstanceRepository = new TourInstanceRepository();
            _checkpointRepository = new CheckpointRepository();
            LoadData();
            InitializeCheckpoints(tourId);
            InitializeTourGuests();
            ShowTourAttendanceOverview();
        }

        private void LoadData()
        {
            allCheckpoints = _checkpointRepository.GetAll();
            tourGuests = _tourGuestRepository.GetAll();
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
                    var finishedTour = _tourInstanceRepository.GetAll().Find(tour => tour.Id == _tourInstaceId);
                    finishedTour.IsCompleted = true;
                    _tourInstanceRepository.Update(finishedTour);
                }
            }
        }

        public void EndTour()
        {
            var finishedTour = _tourInstanceRepository.GetAll().Find(tour => tour.Id == _tourInstaceId);
            finishedTour.IsCompleted = true;
            _tourInstanceRepository.Update(finishedTour);
        }
    }
}
