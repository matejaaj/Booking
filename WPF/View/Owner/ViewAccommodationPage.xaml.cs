﻿using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Owner;
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
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for ViewAccommodationPage.xaml
    /// </summary>
    public partial class ViewAccommodationPage : Page
    {
        public static ViewAccommodationViewModel viewModel { get; set; }
        public ViewAccommodationPage(AccommodationPageDTO accommodation)
        {
            InitializeComponent();
            viewModel = new ViewAccommodationViewModel(accommodation, this);
            DataContext = viewModel;
        }
    }
}
