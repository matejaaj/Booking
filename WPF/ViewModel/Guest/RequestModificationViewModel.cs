using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class RequestModificationViewModel : INotifyPropertyChanged
    {
        private readonly ReservationModificationRequestService _reservationModificationService;
        public AccommodationReservation Reservation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RequestModificationViewModel(AccommodationReservation reservation)
        {
            Reservation = reservation;
            _reservationModificationService = new ReservationModificationRequestService(Injector.CreateInstance<IReservationModificationRequestRepository>());
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SendRequest(DateTime newStartDate, DateTime newEndDate)
        {
            var flag = _reservationModificationService.SendRequest(Reservation.Id, newStartDate, newEndDate);
            if (flag)
            {
                MessageBox.Show("Request sent successfully.");
            }
            else
            {
                MessageBox.Show("Error. Check fields again.");
            }
        }
    }
}