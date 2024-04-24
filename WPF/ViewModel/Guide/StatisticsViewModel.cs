using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class StatisticsViewModel : INotifyPropertyChanged
    {
        private  TourService _tourService;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private TourDTO _mostVisitedTour;
        public TourDTO MostVisitedTour
        {
            get { return _mostVisitedTour; }
            set
            {
                if (_mostVisitedTour != value)
                {
                    _mostVisitedTour = value;
                    OnPropertyChanged(nameof(MostVisitedTour));
                }
            }
        }
        public List<object> Years { get; set; }
        public object SelectedYear { get; set; }


        public StatisticsViewModel()
        {
            InitializeServices();

            Years = new List<object>();
            Years.Add("All time");
            for (int i = 0; i < 20; i++)
            {
                Years.Add((DateTime.Now.Year - i).ToString());
            }
        }

        public void Search()
        {
            Tour mostVisitedTour = _tourService.FindMostVisited(SelectedYear);
            MostVisitedTour = new TourDTO(mostVisitedTour);
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>(), _tourGuestService, _voucherService);
            var _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _tourService = new TourService(Injector.CreateInstance<ITourRepository>(), _tourGuestService, _tourInstanceService);
        }
    }
}