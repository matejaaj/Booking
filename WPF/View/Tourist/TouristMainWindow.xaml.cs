using BookingApp.Domain.Model;
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
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        User User { get; set; }

        public TouristMainWindow(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void btnOpenToursOverview_Click(object sender, RoutedEventArgs e)
        {
            ToursOverview toursOverview = new ToursOverview(User);
            toursOverview.Show();
            this.Close();
        }

        private void btnOpenRequestDrive_Click(object sender, RoutedEventArgs e)
        {
            DriveReservationWindow requestDrive = new  DriveReservationWindow(User);
            requestDrive.Show();
            this.Close();
        }
    }
}
