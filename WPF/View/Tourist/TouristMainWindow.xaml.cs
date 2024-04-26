using BookingApp.Domain.Model;
using BookingApp.WPF.View.Tourist;
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
using BookingApp.View.Tourist;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        User _user;

        public TouristMainWindow(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void btnOpenToursOverview_Click(object sender, RoutedEventArgs e)
        {
            ToursOverview toursOverview = new ToursOverview(_user);
            toursOverview.Show();
            this.Close();
        }

        private void btnOpenRequestDrive_Click(object sender, RoutedEventArgs e)
        {
            DriveReservationWindow requestDrive = new  DriveReservationWindow(_user);
            requestDrive.Show();
            this.Close();
        }

        private void btnOpenMyTours_Click(object sender, RoutedEventArgs e)
        {
/*            MyToursWindow myToursWindow = new MyToursWindow(_user);
            myToursWindow.Show();
            this.Close();*/
        }


        private void btnOpenTabs_Click(object sender, RoutedEventArgs e)
        {
            TouristTabsWindow touristTabsWindow = new TouristTabsWindow(_user);
            touristTabsWindow.Show();
            this.Close();
        }
    }
}
