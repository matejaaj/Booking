using System.Windows;
using BookingApp.WPF.ViewModel.Guide;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guide
{
    public partial class TourForm : Page
    {
        private readonly TourFormViewModel _viewModel;

        public TourForm()
        {
            InitializeComponent();
            _viewModel = new TourFormViewModel();
            DataContext = _viewModel;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveTour();
        }

        private void btnAddCheckpoints_Click(object sender, RoutedEventArgs e)
        {
            AddCheckpoint addCheckpointWindow = new AddCheckpoint(_viewModel.Checkpoints, _viewModel.TourId);
            addCheckpointWindow.ShowDialog();
        }

        private void btnAddStartDate_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Capacity > 0)
            {
                AddStartDate addStartDateWindow = new AddStartDate(_viewModel.TourStartDates, _viewModel.TourId, _viewModel.Capacity);
                addStartDateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Enter capacity", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
 
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(_viewModel.Images, _viewModel.TourId, ImageResourceType.TOUR);
            addImageWindow.ShowDialog();
        }

        private void btnRecomended_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FindBest();
        }
    }
}
