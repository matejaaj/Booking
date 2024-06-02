using System;
using System.Collections.Generic;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class AddStartDate : Window
    {
        private AddStartDateViewModel viewModel;

        public AddStartDate(List<TourInstance> startDates, int tourId, int capacity, int guideId)
        {
            InitializeComponent();
            viewModel = new AddStartDateViewModel(startDates, tourId, capacity, guideId);
            DataContext = viewModel;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }
    }
}
