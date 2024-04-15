using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for RespondView.xaml
    /// </summary>
    public partial class RespondView : Window
    {
        public RespondViewModel VM { get; set; }

        public RespondView(DriveReservation driveReservation, DriveReservationService service)
        {
            InitializeComponent();
            VM = new RespondViewModel(service, driveReservation);
            VM.Time = driveReservation.DepartureTime.ToString();
            DataContext = VM;
        }


        private void Respond_Accept(object sender, RoutedEventArgs e)
        {
            VM.Respond_Accept(this, e);
            Close();

        }

        private void Respond_Decline(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
