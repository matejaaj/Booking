using System.Collections.ObjectModel;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class ActiveTourOverview : Window
    {
        private ActiveTourOverviewViewModel viewModel;

        public ActiveTourOverview(int tourId, int tourInstanceId)
        {
            InitializeComponent();
            viewModel = new ActiveTourOverviewViewModel(tourId, tourInstanceId);
            DataContext = viewModel;
        }

        private void btnMarkAsVisited_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MarkAsVisited();
        }

        private void btnEndTour_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EndTour();
            Close();
        }
    }
}
