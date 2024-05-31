using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guest;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest
{
    public partial class SuperGuestOverview : Page
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
