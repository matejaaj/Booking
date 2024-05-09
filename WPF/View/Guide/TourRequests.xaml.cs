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

namespace BookingApp.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequests.xaml
    /// </summary>
    public partial class TourRequests : Window
    {
        private readonly TourRequestsViewModel _viewModel;

        public TourRequests()
        {
            InitializeComponent();
            _viewModel = new TourRequestsViewModel();
            DataContext = _viewModel;
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
