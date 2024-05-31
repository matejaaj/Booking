﻿using BookingApp.WPF.ViewModel.Tourist;
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
using BookingApp.WPF.ViewModel;

namespace BookingApp.WPF.View.Tourist.UserControls
{
    /// <summary>
    /// Interaction logic for RegularDriveControl.xaml
    /// </summary>
    public partial class RegularDriveControl : UserControl
    {
        public RegularDriveControl()
        {
            InitializeComponent();
        }

        private void Minute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;
            if (autoCompleteBox?.SelectedItem != null && DataContext is RegularDriveFormViewModel viewModel)
            {
                viewModel.UpdateDriverList();
            }
        }
    }
}