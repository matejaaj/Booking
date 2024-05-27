using System.Windows;
using BookingApp.WPF.ViewModel.Guide;
using System.Windows.Controls;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.View.Guide
{
    public partial class TourForm : Page
    {
        private readonly TourFormViewModel _viewModel;
        private User user { get; set; }

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
           "PageTitle", typeof(string), typeof(TourForm), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof(string), typeof(TourForm), new PropertyMetadata(default(string)));

        public string PageIcon
        {
            get { return (string)GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
        }

        public TourForm(User user)
        {
            this.user = user;

            InitializeComponent();
            _viewModel = new TourFormViewModel();
            DataContext = _viewModel;
            this.PageTitle = "CREATE TOUR";
            this.PageIcon = "../../../Resources/Images/Guide/map.png";
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
                AddStartDate addStartDateWindow = new AddStartDate(_viewModel.TourStartDates, _viewModel.TourId, _viewModel.Capacity, user.Id);
                addStartDateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Enter capacity", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
 
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(_viewModel.Images, _viewModel.TourId, ImageResourceType.TOUR, -1);
            addImageWindow.ShowDialog();
        }

        private void btnRecomended_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FindBest();
        }
    }
}
