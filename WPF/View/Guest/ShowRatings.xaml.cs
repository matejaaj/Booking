using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Guest;
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

namespace BookingApp.WPF.View.Guest
{
    /// <summary>
    /// Interaction logic for ShowRatings.xaml
    /// </summary>
    public partial class ShowRatings : Window
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
