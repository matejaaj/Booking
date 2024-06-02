using BookingApp.Application;
using BookingApp.Application.Events;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Driver;
using BookingApp.WPF.ViewModel.Driver;
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

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IEventAggregator EventAggregator { get; } = new SimpleEventAggregator();
        private MainWindowViewModel VM {  get; set; }
        private Page currentPage { get; set; }
        private User driver;
        public bool? DialogOverlayResult {  get; set; }

        public MainWindow(User user)
        {
            driver = user;
            InitializeComponent();
            System.Windows.Application.Current.MainWindow = this;
            DialogOverlayResult = false;
            MainNavigationFrame.Navigate(new DriverOverview(user));
            EventAggregator.Subscribe<ShowMessageEvent>(e => MessageOverlay.Show(e.Message, e.Title));
            EventAggregator.Subscribe<ShowDialogEvent>(e =>
            {
                MessageOverlay.DialogResultReceived += HandeDialogResult;
                MessageOverlay.ShowDialog(e.Message, e.Title);
            });
            EventAggregator.Subscribe<MenuItemsEvent>(e => ChangeVisibility(e.Visibility));
            VM = new MainWindowViewModel(user);
            DataContext = VM;
            lbl_Page.Text = "Home page";
        }

        private void ChangeVisibility(Visibility visibility)
        {
            BtnDrive.Visibility = visibility;
            VM.ChangeVisibility(visibility);
        }

        private void HandeDialogResult(bool result)
        {
            DialogOverlayResult = result;
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

        private void btnStats_Click(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null)
            {
                VM.btnStats_Click(sender, e, currentPage);
                lbl_Page.Text = "Stats";
            }
        }

        public void btnData_Click(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null)
            {
                VM.btnData_Click(sender, e, currentPage);
                lbl_Page.Text = "User";
            }
        }
        private void ShowCreateVehicleForm(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null)
            {
                VM.ShowCreateVehicleForm(sender, e, currentPage);
                lbl_Page.Text = "Register vehicle";
            }
        }

        public void btnTutorial_Click(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null)
            {
                VM.btnTutorial_Click(sender,e,currentPage);
                lbl_Page.Text = "Tutorial";
            }
        }
        private void btnVacatioRequest_Click(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null)
            {
                VM.btnVacatioRequest_Click(sender,e, currentPage);
                lbl_Page.Text = "Vacation request";
            }
        }

        private void btnVacationReports_Click(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null)
            {
                VM.btnVacationReports_Click(sender,e,currentPage);
                lbl_Page.Text = "Vacation reports";
            }
        }
        private void ViewDrive_Cancel(object? sender, EventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null && currentPage is DriverOverview)
            {
                DriverOverviewViewModel dVM = currentPage.DataContext as DriverOverviewViewModel;
                if (dVM != null)
                {
                    dVM.ViewDrive_Cancel(sender, e);
                }
            }
        }

        private void btnDrive_Click(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null && currentPage is DriverOverview)
            {
                DriverOverviewViewModel dVM = currentPage.DataContext as DriverOverviewViewModel;
                if (dVM != null)
                {
                    dVM.btnDrive_Click(sender, e, currentPage);
                }
            }
        }


        private void ViewDrive_Click(object sender, RoutedEventArgs e)
        {
            currentPage = MainNavigationFrame.Content as Page;
            if (currentPage != null && currentPage is DriverOverview)
            {
                DriverOverviewViewModel dVM = currentPage.DataContext as DriverOverviewViewModel;
                if (dVM != null)
                {
                    dVM.ViewDrive_Click(sender, e, currentPage);
                }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationFrame.NavigationService.Navigate(new DriverOverview(driver));
            lbl_Page.Text = "Home page";
        }
        private void SideMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            SideMenu.Visibility = Visibility.Collapsed;
        }

        private void btn_LogOff(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
}
