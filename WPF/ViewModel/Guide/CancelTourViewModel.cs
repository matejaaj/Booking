using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.Model.BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class CancelTourViewModel
    {
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        public TourInstance SelectedInstance { get; set; }

        private  TourInstanceService _tourInstanceService;
        private  TourReservationService _tourReservationService;
        private  VoucherService _voucherService;
        public CancelTourViewModel(int tourId)
        {
            InitializeServices();
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceService.GetAllByTourId(tourId));
        }
        public void CancelTour()
        {
            if (SelectedInstance == null || SelectedInstance.IsCompleted)
            {
                return;
            }

            if (IsCancellationAllowed())
            {
                _tourInstanceService.CancelTour(SelectedInstance);
                MessageBox.Show("Successfully canceled!");
            }
            else
            {
                MessageBox.Show("You can cancel tour at least 48 hours before start time!");
            }
        }
        private bool IsCancellationAllowed()
        {
            return SelectedInstance.StartTime > DateTime.Now.AddHours(48);
        }

        private void InitializeServices()
        {
            _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>(), _tourGuestService, _voucherService);
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
        }
    }
}
