using BookingApp.Model;
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

namespace BookingApp.View.Driver
{
    /// <summary>
    /// Interaction logic for ViewDrive.xaml
    /// </summary>
    public partial class ViewDrive : Window
    {
        public bool IsAtLocation => rbAtLocation.IsChecked == true;
        public int DelayMinutes => int.TryParse(txtDelayMinutes.Text, out int minutes) ? minutes : 0;
        public DriveReservation reservation { get; set; }
        public DriveReservationRepository Repo {  get; set; }
        public event EventHandler ReservationConfirmed;

        public ViewDrive()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAtLocation)
            {
                reservation.DelayMinutes = DelayMinutes;
                Repo.Update(reservation);
                ReservationConfirmed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                reservation.DelayMinutes = -1;
                Repo.Update(reservation);
                ReservationConfirmed?.Invoke(this, EventArgs.Empty);
            }
            
            Close();
        }

        private void rbAtLocation_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
