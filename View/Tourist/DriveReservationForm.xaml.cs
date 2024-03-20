using BookingApp.Model;
using BookingApp.Repository;
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

namespace BookingApp.View.Tourist
{
    /// <summary>
    /// Interaction logic for DriveFrom.xaml
    /// </summary>
    public partial class DriveReservationForm : Window
    {
        public User Tourist;
        public int PickupLocationId { get; set; }
        public int DetailedPickupLocationId { get; set; }
        public int DetailedDropoffLocationId { get; set; }
        public int SelectedDriverId { get; set; }


        VehicleRepository _vehicleRepository { get; set; }
        DetailedLocationRepository _detailedLocationRepository { get; set; }
        LocationRepository _locationRepository { get; set; }
        DriveReservationRepository _driveReservationRepository { get; set; }

        public DriveReservationForm(User loggedUser)
        {
            InitializeRepositories();
            InitializeComponent();
            FillInitialComboBoxValues();

            Tourist = loggedUser;
        }


        private void InitializeRepositories()
        {
            _detailedLocationRepository = new DetailedLocationRepository();
            _locationRepository = new LocationRepository();
            _vehicleRepository = new VehicleRepository();
            _driveReservationRepository = new DriveReservationRepository();
        }

        private void FillInitialComboBoxValues()
        {
            FillCountries(cbStartCountry);
            FillHours();
            FillMinutes();
        }

        private void FillCountries(ComboBox comboBox)
        {
            List<Location> locations = _locationRepository.GetAll();
            var countries = locations.Select(loc => loc.Country).Distinct().OrderBy(c => c).ToList();

            comboBox.Items.Clear();
            foreach (var country in countries)
            {
                comboBox.Items.Add(country);
            }
        }

        private void FillHours()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                string hourText = hour.ToString("00");
                cbDepartureHour.Items.Add(hourText);
            }
        }


        private void FillMinutes()
        {
            for (int minute = 0; minute < 60; minute += 15)
            {
                string minuteText = minute.ToString("00");
                cbDepartureMinute.Items.Add(minuteText);
            }
        }


        private void FillCities()
        {
            var selectedCountry = cbStartCountry.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCountry))
            {
                cbStartCity.ItemsSource = null;
                return;
            }

            var cities = _locationRepository.GetAll()
                .Where(location => location.Country == selectedCountry)
                .Select(location => new KeyValuePair<int, string>(location.Id, location.City))
                .Distinct()
                .OrderBy(pair => pair.Value)
                .ToList();

            cbStartCity.ItemsSource = cities;
            cbStartCity.DisplayMemberPath = "Value";
            cbStartCity.SelectedValuePath = "Key";
 
        }

        private void FillAddress()
        {
            var selectedCity = (KeyValuePair<int, string>)cbStartCity.SelectedItem;

            var addresses = _detailedLocationRepository.GetAll()
                .Where(detailedLocation => detailedLocation.LocationId == selectedCity.Key)
                .Select(detailedLocation => detailedLocation.Address)
                .Distinct()
                .OrderBy(address => address)
                .ToList();

            cbStartStreet.ItemsSource = addresses;
            cbDestinationStreet.ItemsSource = addresses;
            PickupLocationId = selectedCity.Key;
        }
        private void SetDetailedLocationId(ComboBox cbStreet, bool isStartLocation)
        {
            if (cbStreet.SelectedItem == null) return;

            var selectedAddress = cbStreet.SelectedItem.ToString();
            var detailedLocation = _detailedLocationRepository.GetByAddress(selectedAddress);
            if (detailedLocation != null)
            {
                if (isStartLocation) DetailedPickupLocationId = detailedLocation.Id;
                else DetailedDropoffLocationId = detailedLocation.Id;
            }
        }

        private DateTime CreateDateTimeFromSelections()
        {
            if (dpDepartureDate.SelectedDate.HasValue &&
                cbDepartureHour.SelectedItem is string selectedHour &&
                cbDepartureMinute.SelectedItem is string selectedMinute)
            {
                int hour = int.Parse(selectedHour);
                int minute = int.Parse(selectedMinute);

                return new DateTime(dpDepartureDate.SelectedDate.Value.Year, dpDepartureDate.SelectedDate.Value.Month, dpDepartureDate.SelectedDate.Value.Day, hour, minute, 0);
            }
            else
            {
                MessageBox.Show("Please select a valid date and time.");
                return DateTime.MinValue;
            }
        }

        private void UpdateDriverList()
        {
            if (!AreAllCriteriaMet())
            {
                MessageBox.Show("Enter all values.");
                return;
            }

            List<int> drivers = _vehicleRepository.GetDriverIdsByLocationId(PickupLocationId);
            DateTime? date = CreateDateTimeFromSelections();
            drivers = FilterDrivers(drivers, date);
            FillDriverComboBox(drivers);


        }
        private void FillDriverComboBox(List<int> driverIds)
        {
            UserRepository userRepository = new UserRepository();
            List<User> drivers = userRepository.GetByIds(driverIds);

            cbDrivers.Items.Clear();

            foreach (var driver in drivers)
            {
                ComboBoxItem item = new ComboBoxItem
                {
                    Content = driver.Username,
                    Tag = driver.Id 
                };
                cbDrivers.Items.Add(item);
            }
        }



        private List<int> FilterDrivers(List<int> driverIds, DateTime? targetStartTime)
        {
            DriveReservationRepository driveReservationRepository = new DriveReservationRepository();
            var scheduledDrivers = driveReservationRepository.GetAll()
                                    .Where(reservation => reservation.DepartureTime == targetStartTime && driverIds.Contains(reservation.DriverId))
                                    .Select(reservation => reservation.DriverId)
                                    .Distinct()
                                    .ToList();

            return driverIds.Except(scheduledDrivers).ToList();
        }

        private bool AreAllCriteriaMet()
        {
            return dpDepartureDate.SelectedDate != null &&
                   cbStartCountry.SelectedItem != null &&
                   cbStartCity.SelectedItem != null &&
                   cbStartStreet.SelectedItem != null &&
                   cbDestinationStreet.SelectedItem != null &&
                   cbDepartureHour.SelectedItem != null &&
                   cbDepartureMinute.SelectedItem != null;
        }


        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            DateTime departure = CreateDateTimeFromSelections();

            DriveReservation driveReservation = new(DetailedPickupLocationId, DetailedDropoffLocationId, departure, SelectedDriverId, Tourist.Id, 2, 0);
            _driveReservationRepository.Save(driveReservation);
            MessageBox.Show("Reservation succesfful");
            this.Close();
        }
        
        private void cbDepartureMinute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDriverList();
        }
        private void cbStartCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCities();
        }
        private void cbStartCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillAddress();
        }
        private void cbStartStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDetailedLocationId(cbStartStreet, true);
        }

        private void cbDestinationStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDetailedLocationId(cbDestinationStreet, false);
        }


        private void cbDrivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDrivers.SelectedItem is ComboBoxItem selectedItem)
            {
                SelectedDriverId = (int)selectedItem.Tag;
            }
        }
    }
}
