using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.WPF.ViewModel.Guide;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guide
{
    public partial class Statistics : Page
    {
        private readonly StatisticsViewModel _viewModel;

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
           "PageTitle", typeof(string), typeof(Statistics), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof(string), typeof(Statistics), new PropertyMetadata(default(string)));

        public string PageIcon
        {
            get { return (string)GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
        }
        public Statistics()
        {
            InitializeComponent();
            _viewModel = new StatisticsViewModel();
            DataContext = _viewModel;
            this.PageTitle = "STATISTICS";
            this.PageIcon = "../../../Resources/Images/Guide/analytics.png";
        }

        private void btnSearchTours_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchTours();
        }

        private void btnSearchRequests_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchRequests();
        }
    }
}
