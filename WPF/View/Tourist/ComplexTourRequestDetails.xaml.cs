using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Tourist;
using System.Windows;

namespace BookingApp.WPF.View.Tourist
{

    public partial class ComplexTourRequestDetails : Window
    {
        public ComplexTourRequestDetails(ComplexTourRequestDTO complexTourRequest)
        {
            InitializeComponent();
            DataContext = new ComplexTourRequestDetailsViewModel
            {
                ComplexTourRequest = complexTourRequest
            };
        }
    }
}