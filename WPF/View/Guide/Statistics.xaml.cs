using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class Statistics : Window
    {
        private readonly StatisticsViewModel _viewModel;

        public Statistics()
        {
            InitializeComponent();
            _viewModel = new StatisticsViewModel();
            DataContext = _viewModel;
        }

        private void btnSearchTours_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchTours();
        }

        private void btnSearchRequests_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchRequests();
        }
    }
}
