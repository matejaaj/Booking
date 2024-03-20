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
        public int DropoffLocationid { get; set; }
        public int DetailedDropoffLocationId { get; set; }

        public int SelectedDriverId { get; set; }

        public DriveReservationForm(User user)
        {
            Tourist = user;
            InitializeComponent();
            FillCountries(cbStartCountry);
            FillCountries(cbDestinationCountry);
            FillHours();
        }

        private void FillCountries(ComboBox comboBox)
        {
            LocationRepository _locationRepository = new LocationRepository();
            List<Location> locations = _locationRepository.GetAll();
            var countries = locations.Select(loc => loc.Country).Distinct().OrderBy(c => c).ToList();
            comboBox.Items.Clear();
            foreach (var country in countries)
            {
                comboBox.Items.Add(country);
            }
        }

        private void FillCities(ComboBox cbCountry, ComboBox cbCity)
        {
            var selectedCountry = cbCountry.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedCountry))
            {
                LocationRepository _locationRepository = new LocationRepository();
                var cities = _locationRepository.GetAll()
                    .Where(location => location.Country == selectedCountry)
                    .Select(location => new KeyValuePair<int, string>(location.Id, location.City))
                    .Distinct()
                    .OrderBy(pair => pair.Value)
                    .ToList();

                cbCity.ItemsSource = cities;
                cbCity.DisplayMemberPath = "Value";
                cbCity.SelectedValuePath = "Key";
            }
            else
            {
                cbCity.ItemsSource = null;
            }
        }

        private void FillAddress(ComboBox cbCity, ComboBox cbStreet)
        {
            if (cbCity.SelectedItem != null)
            {
                var selectedCity = (KeyValuePair<int, string>)cbCity.SelectedItem;
                DetailedLocationRepository _detailedLocationRepository = new DetailedLocationRepository();
                var addresses = _detailedLocationRepository.GetAll()
                    .Where(detailedLocation => detailedLocation.LocationId == selectedCity.Key)
                    .Select(detailedLocation => detailedLocation.Address)
                    .Distinct()
                    .OrderBy(address => address)
                    .ToList();

                cbStreet.ItemsSource = addresses;
            }
            else
            {
                cbStreet.ItemsSource = null;
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

        private void FillAddressForCity(ComboBox cbCity, ComboBox cbStreet)
        {
            FillAddress(cbCity, cbStreet);
            if (cbCity.SelectedItem is KeyValuePair<int, string> selectedCity)
            {
                if (cbCity == cbStartCity) PickupLocationId = selectedCity.Key;
                else if (cbCity == cbDestinationCity) DropoffLocationid = selectedCity.Key;
            }
        }

        private void SetDetailedLocationId(ComboBox cbStreet, bool isStartLocation)
        {
            if (cbStreet.SelectedItem != null)
            {
                var selectedAddress = cbStreet.SelectedItem.ToString();
                DetailedLocationRepository _detailedLocationRepository = new DetailedLocationRepository();
                var detailedLocation = _detailedLocationRepository.GetByAddress(selectedAddress);
                if (detailedLocation != null)
                {
                    if (isStartLocation) DetailedPickupLocationId = detailedLocation.Id;
                    else DetailedDropoffLocationId = detailedLocation.Id;
                }
            }
        }

        private DateTime CreateDateTimeFromSelections()
        {
            if (dpDepartureDate.SelectedDate.HasValue &&
                cbDepartureHour.SelectedItem is string selectedHour &&
                cbDepartureMinute.SelectedItem is ComboBoxItem selectedMinuteItem)
            {
                int hour = int.Parse(selectedHour);
                int minute = int.Parse(selectedMinuteItem.Content.ToString());

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

            VehicleRepository vehicleRepository = new VehicleRepository();
            List<int> drivers = vehicleRepository.GetDriverIdsByLocationId(PickupLocationId);
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

        private void cbDrivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDrivers.SelectedItem is ComboBoxItem selectedItem)
            {
                SelectedDriverId = (int)selectedItem.Tag; 
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
                   cbDestinationCountry.SelectedItem != null &&
                   cbDestinationCity.SelectedItem != null &&
                   cbDestinationStreet.SelectedItem != null &&
                   cbDepartureHour.SelectedItem != null &&
                   cbDepartureMinute.SelectedItem != null;
        }


        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            DateTime departure = CreateDateTimeFromSelections();

            DriveReservation driveReservation = new(DetailedPickupLocationId, DetailedDropoffLocationId, departure, SelectedDriverId, Tourist.Id, 2, 0);
            DriveReservationRepository driveReservationRepository = new DriveReservationRepository();
            driveReservationRepository.Save(driveReservation);
            MessageBox.Show("Reservation succesfful");
            this.Close();
        }
            
        private void cbDepartureMinute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDriverList();
        }

        private void cbStartCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCities(cbStartCountry, cbStartCity);
        }

        private void cbDestinationCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCities(cbDestinationCountry, cbDestinationCity);
        }

        private void cbStartCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillAddressForCity(cbStartCity, cbStartStreet);
        }

        private void cbDestinationCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillAddressForCity(cbDestinationCity, cbDestinationStreet);
        }

        private void cbStartStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDetailedLocationId(cbStartStreet, true);
        }

        private void cbDestinationStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDetailedLocationId(cbDestinationStreet, false);
        }
    }
}
