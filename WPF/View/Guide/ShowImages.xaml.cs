using System.Collections.Generic;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guide;

namespace BookingApp.WPF.View.Guide
{
    public partial class ShowImages : Window
    {
        private ShowImagesViewModel viewModel;

        public ShowImages(List<Domain.Model.Image> images)
        {
            InitializeComponent();
            viewModel = new ShowImagesViewModel(images);
            DataContext = viewModel;
        }
    }
}
