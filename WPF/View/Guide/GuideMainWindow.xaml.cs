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

namespace BookingApp.WPF.View.Guide
{
    public partial class GuideMainWindow : Window
    {
        public GuideMainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ShowStatisticsPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Statistics());
        }

        private void CreateTourPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new TourForm());
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
            ContentFrame.Navigate(new TourRequests());
        }
    }
}
