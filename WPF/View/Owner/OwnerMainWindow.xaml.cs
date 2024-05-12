using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window, INotifyPropertyChanged
    {
        public Domain.Model.Owner LoggedInOwner { get; set; }
        private string pageName { get; set; }
        public string PageName
        {
            get { return pageName; }
            set
            {
                pageName = value;
                OnPropertyChanged(nameof(PageName));
            }
        }

        public static MainWindowViewModel viewModel { get; set; }

        public OwnerMainWindow(Domain.Model.Owner owner)
        {
            InitializeComponent();
            LoggedInOwner = owner;
            PageName = "Accommodations";
            MainFrame.Navigate(new AccommodationsPage(owner));
            DataContext = this;
        }

        private void ShowRatings_Click(object sender, RoutedEventArgs e)
        {
            PageName = "Ratings";
            ViewRatingsPage page = new ViewRatingsPage(LoggedInOwner);
            MainFrame.Navigate(page);
            SideMenu.Visibility = Visibility.Collapsed;
        }
        
        private void ShowAccommodations_Click(object sender, RoutedEventArgs e)
        {
            PageName = "Accommodations";
            MainFrame.Navigate(new AccommodationsPage(LoggedInOwner));
            SideMenu.Visibility = Visibility.Collapsed;
        }

        private void ShowRescheduling_Click(object sender, RoutedEventArgs e)
        {
            PageName = "Rescheduling Requests";
            MainFrame.Navigate(new ReschedulingOverviewPage(LoggedInOwner));
            SideMenu.Visibility = Visibility.Collapsed;
        }

        private void ShowRenovations_Click(object sender, RoutedEventArgs e)
        {
            PageName = "Renovations";
            MainFrame.Navigate(new ViewRenovationsPage(LoggedInOwner));
            SideMenu.Visibility = Visibility.Collapsed;
        }

        private void ShowMenuBar(object sender, RoutedEventArgs e)
        {
            if (SideMenu.Visibility == Visibility.Collapsed)
            {
                SideMenu.Visibility = Visibility.Visible;
            }
            else
            {
                SideMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void HideMenuBar(object sender, RoutedEventArgs e)
        {
            SideMenu.Visibility = Visibility.Collapsed;
        }

        private void NavigateToPage(Page page)
        {
            MainFrame.Navigate(page);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void SuperOwner_Click(object sender, RoutedEventArgs e)
        {
            PageName = "Super-Owner";
            MainFrame.Navigate(new SuperOwnerPage(LoggedInOwner));
            SideMenu.Visibility = Visibility.Collapsed;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void HideNotifications(object sender, RoutedEventArgs e)
        {
            NotificationMenu.Visibility = Visibility.Collapsed;
        }

        private void ShowNotifications(object sender, RoutedEventArgs e)
        {
            if (NotificationMenu.Visibility == Visibility.Collapsed)
            {
                NotificationMenu.Visibility = Visibility.Visible;
            }
            else
            {
                NotificationMenu.Visibility = Visibility.Collapsed;
            }
        }
    }
}
