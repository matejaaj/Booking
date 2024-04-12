using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class AllToursOverview : Window
    {
        private readonly AllToursOverviewViewModel _viewModel;

        public AllToursOverview()
        {
            InitializeComponent();
            _viewModel = new AllToursOverviewViewModel();
            DataContext = _viewModel;
        }

        private void btnCancelTour_Click(object sender, RoutedEventArgs e)
        {
            CancelTour cancelTourWindow = new CancelTour(_viewModel.SelectedTour.Id);
            cancelTourWindow.Owner = this;
            cancelTourWindow.Show();
        }
    }
}
