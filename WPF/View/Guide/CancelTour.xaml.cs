using System;
using System.Collections.Generic;
using System.Windows;
using BookingApp.WPF.ViewModel.Guide;
using BookingApp.Domain.Model;



namespace BookingApp.WPF.View.Guide
{
    public partial class CancelTour : Window
    {
        private readonly CancelTourViewModel _viewModel;

        public CancelTour(int tourId)
        {
            InitializeComponent();
            _viewModel = new CancelTourViewModel(tourId);
            DataContext = _viewModel;
        }

        private void btnCancelTour_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelTour();
        }
    }
}
