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
using System.Windows.Shapes;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsWindow.xaml
    /// </summary>
    public partial class TourRequestStatisticsWindow : Window
    {
        public TourStatisticsViewModel ViewModel { get; }


        public TourRequestStatisticsWindow(User loggedUser, TourRequestService requestService, TourRequestSegmentService segmentService)
        {
            InitializeComponent();
            ViewModel = new TourStatisticsViewModel(loggedUser.Id, requestService, segmentService);
            DataContext = ViewModel;
        }
    }
}
