using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Validation;
using BookingApp.WPF.View.Guide;
using BookingApp.WPF.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for VehicleForm.xaml
    /// </summary>
    public partial class VehicleForm : Page
    {
        public VehicleViewModel VM {  get; set; }
        public VehicleForm(int userId)
        {
            InitializeComponent();
            VM = new VehicleViewModel(userId);
            DataContext = VM;
        }

      
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            VM.btnConfirm_Click(sender, e);
            NavigationService.GoBack();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            VM.btnAddImage_Click(sender, e, this);
        }

        private void btnShowImages_Click(object sender, RoutedEventArgs e)
        {
            VM.btnShowImages_Click(sender, e, this);
        }


    }
}
