using BookingApp.Domain.Model;
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

namespace BookingApp.View.Tourist
{
    /// <summary>
    /// Interaction logic for RequestDriveWindow.xaml
    /// </summary>
    public partial class DriveReservationWindow : Window
    {
        private DriveReservationViewModel viewModel;

        public DriveReservationWindow(User user)
        {
            InitializeComponent();
            viewModel = new DriveReservationViewModel(user);
            DataContext = viewModel;
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Reserve();
            this.Close();
        }

        private void Country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.FillCities();
        }


        private void Minutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.UpdateDriverList();
        }
    }
}
