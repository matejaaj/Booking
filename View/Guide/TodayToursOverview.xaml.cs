using BookingApp.Model;
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

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TodayToursOverview.xaml
    /// </summary>
    public partial class TodayToursOverview : Window
    {
        private List<Tour> Tours;
        public static ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }

        private List<TourInstance> TourInstances;

        private readonly TourRepository _tourRepository;
        private readonly TourInstanceRepository _tourInstaceRepository;

        DateTime todayDate;

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
                if(tourInstace.StartTime.Date == todayDate.Date)
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

            }
        }
    }
}
