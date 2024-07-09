using BookingApp.WPF.ViewModel.Tourist;
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


namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for ReviewTourWindow.xaml
    /// </summary>
    public partial class ReviewTourWindow : Window
    {
        private ReviewTourViewModel _viewModel;

        public ReviewTourWindow(TourInstanceViewModel tour, int touristId)
        {
            InitializeComponent();
            _viewModel = new ReviewTourViewModel(tour, touristId);
            DataContext = _viewModel;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveReviews();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddPictureButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reviewTourFormViewModel = button.DataContext as ReviewTourFormViewModel;

            _viewModel.AddPicture(reviewTourFormViewModel);
        }


        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            var imagePath = image.Tag as string;

            // Find the parent ItemsControl to get the correct DataContext
            var itemControl = FindParent<ItemsControl>(image);
            var reviewFormViewModel = itemControl.DataContext as ReviewTourFormViewModel;

            if (reviewFormViewModel != null && imagePath != null)
            {
                _viewModel.RemovePicture(reviewFormViewModel, imagePath);
            }
        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            if (parent != null) return parent;
            return FindParent<T>(parentObject);
        }


    }
}