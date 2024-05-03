using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for ViewAccommodation.xaml
    /// </summary>
    public partial class ViewAccommodation : Window
    {
        public static ViewAccommodationViewModel viewModel { get; set; }    
        public ViewAccommodation(Accommodation accommodation)
        {
            InitializeComponent();
            viewModel = new ViewAccommodationViewModel(accommodation);
            DataContext = viewModel;
        }

        private void RecentReservationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.RecentReservationsListBox_SelectionChanged(sender, RecentReservationsListBox);
        }

    }
}
