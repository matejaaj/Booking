using System.Windows;
using System.Windows.Controls;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Guest;

namespace BookingApp.WPF.View.Guest
{
    public partial class RenovationRecommendationForm : Window
    {
        private readonly RenovationRecommendationViewModel viewModel;

        public RenovationRecommendationForm(RateForm rateForm, int reservationId)
        {
            InitializeComponent();
            viewModel = new RenovationRecommendationViewModel(rateForm, reservationId);
            DataContext = viewModel;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Submit();
            Close();
        }
    }
}
