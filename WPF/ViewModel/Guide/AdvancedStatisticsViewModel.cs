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
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class AdvancedStatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KeyValuePair<string, int>> AgeGroups { get; set; }

        private  TourInstanceService _tourInstanceService;

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
            InitializeServices();

            AgeGroups = new ObservableCollection<KeyValuePair<string, int>>();

            LoadStatistics(tour);
            LoadChartData();
        }

        public void LoadStatistics(TourDTO tour)
        {
            var statistics = _tourInstanceService.GetStatistics(tour.Id);

            TouristsUnder18 = statistics.TouristsUnder18;
            TouristsBetween18And50 = statistics.TouristsBetween18And50;
            TouristsOver50 = statistics.TouristsOver50;
        }

        private void LoadChartData()
        {
            AgeGroups.Clear();

            AgeGroups.Add(new KeyValuePair<string, int>("Under 18", TouristsUnder18));
            AgeGroups.Add(new KeyValuePair<string, int>("18 to 50", TouristsBetween18And50));
            AgeGroups.Add(new KeyValuePair<string, int>("Over 50", TouristsOver50));

            Debug.WriteLine("Chart data loaded");
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
        }

    }
}
