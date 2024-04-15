using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class RequestModificationViewModel : INotifyPropertyChanged
    {
        private readonly ReservationModificationRequestService _reservationModificationService;
        public AccommodationReservation Reservation { get; set; }
        public string StatusMessage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RequestModificationViewModel(AccommodationReservation reservation, ReservationModificationRequestService reservationModificationService)
        {
            Reservation = reservation;
            _reservationModificationService = reservationModificationService;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SendRequest(DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate == DateTime.MinValue || newEndDate == DateTime.MinValue)
            {
                StatusMessage = "Please select both start and end dates.";
                OnPropertyChanged(nameof(StatusMessage));
                return;
            }

            if (newStartDate >= newEndDate)
            {
                StatusMessage = "End date must be after start date.";
                OnPropertyChanged(nameof(StatusMessage));
                return;
            }

            var existingRequest = _reservationModificationService.GetByReservationId(Reservation.Id);

            if (existingRequest != null)
            {
                existingRequest.NewStartDate = newStartDate;
                existingRequest.NewEndDate = newEndDate;
                existingRequest.Status = ReservationModificationRequest.RequestStatus.PENDING;
                existingRequest.OwnerComment = "";
                _reservationModificationService.Update(existingRequest);

                StatusMessage = "Request updated successfully.";
            }
            else
            {
                var request = new ReservationModificationRequest(
                    Reservation.Id, Reservation.StartDate, Reservation.EndDate,
                    newStartDate, newEndDate, ReservationModificationRequest.RequestStatus.PENDING, "");

                _reservationModificationService.Save(request);

                StatusMessage = "Request sent successfully.";
            }

            OnPropertyChanged(nameof(StatusMessage));
        }
    }
}