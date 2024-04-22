using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristTabsWindow.xaml
    /// </summary>
    public partial class TouristTabsWindow : Window
    {
        private User _user;

        public TouristTabsWindow(User user)
        {
            _user = user;
            InitializeComponent();

            var myToursViewModel = new MyToursViewModel(user);
            this.DataContext = myToursViewModel;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is TabControl tabControl)
            {
                // Uzmite trenutno selektovan TabItem
                var selectedTab = tabControl.SelectedItem as TabItem;

                switch (selectedTab.Header)
                {
                    case "Ture":
                        // Logika za 'Ture' tab
                        break;
                    case "Taksi":
                        // Logika za 'Taksi' tab
                        break;
                        // Dodajte case-ove za druge glavne tabove ako ih ima
                }
            }
        }

        private void TureTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is TabControl tabControl)
            {
            }
        }


        private void TaksiTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is TabControl tabControl)
            {

            }
        }


        private void MoreDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tour = button.Tag as TourInstanceViewModel;
            if (tour != null)
            {
                var detailsWindow = new MyTourMoreDetailsWindow(tour);
                detailsWindow.Show();
            }

        }
        private void RateTour_Click(object sender, RoutedEventArgs e)
        {
            TourReviewService _tourReviewService = new TourReviewService();

            var button = sender as Button;
            var tour = button.Tag as TourInstanceViewModel;
            if (tour != null)
            {
                if (!tour.IsFinished)
                {
                    MessageBox.Show("Tura nije gotova i dalje");
                    return;
                }

                if (_tourReviewService.HasUserReviewedTour(_user.Id, tour.Id))
                {
                    MessageBox.Show("Tura je već ocenjena");
                    return;
                }

                var reviewWindow = new ReviewTourWindow(tour, _user.Id);
                reviewWindow.Show();
            }
        }


    }
}
