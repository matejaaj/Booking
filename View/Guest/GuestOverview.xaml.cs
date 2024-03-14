using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace BookingApp.View.Guest
{
    public partial class GuestOverview : Window
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public List<Location> locations { get; set; }
        public string SelectedLocation { get; set; }
        public User LoggedInGuest { get; set; }

        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        public GuestOverview(User guest)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInGuest = guest;
            _accommodationRepository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            _locationRepository = new LocationRepository();
            locations = _locationRepository.GetAll();
            ButtonSearch.Click += ButtonSearch_Click;
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                string name = NameTextBox.Text.Trim();
                string locationString = SelectedLocation;
                int locationId = 0;
                string type = ((ComboBoxItem)TypeCmbBox.SelectedItem).Content.ToString();
                int guests = Convert.ToInt32(GuestsTextBox.Text);
                int days = Convert.ToInt32(DaysTextBox.Text);

                foreach (var loc in locations)
                {
                    if (loc.ToString().Equals(locationString))
                        locationId = loc.Id;
                }

                AccommodationsDataGrid.ItemsSource = Accommodations.Where(accommodation =>
                    accommodation.Name.ToLower().Contains(name.ToLower()) &&
                    accommodation.MaxGuests >= guests && guests > 0 &&
                    accommodation.MinReservations <= days &&
                    accommodation.Type.ToString().ToLower().Equals(type.ToLower()) &&
                    accommodation.LocationId == locationId
                    );
            }

        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                                     !string.IsNullOrWhiteSpace(GuestsTextBox.Text) &&
                                     !string.IsNullOrWhiteSpace(DaysTextBox.Text) &&
                                     TypeCmbBox.SelectedItem != null &&
                                     !string.IsNullOrWhiteSpace(SelectedLocation);
        }

        private void ReserveAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccommodation = (sender as Button)?.DataContext as Accommodation;
            AccommodationReservationForm reservationAccommodationWindow = new AccommodationReservationForm(selectedAccommodation, LoggedInGuest);
            reservationAccommodationWindow.Show();
        }

    }
}