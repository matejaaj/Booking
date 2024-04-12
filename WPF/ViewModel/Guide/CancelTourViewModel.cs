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
            if(SelectedInstance != null && !SelectedInstance.IsCompleted)
            {
                if(SelectedInstance.StartTime > DateTime.Now.AddHours(48)) 
                {
                    List<TourReservation> allToursReservations = _tourReservationService.GetAll();
                    foreach(var tourReservation in allToursReservations)
                    {
                        if(tourReservation.TourInstanceId == SelectedInstance.Id)
                        {
                            Voucher voucher = new Voucher(tourReservation.UserId, DateTime.Now.AddYears(1));
                            _voucherService.Save(voucher);
                        }
                    }

                    _tourInstanceService.Delete(SelectedInstance);

                    MessageBox.Show("Succesfully canceled!");
                }
                else
                {
                    MessageBox.Show("You can cancel tour at least 48 hours before start time!");
                }
            }

        }
    }
}
