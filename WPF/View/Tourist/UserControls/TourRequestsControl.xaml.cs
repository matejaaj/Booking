using BookingApp.Application.UseCases;
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
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.ViewModel.Tourist;
using BookingApp.DTO;

namespace BookingApp.WPF.View.Tourist.UserControls
{
    /// <summary>
    /// Interaction logic for TourRequestsControl.xaml
    /// </summary>
    public partial class TourRequestsControl : UserControl
    {
        public TourRequestsControl()
        {
            InitializeComponent();
        }

    }
}
