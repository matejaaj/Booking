using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.Model.BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class CancelTourViewModel
    {
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        public TourInstance SelectedInstance { get; set; }

        private readonly TourInstanceService _tourInstanceService;
        private readonly TourReservationService _tourReservationService;
        private readonly VoucherService _voucherService;
        public CancelTourViewModel(int tourId)
        {
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            _tourInstanceService = new TourInstanceService();
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
    }
}
