using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guest;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest
{
    public partial class ShowRatings : Page
    {
        private readonly ShowRatingsViewModel _viewModel;

        public ShowRatings(User loggedInGuest)
        {
            InitializeComponent();
            _viewModel = new ShowRatingsViewModel(loggedInGuest);
            DataContext = _viewModel;
        }
    }
}
