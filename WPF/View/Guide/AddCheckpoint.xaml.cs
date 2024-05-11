using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class AddCheckpoint : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private AddCheckpointViewModel viewModel;

        public AddCheckpoint(List<Checkpoint> checkpoints, int tourId)
        {
            InitializeComponent();
            viewModel = new AddCheckpointViewModel(checkpoints, tourId);
            DataContext = viewModel;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddCheckpoint();
        }
    }
}
