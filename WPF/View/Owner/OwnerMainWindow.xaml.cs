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

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        public Domain.Model.Owner Owner1 { get; set; }
        public OwnerMainWindow(Domain.Model.Owner owner)
        {
            InitializeComponent();
            Owner1 = owner;
            MainFrame.Navigate(new AccommodationsPage(owner));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowMenuBar(object sender, RoutedEventArgs e)
        {
            if (SideMenu.Visibility == Visibility.Collapsed)
            {
                SideMenu.Visibility = Visibility.Visible;
            }
            else
            {
                SideMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void HideMenuBar(object sender, RoutedEventArgs e)
        {
            SideMenu.Visibility = Visibility.Collapsed;
        }
    }
}
