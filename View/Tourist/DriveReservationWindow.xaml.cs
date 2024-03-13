using BookingApp.Model;
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
        public User User { get; set; }

        public DriveReservationWindow(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void btnRequestDrive_Click(object sender, RoutedEventArgs e)
        {
            // Logika za Request Drive
            MessageBox.Show("Requesting Drive...");
        }

        private void btnRequestFastDrive_Click(object sender, RoutedEventArgs e)
        {
            // Logika za Request Fast Drive
            MessageBox.Show("Requesting Fast Drive...");
        }
    }
}
