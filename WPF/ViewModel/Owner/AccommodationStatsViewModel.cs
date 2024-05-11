using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class AccommodationStatsViewModel : INotifyPropertyChanged
    {
        public AccommodationStatisticsDTO SelectedYear { get; set; }
        public string BusiestYear { get; set; } 
        public Accommodation Accommodation { get; set; }
        private static AccommodationReservationService _accommodationReservationService;
        private static AccommodationService _accommodationService;
        private static ReservationModificationRequestService _reservationModificationRequestService;
        private ObservableCollection<AccommodationStatisticsDTO> yearlyStats;
        public ObservableCollection<AccommodationStatisticsDTO> YearlyStats
        {
            get { return yearlyStats; }
            set
            {
                yearlyStats = value;
                OnPropertyChanged(nameof(YearlyStats));
            }
        }

        public AccommodationStatsViewModel(Accommodation accommodation)
        {
            this.Accommodation = accommodation;
            InitializeServices();
            YearlyStats = new ObservableCollection<AccommodationStatisticsDTO>();
            Update();
        }

        private void Update()
        {
            YearlyStats.Clear();
            YearlyStats = new ObservableCollection<AccommodationStatisticsDTO>(_accommodationService.GenerateStatisticsByYear(Accommodation));
            BusiestYear = "Busiest Year: " + _accommodationService.GetBusiestYear(YearlyStats);
        }

        private void InitializeServices()
        {
            _accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            _reservationModificationRequestService = new ReservationModificationRequestService(Injector.CreateInstance<IReservationModificationRequestRepository>());
            _accommodationService = new AccommodationService(_accommodationReservationService, _reservationModificationRequestService, Injector.CreateInstance<IAccommodationRepository>());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e, AccommodationStatsPage accommodationStatsPage)
        {
            AccommodationMonthlyStatsPage page = new AccommodationMonthlyStatsPage(SelectedYear, Accommodation);
            accommodationStatsPage.NavigationService.Navigate(page);
        }
    }
}
