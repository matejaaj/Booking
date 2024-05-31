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
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist;

namespace BookingApp.WPF.View.Tourist.UserControls
{
    /// <summary>
    /// Interaction logic for DriveReservationControl.xaml
    /// </summary>
    public partial class DriveReservationControl : UserControl
    {
        public static readonly DependencyProperty TouristProperty = DependencyProperty.Register(
            "Tourist", typeof(User), typeof(DriveReservationControl), new PropertyMetadata(null));

        public User Tourist
        {
            get { return (User)GetValue(TouristProperty); }
            set { SetValue(TouristProperty, value); }
        }


        public DriveReservationControl()
        {
            InitializeComponent();
        }

        private void AddDelay_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var reservation = button.Tag as DriveReservationViewModel;
            if (reservation.DelayTourist != 0)
            {
                MessageBox.Show("Delay already set for the tourist.");
                return;  
            }

            int delay = 0;

            DelayInputDialog dialog = new DelayInputDialog();
            if (dialog.ShowDialog() == true)
            {
                delay = dialog.DelayMinutes ?? 0;
                MessageBox.Show($"Delay of {delay} minutes added.");

                if (DataContext is DriveMainTabViewModel viewModel)
                {
                    viewModel.AddDelay(reservation, delay);
                }
            }
        }


        private void MarkDriverAsUnreliable_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var reservation = button.Tag as DriveReservationViewModel;
            if (reservation == null)
            {
                MessageBox.Show("No reservation selected.");
                return;
            }

            if (DataContext is DriveMainTabViewModel viewModel)
            {
                viewModel.ValidateAndMarkDriverAsUnreliable(reservation);
            }
        }




        private void ResserveDrive_Click(object sender, RoutedEventArgs e)
        {

            if (DataContext is DriveMainTabViewModel viewModel)
            {
                viewModel.OpenReservationWindos();
            }

        }
    }
}
