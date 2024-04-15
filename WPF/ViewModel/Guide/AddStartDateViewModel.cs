using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class AddStartDateViewModel
    {
        private List<TourInstance> _startDates;
        private int _tourId;
        private int _capacity;

        public AddStartDateViewModel(List<TourInstance> startDates, int tourId, int capacity)
        {
            _startDates = startDates;
            _tourId = tourId;
            _capacity = capacity;
            LoadTimeComboboxes();
        }

        public void Confirm()
        {
            if (StartDate.HasValue && StartHourSelectedItem != null && StartMinuteSelectedItem != null)
            {
                DateTime startDate = StartDate.Value.Date;
                int hour = int.Parse(StartHourSelectedItem.ToString());
                int minute = int.Parse(StartMinuteSelectedItem.ToString());
                startDate = startDate.AddHours(hour).AddMinutes(minute);

                TourInstance newStartDate = new TourInstance(_tourId, _capacity, startDate);

                _startDates.Add(newStartDate);

                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Not added", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public DateTime? StartDate { get; set; }
        public object StartHourSelectedItem { get; set; }
        public object StartMinuteSelectedItem { get; set; }

        private void LoadTimeComboboxes()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                StartHourItems.Add(hour.ToString("00"));
            }

            for (int minute = 0; minute < 60; minute++)
            {
                StartMinuteItems.Add(minute.ToString("00"));
            }

            StartHourSelectedItem = StartHourItems[0];
            StartMinuteSelectedItem = StartMinuteItems[0];
        }

        public List<string> StartHourItems { get; } = new List<string>();
        public List<string> StartMinuteItems { get; } = new List<string>();
    }
}
