using BookingApp.WPF.ViewModel.Owner;
using Microsoft.Windows.Controls;
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
    /// Interaction logic for AccommodationStatsPage.xaml
    /// </summary>
    public partial class AccommodationStatsPage : Page
    {
        public static AccommodationStatsViewModel viewModel { get; set; }
        public AccommodationStatsPage(Domain.Model.Accommodation accommodation)
        {
            InitializeComponent();
            viewModel = new AccommodationStatsViewModel(accommodation);
            DataContext = viewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.DataGrid_SelectionChanged(sender, e, this);
        }
    }
}
