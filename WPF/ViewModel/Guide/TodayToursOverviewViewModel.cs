using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TodayToursOverviewViewModel
    {
        public ObservableCollection<Tour> TodayTours { get; set; }
        public Tour SelectedTour { get; set; }

        private  TourService _tourService;
        private  TourInstanceService _tourInstanceService;

        public TodayToursOverviewViewModel()
        {
            InitializeServices();
            LoadTodayTours();
        }

        private void LoadTodayTours()
        {
            TodayTours = new ObservableCollection<Tour>(_tourService.LoadTodayTours());
        }

        public void StartTour()
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Select tour first.");
            }
            else
            {
                var tourInstance = _tourInstanceService.GetCurrentTourInstance(SelectedTour.Id);
                    
                if (tourInstance != null)
                {
                    var activeTourOverview = new ActiveTourOverview(SelectedTour.Id, tourInstance.Id);
                    activeTourOverview.ShowDialog();
                    UpdateTodayTours();
                }
            }
        }
        private void UpdateTodayTours()
        {
            TodayTours.Clear();
            LoadTodayTours();
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>(), _tourGuestService, _voucherService);
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _tourService = new TourService(Injector.CreateInstance<ITourRepository>(), _tourGuestService, _tourInstanceService);
        }
    }
}
