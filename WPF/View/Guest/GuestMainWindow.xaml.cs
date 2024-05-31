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

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for GuestMainWindow.xaml
    /// </summary>
    public partial class GuestMainWindow : Window
    {
        private User Guest { get; set; }
        public GuestMainWindow(User guest)
        {
            InitializeComponent();
            Guest = guest;
        }

        private void ShowAccommodations(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new GuestOverview(Guest));
        }

        private void ShowReservations(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new PreviousReservations(Guest));
        }

        private void ShowRatings(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ShowRatings(Guest));
        }
        private void RateAndRenovate(object sender, RoutedEventArgs e)
        {

        }
        private void ShowLocations(object sender, RoutedEventArgs e)
        {
            // Logika za prikaz lokacija "Bio gde/bilo kada"
        }

        private void ShowForum(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ForumForm(Guest));//Guest
        }
        private void ShowSuperGuestPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new SuperGuestOverview(Guest));
        }


        private void ShowContextMenu(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Button button = (Button)sender;
                ContextMenu contextMenu = button.ContextMenu;
                contextMenu.PlacementTarget = button;
                contextMenu.IsOpen = true;
            }
        }

    }
}
