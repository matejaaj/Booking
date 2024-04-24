using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class ActiveTourOverviewViewModel
    {
        public ObservableCollection<Checkpoint> NotVisitedCheckpoints { get; set; }
        public ObservableCollection<Checkpoint> VisitedCheckpoints { get; set; }
        public ObservableCollection<TourGuest> NotPresentTourists { get; set; }
        public Checkpoint SelectedCheckpoint { get; set; }

        private int _tourInstaceId;

        private TourInstanceService _tourInstanceService;
        private TourGuestService _tourGuestService;
        private CheckpointService _checkpointService;

        public ActiveTourOverviewViewModel(
            int tourId,
            int tourInstanceId)
        {
            _tourInstaceId = tourInstanceId;
            InitializeServices();
            LoadData(tourId);
            ShowTourAttendanceOverview();
        }

        private void LoadData(int tourId)
        {
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

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>(), _tourGuestService, _voucherService);
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _checkpointService = new CheckpointService(Injector.CreateInstance<ICheckpointRepository>(), _tourInstanceService);
        }
    }
}
