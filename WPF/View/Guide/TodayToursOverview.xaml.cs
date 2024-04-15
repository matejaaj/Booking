using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class TodayToursOverview : Window
    {
        private readonly TodayToursOverviewViewModel _viewModel;

        public TodayToursOverview()
        {
            InitializeComponent();
            _viewModel = new TodayToursOverviewViewModel();
            DataContext = _viewModel;
        }

        private void btnStartTour_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartTour();
        }
    }
}
