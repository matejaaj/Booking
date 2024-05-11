using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class AddStartDateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<TourInstance> _startDates;
        public ObservableCollection<TourInstance> StartDates { get; set; }
        private int _tourId;
        private int _capacity;
        public DateTime? SelectedDate { get; set; }

        public AddStartDateViewModel(List<TourInstance> startDates, int tourId, int capacity)
        {
            _startDates = startDates;
            _tourId = tourId;
            _capacity = capacity;
            StartDates = new ObservableCollection<TourInstance>(startDates);
        }

        public void Add()
        {
            if (SelectedDate.HasValue && SelectedTime != null)
            {
                DateTime combinedDateTime = SelectedDate.Value.Date + SelectedTime;
                TourInstance newStartDate = new TourInstance(_tourId, _capacity, combinedDateTime);

                MessageBox.Show(SelectedTime.ToString());

                _startDates.Add(newStartDate);
                StartDates.Add(newStartDate);
            }
           
        }
    }
}
