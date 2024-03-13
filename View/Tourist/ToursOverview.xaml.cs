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

namespace BookingApp.View.Tourist
{
    /// <summary>
    /// Interaction logic for ToursOverview.xaml
    /// </summary>
    public partial class ToursOverview : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }

        public Tour SelectedTour { get; set; }

        public User LoggedInUser { get; set; }

        private readonly TourRepository _repository;

        public ToursOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetAll());
        }

        private void ShowCreateTourForm(object sender, RoutedEventArgs e)
        {
/*            TourForm createTourForm = new TourForm(LoggedInUser);
            createTourForm.Show();*/
        }

        private void ShowViewTourForm(object sender, RoutedEventArgs e) 
        {
/*            if (SelectedTour != null)
            {
                TourForm viewTourForm = new TourForm(SelectedTour);
                viewTourForm.Show();
            }*/
        }

        private void ShowUpdateTourForm(object sender, RoutedEventArgs e)
        {
/*            if (SelectedTour != null)
            {
                TourForm updateTourForm = new TourForm(SelectedTour, LoggedInUser);
                updateTourForm.Show();
            }*/
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
/*            if (SelectedTour != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete tour",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _repository.Delete(SelectedTour);
                    Tours.Remove(SelectedTour);
                }
            }*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour == null)
            {
                MessageBox.Show("Select tour first.");
            } else
            {
                TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour);
                tourReservationWindow.Show();
            }

        }
    }
}
