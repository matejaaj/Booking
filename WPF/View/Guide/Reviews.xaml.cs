using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class Reviews : Window
    {
        private readonly ReviewsViewModel _viewModel;
        
        public Reviews(TourInstance instance)
        {
            InitializeComponent();
            _viewModel = new ReviewsViewModel(instance);
            DataContext = _viewModel;
        }

        private void btnReportReview_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ReportReview();
        }
    }
}
