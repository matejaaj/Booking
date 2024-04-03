using BookingApp.Domain.Model;
using BookingApp.DTO;
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
    public partial class ToursOverview : Window
    {
        public ObservableCollection<TourDTO> Tours { get; set; }

        public TourDTO SelectedTour { get; set; }

        public User LoggedInUser { get; set; }

        private  TourRepository _tourRepository;
        private LocationRepository _locationRepository;
        private LanguageRepository _languageRepository;

        public ToursOverview(User user)
        {
            InitializeComponent();
            InitializeRepositories();

            DataContext = this;
            LoggedInUser = user;
            Tours = new ObservableCollection<TourDTO>();

            Update();
        }

        private void InitializeRepositories()
        {
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _languageRepository = new LanguageRepository();
        }

        public void Update()
        {
            Tours.Clear();
            foreach (Tour tour in _tourRepository.GetAll())
            {
                TourDTO dto = new TourDTO(tour);
                dto.Location = _locationRepository.GetLocationById(tour.LocationId).ToString();
                dto.Language = _languageRepository.GetById(tour.LanguageId).ToString();
                Tours.Add(dto);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Select tour first.");
            }
            else
            {
                TourReservationForm tourReservationWindow = new TourReservationForm(SelectedTour.ToTour(), LoggedInUser);
                tourReservationWindow.Show();
            }
        }

        private void ShowCreateTourForm(object sender, RoutedEventArgs e)
        {

        }

        private void ShowViewTourForm(object sender, RoutedEventArgs e) 
        {

        }

        private void ShowUpdateTourForm(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
