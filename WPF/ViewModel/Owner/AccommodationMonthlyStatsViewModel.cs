using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class AccommodationMonthlyStatsViewModel : INotifyPropertyChanged
    {
        private AccommodationStatisticsDTO SelectedYear { get; set; }
        public string BusiestMonth { get; set; }
        private Accommodation Accommodation { get; set; }
        private static AccommodationReservationService _accommodationReservationService;
        private static AccommodationService _accommodationService;
        private static ReservationModificationRequestService _reservationModificationRequestService;

        private ObservableCollection<AccommodationStatisticsDTO> monthlyStats;
        public ObservableCollection<AccommodationStatisticsDTO> MonthlyStats
        {
            get { return monthlyStats; }
            set
            {
                monthlyStats = value;
                OnPropertyChanged(nameof(MonthlyStats));
            }
        }
        public AccommodationMonthlyStatsViewModel(AccommodationStatisticsDTO selectedYear, Accommodation accommodation)
        {
            InitializeServices();
            SelectedYear = selectedYear;
            Accommodation = accommodation;
            MonthlyStats = new ObservableCollection<AccommodationStatisticsDTO>();
            Update();
        }

        private void Update()
        {
            MonthlyStats.Clear();
            MonthlyStats = new ObservableCollection<AccommodationStatisticsDTO>(_accommodationService.GenerateStatisticsByMonth(SelectedYear, Accommodation));
            BusiestMonth = "Busiest Month: "+_accommodationService.GetBusiestMonth(MonthlyStats);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitializeServices()
        {
            _accommodationReservationService = new AccommodationReservationService();
            _reservationModificationRequestService = new ReservationModificationRequestService();
            _accommodationService = new AccommodationService(_accommodationReservationService, _reservationModificationRequestService);
        }
    }
}
