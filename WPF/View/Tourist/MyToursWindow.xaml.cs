using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace BookingApp.WPF.View.Tourist
{
    public partial class MyToursWindow : Window
    {
        public User User { get; set; }
        public MyToursViewModel ViewModel { get; set; }

        public MyToursWindow(User user)
        {
            InitializeComponent();
            User = user;
            ViewModel = new MyToursViewModel(user);
            DataContext = ViewModel;
        }

        private void MoreDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tour = button.Tag as TourInstanceViewModel;
            if (tour != null)
            {
                var detailsWindow = new MyTourMoreDetailsWindow(tour);
                detailsWindow.Show(); 
            }

        }
    
        private void RateTour_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tour = button.Tag as TourInstanceViewModel;
            if (tour != null)
            {
                if (!tour.IsFinished)
                {
                    MessageBox.Show("Tura nije gotova i dalje");
                    return;
                }

                if (ViewModel.CheckIfAlreadyReveiewed(User.Id, tour.Id))
                {
                    MessageBox.Show("Tura je već ocenjena");
                    return;
                }

                var reviewWindow = new ReviewTourWindow(tour, User.Id);
                reviewWindow.Show();
            }
        }




    }
}
