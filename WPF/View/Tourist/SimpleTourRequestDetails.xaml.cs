using System.Windows;
using BookingApp.DTO;

namespace BookingApp.WPF.View.Tourist
{

    public partial class SimpleTourRequestDetails : Window
    {
        private TourRequestDTO _tourRequest;

        public SimpleTourRequestDetails(TourRequestDTO tourRequest)
        {
            InitializeComponent();
            _tourRequest = tourRequest;
            this.DataContext = _tourRequest;
        }
    }
}