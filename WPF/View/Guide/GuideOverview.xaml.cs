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
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class GuideOverview : Window
    {
        private GuideOverviewViewModel viewModel;

        public GuideOverview()
        {
            InitializeComponent();
            viewModel = new GuideOverviewViewModel();
            DataContext = viewModel;
        }

        private void btnShowTourForm_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowTourForm();
        }

        private void btnShowTodayTours_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowTodayTours();
        }

        private void btnShowAllTours_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowAllTours();
        }

        private void btnStatistics_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowStatistics();
        }

        private void btnRequests_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowRequests();
        }
    }
}

