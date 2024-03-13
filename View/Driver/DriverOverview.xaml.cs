using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.Driver
{
    /// <summary>
    /// Interaction logic for DriverOverview.xaml
    /// </summary>
    public partial class DriverOverview : Window
    {
        public static ObservableCollection<Vehicle> Vehicles { get; set; }

        public static ObservableCollection<DriveReservation> DriveReservations { get; set; }

        private readonly DriveReservationRepository _repository;
        public DriverOverview()
        {
            InitializeComponent();
            DataContext = this;
            Vehicles = new ObservableCollection<Vehicle>();
            _repository = new DriveReservationRepository();
            DriveReservations = new ObservableCollection<DriveReservation>(_repository.GetAll());

        }
        private void ShowCreateVehicleForm(object sender, RoutedEventArgs e)
        {
            VehicleForm vehicleForm = new VehicleForm();
            vehicleForm.Show();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
