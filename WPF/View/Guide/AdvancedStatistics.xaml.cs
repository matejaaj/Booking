using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.View.Guide
{
    public partial class AdvancedStatistics : Window
    {
        private readonly AdvancedStatisticsViewModel _viewModel;
        public AdvancedStatistics(TourDTO tourDTO)
        {
            InitializeComponent();
            _viewModel = new AdvancedStatisticsViewModel(tourDTO);
            DataContext = _viewModel;
        }
    }
}
