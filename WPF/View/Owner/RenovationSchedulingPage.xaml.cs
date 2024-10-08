﻿using BookingApp.WPF.ViewModel.Owner;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationSchedulingPage.xaml
    /// </summary>
    public partial class RenovationSchedulingPage : Page
    {
        public static RenovationSchedulingViewModel viewModel { get; set; }
        public RenovationSchedulingPage(Domain.Model.Accommodation accommodation, DTO.AccommodationPageDTO selectedAccommodation)
        {
            InitializeComponent();
            viewModel = new RenovationSchedulingViewModel(accommodation, selectedAccommodation, this);
            DataContext = viewModel;
        }
    }
}
