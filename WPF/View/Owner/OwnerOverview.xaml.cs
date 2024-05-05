using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OwnerOverview.xaml
    /// </summary>
    public partial class OwnerOverview : Window
    {
        public static OwnerOverviewViewModel viewModel { get; set; }

        public OwnerOverview(Domain.Model.Owner owner)
        {
            InitializeComponent();
            viewModel = new OwnerOverviewViewModel(owner);
            DataContext = viewModel;

            Loaded += OwnerOverview_Loaded;
        }

        private void OwnerOverview_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.OwnerOverview_Loaded(sender, e);
        }

        private void ShowCreateAccommodationForm(object sender, RoutedEventArgs e)
        {
            //viewModel.ShowCreateAccommodationForm(sender, e);
        }

        private void ShowViewAccommodation(object sender, RoutedEventArgs e)
        {
            viewModel.ShowViewAccommodation(sender, e);
        }
        
        private void ShowRatingsButton(object sender, RoutedEventArgs e)
        {
            //viewModel.ShowRatingsButton(sender, e);
        }
        private void SuperTrophyButton(object sender, RoutedEventArgs e)
        {
            viewModel.SuperTrophyButton(sender, e);
        }

        private void ReschedulingButton(object sender, RoutedEventArgs e)
        {
           // viewModel.ReschedulingButton(sender, e);
        }
    }
}
