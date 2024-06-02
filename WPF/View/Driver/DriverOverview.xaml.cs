using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

        private void ViewDrive_Respond(object? sender, EventArgs e)
        {
            VM.ViewDrive_Respond(sender, e);
        }

        public void OnLoad(object? sender, EventArgs e)
        {
            VM.UpdateReservationList();
        }
    }
}
