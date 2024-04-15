using System.Collections.ObjectModel;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class TourAttendanceOverview : Window
    {
        private readonly TourAttendanceOverviewViewModel _viewModel;

        public TourAttendanceOverview(int checkpointId, ObservableCollection<TourGuest> notPresentTourists)
        {
            InitializeComponent();
            _viewModel = new TourAttendanceOverviewViewModel(checkpointId, notPresentTourists);
            DataContext = _viewModel;
        }

        private void btnMarkAsPresent_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MarkAsPresent();
        }
    }
}
