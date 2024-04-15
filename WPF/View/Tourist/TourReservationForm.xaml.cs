using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.Model.BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.Tourist
{
    public partial class TourReservationForm : Window
    {
        private TourReservationFormViewModel _viewModel;

        public TourReservationForm(TourDTO selectedTour, User loggedUser)
        {
            InitializeComponent();
            _viewModel = new TourReservationFormViewModel(selectedTour, loggedUser);
            this.DataContext = _viewModel;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveReservation();
        }
    }
}
