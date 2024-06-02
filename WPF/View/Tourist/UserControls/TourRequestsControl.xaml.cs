using BookingApp.Application.UseCases;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.ViewModel.Tourist;
using BookingApp.DTO;

namespace BookingApp.WPF.View.Tourist.UserControls
{
    /// <summary>
    /// Interaction logic for TourRequestsControl.xaml
    /// </summary>
    public partial class TourRequestsControl : UserControl
    {
        public TourRequestsControl()
        {
            InitializeComponent();
        }

        private void OpenTourRequestForm_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as TourRequestsViewModel;
            viewModel.OpenFormWindow();

        }


        private void OpenRequestStatistics_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as TourRequestsViewModel;
            viewModel.OpenStatisticsWindow();
        }

        private void SimpleRequestDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var tourRequest = button.Tag as TourRequestDTO;
            if (tourRequest == null)
                return;


            SimpleTourRequestDetails detailsWindow = new SimpleTourRequestDetails(tourRequest);
            detailsWindow.Show(); 
        }


        private void ComplexRequestDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var complexTourRequest = button.Tag as ComplexTourRequestDTO;
            if (complexTourRequest == null)
                return;

            ComplexTourRequestDetails detailsWindow = new ComplexTourRequestDetails(complexTourRequest);
            detailsWindow.Show();
        }

    }
}
