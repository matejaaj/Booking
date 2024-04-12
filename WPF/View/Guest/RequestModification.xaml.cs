using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for RequestModificationWindow.xaml
    /// </summary>
    public partial class RequestModification : Window
    {
        private AccommodationReservation reservation;
        private ReservationModificationRequestRepository reservationModificationRepository = new ReservationModificationRequestRepository();
        public RequestModification(AccommodationReservation reservation)
        {
            InitializeComponent();
            this.reservation = reservation;
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime newStartDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime newEndDate = EndDatePicker.SelectedDate ?? DateTime.MinValue;

            if (newStartDate == DateTime.MinValue || newEndDate == DateTime.MinValue)
            {
                StatusTextBlock.Text = "Please select both start and end dates.";
                return;
            }

            if (newStartDate >= newEndDate)
            {
                StatusTextBlock.Text = "End date must be after start date.";
                return;
            }

            var existingRequest = reservationModificationRepository.GetByReservationId(reservation.Id);

            if (existingRequest != null)
            {
                existingRequest.NewStartDate = newStartDate;
                existingRequest.NewEndDate = newEndDate;
                existingRequest.Status = ReservationModificationRequest.RequestStatus.PENDING;
                existingRequest.OwnerComment = "";
                reservationModificationRepository.Update(existingRequest);

                StatusTextBlock.Text = "Request updated successfully.";
            }
            else
            {
                ReservationModificationRequest request = new ReservationModificationRequest(reservation.Id, reservation.StartDate, reservation.EndDate, newStartDate, newEndDate, ReservationModificationRequest.RequestStatus.PENDING, "");
                reservationModificationRepository.Save(request);

                StatusTextBlock.Text = "Request sent successfully.";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
