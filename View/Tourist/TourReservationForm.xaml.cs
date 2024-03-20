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
        public int TouristId { get; set; }
        public Tour SelectedTour { get; private set; }
        public int NumberOfPeople { get; set; }

        public TourInstance SelectedTourInstance { get; private set; }
        public TourInstanceRepository _tourInstanceRepository { get; set; }
        public TourReservationRepository _tourReservationRepository { get; set; }
        public TourGuestRepository _tourGuestRepository { get; set; }

        public TourReservationForm(Tour selectedTour, User loggedUser)
        {
            TouristId = loggedUser.Id;
            SelectedTour = selectedTour;

            InitializeComponent();
            InitializeRepositories();
            FillDates();
        }

        private void InitializeRepositories()
        {
            _tourInstanceRepository = new TourInstanceRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourGuestRepository = new TourGuestRepository();
        }

        private void FillDates()
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
                spPersonInputs.Children.Add(new TextBox { Margin = new Thickness(0, 0, 0, 10) }); 
                spPersonInputs.Children.Add(new TextBox { Margin = new Thickness(0, 0, 0, 20) });
                spPersonInputs.Children.Add(new TextBox { Margin = new Thickness(0, 0, 0, 20) });
            }
        }

        private void CmbStartTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = DateTime.Parse(cmbStartTime.SelectedItem.ToString());
            cmbNumberOfPeople.IsEnabled = true;
            SelectedTourInstance = _tourInstanceRepository.GetByDateAndId(SelectedTour.Id, selectedDate);
        }

        private void CmbNumberOfPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumberOfPeople =  GetSelectedNumberOfPeople();

            if (NumberOfPeople > SelectedTourInstance.RemainingSlots)
            {  
                MessageBox.Show($"Insufficient spots available for the selected tour. Remaining spots for the selected date: {SelectedTourInstance.RemainingSlots}.");
                cmbNumberOfPeople.SelectedItem = null;
                return;
            }

            GenerateInputFields(NumberOfPeople);
        }

        private int GetSelectedNumberOfPeople()
        {
            if (cmbNumberOfPeople.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Content.ToString(), out int numberOfPeople))
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
            UpdateTourInstanceCapacity(NumberOfPeople);
            SaveTourReservation(guests);

            MessageBox.Show("Reservation successful");
        }

        private bool ConfirmAction(string message, string caption)
        {
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        private List<TourGuest> GetGuestsFromInputFields()
        {
            var guests = new List<TourGuest>();

            for (int i = 0; i < spPersonInputs.Children.Count; i += 3)
            {
                var firstNameBox = spPersonInputs.Children[i] as TextBox;
                var lastNameBox = spPersonInputs.Children[i + 1] as TextBox;
                var ageBox = spPersonInputs.Children[i+2] as TextBox;

                if (firstNameBox != null && lastNameBox != null && ageBox != null)
                {
                    string fullName = $"{firstNameBox.Text} {lastNameBox.Text}";
                    int age = int.Parse(ageBox.Text);
                    var tourGuest = new TourGuest(fullName, age, SelectedTourInstance.Id, TouristId, 0);
                    guests.Add(tourGuest);
                }
            }
            return guests;
        }

        private void UpdateTourInstanceCapacity(int numberOfPeople)
        {
            SelectedTourInstance.RemainingSlots -= numberOfPeople;
            _tourInstanceRepository.Update(SelectedTourInstance);
        }

        private void SaveTourReservation(List<TourGuest> tourGuests)
        {
            TourReservation tourReservation = new TourReservation(SelectedTourInstance.Id, TouristId);
            _tourGuestRepository.SaveMultiple(tourGuests);
            _tourReservationRepository.Save(tourReservation);
        }
    }
}
