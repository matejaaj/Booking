using System.Collections.Generic;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class ShowTourInstances : Window
    {
        private ShowTourInstancesViewModel viewModel;

        public ShowTourInstances(List<TourInstance> instances)
        {
            InitializeComponent();
            viewModel = new ShowTourInstancesViewModel(instances);
            DataContext = viewModel;
        }
    }
}
