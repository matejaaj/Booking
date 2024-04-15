using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application.UseCases;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest
{
    public partial class RequestModification : Window
    {
        private RequestModificationViewModel _viewModel;

        public RequestModification(AccommodationReservation reservation)
        {
            InitializeComponent();
            _viewModel = new RequestModificationViewModel(reservation, new ReservationModificationRequestService());
            DataContext = _viewModel;
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime newStartDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime newEndDate = EndDatePicker.SelectedDate ?? DateTime.MinValue;
            _viewModel.SendRequest(newStartDate, newEndDate);
            StatusTextBlock.Text = _viewModel.StatusMessage;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}