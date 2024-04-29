using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for DriverOverview.xaml
    /// </summary>
    public partial class DriverOverview : Page
    {
        public static DriverOverviewViewModel VM{ get; set; }

        public DriverOverview(User driver)
        {
            InitializeComponent();
            VM = new DriverOverviewViewModel(driver);
            DataContext = VM;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            VM.btnDeleteVehicle_Click(sender, e);
        }

        private void ViewDrive_Click(object sender, RoutedEventArgs e)
        {
            VM.ViewDrive_Click(sender, e);
        }



        private void ViewDrive_Respond(object? sender, EventArgs e)
        {
            VM.ViewDrive_Respond(sender, e);
        }
        private void ViewDrive_Cancel(object? sender, EventArgs e)
        {
            VM.ViewDrive_Cancel(sender, e);
        }

        private void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            VM.btnDrive_Click(sender, e);   
        }

        private void btnStats_Click(object sender, RoutedEventArgs e)
        {
            VM.btnStats_Click(sender, e);   
        }

        private void ShowCreateVehicleForm(object sender, RoutedEventArgs e)
        {
            VM.ShowCreateVehicleForm(sender, e, this);
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
