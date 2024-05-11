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

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
           "PageTitle", typeof(string), typeof(TodayToursOverview), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof(string), typeof(TodayToursOverview), new PropertyMetadata(default(string)));

        public string PageIcon
        {
            get { return (string)GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
        }
        public TodayToursOverview()
        {
            InitializeComponent();
            _viewModel = new TodayToursOverviewViewModel();
            DataContext = _viewModel;
            this.PageTitle = "TODAY TOURS";
            this.PageIcon = "../../../Resources/Images/Guide/tour-guide.png";
        }

        private void btnStartTour_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartTour();
        }
    }
}
