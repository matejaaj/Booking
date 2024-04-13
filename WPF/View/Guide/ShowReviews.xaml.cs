using BookingApp.DTO;
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
    public partial class ShowReviews : Window
    {
        private readonly ShowReviewsViewModel _viewModel;
        public ShowReviews(TourDTO tourDTO)
        {
            InitializeComponent();
            _viewModel = new ShowReviewsViewModel(tourDTO);
            DataContext = _viewModel;
        }

        private void btnShowReviews_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowReviews();
        }
    }
}
