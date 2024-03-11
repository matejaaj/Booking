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
    /// Interaction logic for VehicleForm.xaml
    /// </summary>
    public partial class VehicleForm : Window
    {
        public User LoggedInUser { get; set; }
        public List<Location> Locations { get; set; }
        public List<Language> Languages { get; set; }


        private readonly VehicleRepository _repository;

        private int _vehicleId;

        public int VehicleId
        {
            get => _vehicleId;
            set
            {
                if(value != _vehicleId)
                {
                    _vehicleId = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _locationId;
        public  Location LocationId
        {
            get => _locationId;
            set
            {
                if(value != _locationId)
                {
                    _locationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxPassengers;
        public int MaxPassengers
        {
            get => _maxPassengers;
            set
            {
                if(value != _maxPassengers)
                {
                    _maxPassengers = value;
                    OnPropertyChanged();
                }
            }
        }

        private Language _languageId;
        public Language LanguageId
        {
            get => _languageId;
            set
            {
                if (value != _languageId)
                {
                    _languageId = value;
                    OnPropertyChanged();
                }
            }
        }

        public VehicleForm(User driver)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = driver;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
