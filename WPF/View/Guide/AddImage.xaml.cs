using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class AddImage : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private AddImageViewModel viewModel;

        public AddImage(List<Domain.Model.Image> images, int entityId, ImageResourceType imageResourceType)
        {
            InitializeComponent();
            viewModel = new AddImageViewModel(images, entityId, imageResourceType);
            DataContext = viewModel;
        }
 
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddImage();
        }
    }
}
