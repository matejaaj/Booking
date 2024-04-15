using System.Collections.Generic;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class ShowCheckpoints : Window
    {
        private ShowCheckpointsViewModel viewModel;

        public ShowCheckpoints(List<Checkpoint> checkpoints)
        {
            InitializeComponent();
            viewModel = new ShowCheckpointsViewModel(checkpoints);
            DataContext = viewModel;
        }
    }
}
