using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.Tourist
{
    public partial class TourReservationForm : Window
    {
        public int UserId { get; set; }
        public Tour SelectedTour { get; private set; }
        public TourInstance SelectedTourInstance { get; private set; }

        private readonly TourInstanceRepository _tourInstanceRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourGuestRepository _tourGuestRepository;

        public TourReservationForm(Tour selectedTour, User user)
        {
            InitializeComponent();

            UserId = user.Id;
            SelectedTour = selectedTour;

            _tourInstanceRepository = new TourInstanceRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourGuestRepository = new TourGuestRepository();

            GenerateDatesComboBox();
        }

        private void GenerateDatesComboBox()
        {
            var tourInstances = _tourInstanceRepository.GetAllById(SelectedTour.Id);
            cmbStartTime.ItemsSource = tourInstances.Select(t => t.StartTime.ToString("g")).ToList();
            cmbNumberOfPeople.IsEnabled = false;
        }

        private void GenerateInputFields(int numberOfPeople)
        {
            spPersonInputs.Children.Clear();

            for (int i = 0; i < numberOfPeople; i++)
            {
                spPersonInputs.Children.Add(new TextBox { Margin = new Thickness(0, 0, 0, 10) }); // First Name
                spPersonInputs.Children.Add(new TextBox { Margin = new Thickness(0, 0, 0, 20) }); // Last Name
            }
        }



        private void CmbStartTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(cmbStartTime.SelectedItem is string selectedTime) || !DateTime.TryParse(selectedTime, out DateTime selectedDate))
            {
                MessageBox.Show("Date not valid.");
                return;
            }

            cmbNumberOfPeople.IsEnabled = true;
            SelectedTourInstance = _tourInstanceRepository.GetByDateAndId(SelectedTour.Id, selectedDate);
        }

        private void CmbNumberOfPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(cmbNumberOfPeople.SelectedItem is ComboBoxItem selectedItem) || !int.TryParse(selectedItem.Content.ToString(), out int numberOfPeople))
            {
                return; 
            }


            if (numberOfPeople > SelectedTourInstance.RemainingSlots)
            {
                int remainingSeats = SelectedTour.MaximumCapacity - (SelectedTourInstance?.RemainingSlots ?? 0);
                MessageBox.Show($"Insufficient spots available for the selected tour. Remaining spots for the selected date: {remainingSeats}.");
                cmbNumberOfPeople.SelectedItem = null;
                return;
            }

            GenerateInputFields(numberOfPeople);
        }


        private int GetSelectedNumberOfPeople(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Content.ToString(), out int numberOfPeople))
            {
                return numberOfPeople;
            }
            return 0;
        }


        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfirmAction("Are you sure?", "Confirmation"))
            {
                return; 
            }

            var guests = GetGuestsFromInputFields();
            var numberOfPeople = GetSelectedNumberOfPeople(cmbNumberOfPeople);


            UpdateTourInstanceCapacity((int)numberOfPeople);
            SaveTourReservation(guests);

            MessageBox.Show("Reservation successful");
        }

        private void UpdateTourInstanceCapacity(int numberOfPeople)
        {
            SelectedTourInstance.RemainingSlots -= numberOfPeople;
            _tourInstanceRepository.Update(SelectedTourInstance);
        }
        private void SaveTourReservation(List<TourGuest> tourGuests)
        {
            TourReservation tourReservation = new TourReservation(SelectedTourInstance.Id, UserId);
            _tourGuestRepository.SaveMultiple(tourGuests);
            _tourReservationRepository.Save(tourReservation); 
        }
        private List<TourGuest> GetGuestsFromInputFields()
        {
            var guests = new List<TourGuest>();

            for (int i = 0; i < spPersonInputs.Children.Count; i += 2)
            {
                var firstNameBox = spPersonInputs.Children[i] as TextBox;
                var lastNameBox = spPersonInputs.Children[i + 1] as TextBox;

                if (firstNameBox != null && lastNameBox != null)
                {
                    string fullName = $"{firstNameBox.Text} {lastNameBox.Text}";
                    var tourGuest = new TourGuest(fullName, SelectedTourInstance.Id, UserId, 0);
                    guests.Add(tourGuest);
                }
            }

            return guests;
        }

        private bool ConfirmAction(string message, string caption)
        {
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
    }
}
