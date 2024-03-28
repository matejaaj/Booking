using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.View.Guide
{
    public partial class ActiveTourOverview : Window
    {   
        public  ObservableCollection<Checkpoint> NotVisitedCheckpoints { get; set; }
        public  ObservableCollection<Checkpoint> VisitedCheckpoints { get; set; }
        public ObservableCollection<TourGuest> NotPresentTourists { get; set; }

        private List<Checkpoint> allCheckpoints;
        public Checkpoint SelectedCheckpoint { get; set; }

        private  TourGuestRepository _tourGuestRepository;
        private List<TourGuest> tourGuests;

        private  TourInstanceRepository _tourInstanceRepository;
        private List<TourInstance> tourInstances;

        private int _tourInstaceId;

        private CheckpointRepository _checkpointRepository;


        public ActiveTourOverview(int tourId, int tourInstanceId)
        {
            InitializeComponent();
            DataContext = this;
            _tourInstaceId = tourInstanceId;

            InitializeRepositories();
            LoadData();
            InitializeCheckpoints(tourId);
            InitializeTourGuests();
            ShowTourAttendanceOverview();
        }
        private void InitializeRepositories()
        {
            _checkpointRepository = new CheckpointRepository();
            _tourGuestRepository = new TourGuestRepository();
            _tourInstanceRepository = new TourInstanceRepository();
        }

        private void LoadData()
        {
            tourInstances = _tourInstanceRepository.GetAll();
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

        private void btnMarkAsVisited_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedCheckpoint != null)
            {
                TourAttendanceOverview tourAttendanceOverview = new TourAttendanceOverview(SelectedCheckpoint.Id, NotPresentTourists);
                tourAttendanceOverview.ShowDialog();

                VisitedCheckpoints.Add(SelectedCheckpoint);
                NotVisitedCheckpoints.Remove(SelectedCheckpoint);

                if (NotVisitedCheckpoints.Count() == 0)
                {
                    var finishedTour = tourInstances.Find(tour => tour.Id == _tourInstaceId);
                    finishedTour.IsCompleted = true;
                    _tourInstanceRepository.Update(finishedTour);
                }
            }
        }

        private void btnEndTour_Click(object sender, RoutedEventArgs e)
        {
            var finishedTour = tourInstances.Find(tour => tour.Id == _tourInstaceId);
            finishedTour.IsCompleted = true;
            _tourInstanceRepository.Update(finishedTour);
            Close();
        }
    }
}
