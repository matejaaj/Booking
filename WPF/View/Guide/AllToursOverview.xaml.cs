using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guide;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guide
{
    public partial class AllToursOverview : Page
    {
        private readonly AllToursOverviewViewModel _viewModel;

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
           "PageTitle", typeof(string), typeof(AllToursOverview), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof(string), typeof(AllToursOverview), new PropertyMetadata(default(string)));

        public string PageIcon
        {
            get { return (string)GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
        }
        public AllToursOverview()
        {
            InitializeComponent();
            _viewModel = new AllToursOverviewViewModel();
            DataContext = _viewModel;
            this.PageTitle = "ALL TOURS";
            this.PageIcon = "../../../Resources/Images/Guide/planet-earth.png";
        }

        private void btnCancelTour_Click(object sender, RoutedEventArgs e)
        {
            CancelTour cancelTourWindow = new CancelTour(_viewModel.SelectedTour.Id);
            cancelTourWindow.Show();
        }

        private void btnAdvancedStatistics_Click(object sender, RoutedEventArgs e)
        {
            AdvancedStatistics advancedStatisticsWindow = new AdvancedStatistics(_viewModel.SelectedTour);
            advancedStatisticsWindow.Show();
        }

        private void btnShowReviews_Click(object sender, RoutedEventArgs e)
        {
            ShowReviews showReviewsWindow = new ShowReviews(_viewModel.SelectedTour);
            showReviewsWindow.Show();
        }
    }
}
