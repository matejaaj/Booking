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
using BookingApp.Domain.Model;

namespace BookingApp.WPF.View.Guide
{
    public partial class GuideMainWindow : Window
    {   
        private User user { get; set; }
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            DataContext = this;
        }

        private void ShowStatisticsPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Statistics());
        }

        private void CreateTourPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new TourForm(user));
        }

        private void AllToursPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AllToursOverview());
        }

        private void TodayToursPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new TodayToursOverview());
        }

        private void RequestsPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new TourRequests(user));
        }

        private void ShowSuperGuidePage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new SuperGuide(user));
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Home(user, this));
        }

        private void ContentFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
