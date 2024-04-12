using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Application.UseCases;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class AdvancedStatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KeyValuePair<string, int>> AgeGroups { get; set; }

        private readonly List<TourInstance> _tourInstances;
        private readonly List<TourGuest> _tourGuests;

        private readonly TourInstanceService _tourInstanceService;
        private readonly TourGuestService _tourGuestService;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _touristsUnder18;
        public int TouristsUnder18
        {
            get => _touristsUnder18;
            set
            {
                if (_touristsUnder18 != value)
                {
                    _touristsUnder18 = value;
                    OnPropertyChanged(nameof(TouristsUnder18));
                    LoadChartData();
                }
            }
        }

        private int _touristsBetween18And50;
        public int TouristsBetween18And50
        {
            get => _touristsBetween18And50;
            set
            {
                if(_touristsBetween18And50 != value)
                {
                    _touristsBetween18And50 = value;
                    OnPropertyChanged(nameof(TouristsBetween18And50));
                    LoadChartData();
                }
            }

        }

        private int _touristsOver50;
        public int TouristsOver50
        {
            get => _touristsOver50;
            set
            {
                if (_touristsOver50 != value)
                {
                    _touristsOver50 = value;
                    OnPropertyChanged(nameof(TouristsOver50));
                    LoadChartData();
                }
            }

        }

        public AdvancedStatisticsViewModel(TourDTO tour)
        {
            _tourGuestService = new TourGuestService();
            _tourInstanceService = new TourInstanceService();

            _tourInstances = _tourInstanceService.GetAll();
            _tourGuests = _tourGuestService.GetAll();

            AgeGroups = new ObservableCollection<KeyValuePair<string, int>>();

            GetStatistics(tour);
            LoadChartData();
        }

        private void GetStatistics(TourDTO tour)
        {
            var relevantGuests = GetGuestsForTour(tour);

            TouristsUnder18 = CountGuestsByAge(relevantGuests, age => age < 18);
            TouristsBetween18And50 = CountGuestsByAge(relevantGuests, age => age >= 18 && age < 50);
            TouristsOver50 = CountGuestsByAge(relevantGuests, age => age >= 50);

            MessageBox.Show(relevantGuests.Count().ToString() + " " + TouristsUnder18 + " " + TouristsBetween18And50 + " " + TouristsOver50);
        }

        private List<TourGuest> GetGuestsForTour(TourDTO tour)
        {
            List<TourGuest> guestsForTour = new List<TourGuest>();

            foreach (var instance in _tourInstances)
            {
                if(instance.TourId == tour.Id && instance.IsCompleted)
                {
                    MessageBox.Show("Nasao je instancu");
                    foreach (var guest in _tourGuests)
                    {
                        if (guest.TourReservationId == instance.Id)
                        {
                            guestsForTour.Add(guest);
                        }
                    }
                }
            }
            return guestsForTour;
        }


        private int CountGuestsByAge(IEnumerable<TourGuest> guests, Func<int, bool> agePredicate)
        {
            return guests.Count(guest => agePredicate(guest.Age));
        }


        private void LoadChartData()
        {
            AgeGroups.Clear();

            AgeGroups.Add(new KeyValuePair<string, int>("Under 18", TouristsUnder18));
            AgeGroups.Add(new KeyValuePair<string, int>("18 to 50", TouristsBetween18And50));
            AgeGroups.Add(new KeyValuePair<string, int>("Over 50", TouristsOver50));

            Debug.WriteLine("Chart data loaded");
        }

    }
}
