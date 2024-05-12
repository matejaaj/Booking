using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Windows;
using System.Diagnostics;
using BookingApp.WPF.ViewModel.Guest;

namespace BookingApp.WPF.View.Guest
{
    public partial class SuperGuestOverview : Window
    {
        private readonly SuperGuestOverviewViewModel _viewModel;
        public SuperGuestOverview(User loggedInGuest)
        {
            InitializeComponent();
            _viewModel = new SuperGuestOverviewViewModel(loggedInGuest);
            DataContext = _viewModel;
        }
    }
}
