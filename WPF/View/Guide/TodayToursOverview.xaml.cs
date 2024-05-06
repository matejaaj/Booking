using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guide;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guide
{
    public partial class TodayToursOverview : Page
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
