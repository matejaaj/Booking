using BookingApp.Domain.Model;
using BookingApp.Repository;
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

namespace BookingApp.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for TodayToursOverview.xaml
    /// </summary>
    public partial class TodayToursOverview : Window
    {
        private List<Tour> Tours;
        public ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }

        private List<TourInstance> TourInstances;

        private readonly TourRepository _tourRepository;
        private readonly TourInstanceRepository _tourInstaceRepository;

        private int _tourInstanceId;
        private DateTime todayDate;

        public TodayToursOverview()
        {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            _tourInstaceRepository = new TourInstanceRepository();
            Tours = _tourRepository.GetAll();
            TourInstances = _tourInstaceRepository.GetAll();
            TodayTours = new ObservableCollection<Tour>();

            todayDate = DateTime.Now;

            foreach(var tourInstace in TourInstances)
            {
                if(tourInstace.StartTime.Date == todayDate.Date && tourInstace.IsCompleted == false)
                {
                    var matchingTour = Tours.Find(tour => tour.Id == tourInstace.TourId);
                    if (matchingTour != null)
                        TodayTours.Add(matchingTour);
                }
            }
        }

        private void btnStartTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Select tour first.");
            }
            else
            {
                TourInstance tourInstace = TourInstances.Find(tour => tour.TourId == SelectedTour.Id &&
                    tour.StartTime.Date == todayDate.Date && tour.IsCompleted == false);

                _tourInstanceId = tourInstace.Id;

                ActiveTourOverview activeTourOverview = new ActiveTourOverview(SelectedTour.Id, _tourInstanceId);
                activeTourOverview.ShowDialog();
            }
            Update();
        }

        private void Update()
        {
            TodayTours.Clear();
            TourInstances = _tourInstaceRepository.GetAll();
            foreach (var tourInstace in TourInstances)
            {
                if (tourInstace.StartTime.Date == todayDate.Date && tourInstace.IsCompleted == false)
                {
                    var matchingTour = Tours.Find(tour => tour.Id == tourInstace.TourId);
                    if (matchingTour != null)
                        TodayTours.Add(matchingTour);
                }
            }
        }
    }
}
