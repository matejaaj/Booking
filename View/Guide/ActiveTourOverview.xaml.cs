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

        private readonly TourGuestRepository _tourGuestRpository;
        private List<TourGuest> tourGuests;

        private readonly TourInstanceRepository _tourInstanceRepository;
        private List<TourInstance> tourInstaces;

        private int _tourInstaceId;

        private readonly CheckpointRepository _checkpointRepository;
        public ActiveTourOverview(int tourId, int tourInstanceId)
        {
            InitializeComponent();
            DataContext = this;
            _tourInstaceId = tourInstanceId;
            _checkpointRepository = new CheckpointRepository();
            _tourGuestRpository = new TourGuestRepository();
            _tourInstanceRepository = new TourInstanceRepository();


            tourInstaces = _tourInstanceRepository.GetAll();
            allCheckpoints = _checkpointRepository.GetAll();
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

            _tourGuestRpository = new TourGuestRepository();
            tourGuests = _tourGuestRpository.GetAll();

            NotPresentTourists = new ObservableCollection<TourGuest>();
            foreach (var tourGuest in tourGuests)
            {
                if (_tourInstaceId == tourGuest.TourReservationId)
                {
                    NotPresentTourists.Add(tourGuest);
                }
            }

            TourAttendanceOverview tourAttendanceOverview = new TourAttendanceOverview(firstCheckpoint.Id, NotPresentTourists);
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
                    var finishedTour = tourInstaces.Find(tour => tour.Id == _tourInstaceId);
                    finishedTour.IsCompleted = true;
                    _tourInstanceRepository.Update(finishedTour);
                }
            }
        }

        private void btnEndTour_Click(object sender, RoutedEventArgs e)
        {
            var finishedTour = tourInstaces.Find(tour => tour.Id == _tourInstaceId);
            finishedTour.IsCompleted = true;
            _tourInstanceRepository.Update(finishedTour);
            Close();
        }
    }
}
