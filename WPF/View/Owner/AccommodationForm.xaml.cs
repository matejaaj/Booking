using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Guide;
using BookingApp.WPF.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationForm.xaml
    /// </summary>
    public partial class AccommodationForm : Window
    {
        public static AccommodationFormViewModel viewModel { get; set; }

        public AccommodationForm(Domain.Model.Owner owner)
        {
            InitializeComponent();
            viewModel = new AccommodationFormViewModel(owner, this);
            DataContext = viewModel;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                viewModel.btnConfirm_Click(sender, e);  
                Close();
            }
            else
            {
                MessageBox.Show("Please fill all fields",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(txtName.Text) &&
                   !string.IsNullOrWhiteSpace(txtMinimumReservationDays.Text) &&
                   cmbLocation.SelectedItem != null &&
                   cmbType.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(txtCapacity.Text) &&
                   !string.IsNullOrWhiteSpace(txtCancelThreshold.Text);
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.btnAddImage_Click(sender, e);
        }

        private void btnShowImages_Click(object sender, RoutedEventArgs e)
        {
            viewModel.btnShowImages_Click(sender, e);
        }
    }
}
