using System.Windows;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class TourForm : Window
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddCheckpoints_Click(object sender, RoutedEventArgs e)
        {
            AddCheckpoint addCheckpointWindow = new AddCheckpoint(_viewModel.Checkpoints, _viewModel.TourId);
            addCheckpointWindow.Owner = this;
            addCheckpointWindow.ShowDialog();
        }

        private void btnShowCheckpoints_Click(object sender, RoutedEventArgs e)
        {
            ShowCheckpoints showCheckpointswindow = new ShowCheckpoints(_viewModel.Checkpoints);
            showCheckpointswindow.Owner = this;
            showCheckpointswindow.ShowDialog();
        }

        private void btnAddStartDate_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.Capacity > 0)
            {
                AddStartDate addStartDateWindow = new AddStartDate(_viewModel.TourStartDates, _viewModel.TourId, _viewModel.Capacity);
                addStartDateWindow.Owner = this;
                addStartDateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Enter capacity", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnShowStartDates_Click(object sender, RoutedEventArgs e)
        {
            ShowTourInstances showStartDatesWindow = new ShowTourInstances(_viewModel.TourStartDates);
            showStartDatesWindow.Owner = this;
            showStartDatesWindow.ShowDialog();
        }

        private void btnShowImages_Click(object sender, RoutedEventArgs e)
        {
            ShowImages showImagesWindow = new ShowImages(_viewModel.Images);
            showImagesWindow.Owner = this;
            showImagesWindow.ShowDialog();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(_viewModel.Images, _viewModel.TourId, ImageResourceType.TOUR);
            addImageWindow.Owner = this;
            addImageWindow.ShowDialog();
        }

        private void btnRecomended_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FindBest();
        }
    }
}
