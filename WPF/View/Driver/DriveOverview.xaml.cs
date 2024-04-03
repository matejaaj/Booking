using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for DriveOverview.xaml
    /// </summary>
    public partial class DriveOverview : Window
    {
        public DriveOverviewViewModel VM {  get; set; }
        public DriveOverview(DriveReservationService service)
        {
            InitializeComponent();
            VM = new DriveOverviewViewModel(service);
            DataContext = VM;
        }

       
        private void btnStart(object sender, RoutedEventArgs e)
        {
            VM.btnStart(sender, e);
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnFinish(object sender, RoutedEventArgs e)
        {
            VM.btnFinish(sender, e);
            Close();
        }

    }
}
