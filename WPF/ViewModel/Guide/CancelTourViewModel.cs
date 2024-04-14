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
                CancelTourAndIssueVoucher();
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

        private void CancelTourAndIssueVoucher()
        {
            IssueVouchersToTourParticipants();
            _tourInstanceService.Delete(SelectedInstance);
        }

        private void IssueVouchersToTourParticipants()
        {
            List<TourReservation> allToursReservations = _tourReservationService.GetAll();
            foreach (var tourReservation in allToursReservations)
            {
                if (tourReservation.TourInstanceId == SelectedInstance.Id)
                {
                    IssueVoucher(tourReservation.UserId);
                }
            }
        }

        private void IssueVoucher(int userId)
        {
            Voucher voucher = new Voucher(userId, DateTime.Now.AddYears(1));
            _voucherService.Save(voucher);
        }

    }
}
