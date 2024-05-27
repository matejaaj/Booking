using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Tourist;
using System.Windows;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourDetailsWindow.xaml
    /// </summary>
    public partial class TourDetailsWindow : Window
    {
        public TourDetailsWindow(TourDTO tour, User user)
        {
            InitializeComponent();
            DataContext = new TourDetailsViewModel(tour, user);
        }
    }
}