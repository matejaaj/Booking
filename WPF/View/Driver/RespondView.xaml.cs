using BookingApp.Domain.Model;
using BookingApp.Repository;
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
        public static DriveReservation Reservation { get; set; }
        public static String Time { get; set; }

        private readonly DriveReservationRepository driveReservationRepository = new DriveReservationRepository();

        public event EventHandler ReservationConfirmed;

        public RespondView(DriveReservation driveReservation)
        {
            InitializeComponent();
            Time = driveReservation.DepartureTime.ToString();
            Reservation = driveReservation;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Respond_Accept(object sender, RoutedEventArgs e)
        {
            Reservation.DriveReservationStatusId = 2;
            driveReservationRepository.Update(Reservation);
            ReservationConfirmed.Invoke(this, EventArgs.Empty);
            Close();

        }

        private void Respond_Decline(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
