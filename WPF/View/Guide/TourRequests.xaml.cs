using BookingApp.WPF.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequests.xaml
    /// </summary>
    public partial class TourRequests : Page
    {
        private readonly TourRequestsViewModel _viewModel;

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
            "PageTitle", typeof(string), typeof(TourRequests), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof(string), typeof(TourRequests), new PropertyMetadata(default(string)));

        public string PageIcon
        {
            get { return (string)GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
        }


        public TourRequests()
        {
            InitializeComponent();
            _viewModel = new TourRequestsViewModel();
            DataContext = _viewModel;
            this.PageIcon = "../../../Resources/Images/Guide/personal.png";
            this.PageTitle = "REQUESTS";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Filter();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Accept();
        }
    }
}
