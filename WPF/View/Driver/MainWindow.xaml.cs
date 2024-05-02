using BookingApp.Application;
using BookingApp.Application.Events;
using BookingApp.Domain.Model;
using BookingApp.WPF.View.Driver;
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

        public MainWindow(User user)
        {
            InitializeComponent();
            MainNavigationFrame.Navigate(new DriverOverview(user));
            EventAggregator.Subscribe<ShowMessageEvent>(e => MessageOverlay.Show(e.Message));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ShowOverlay(String message)
        {
            MessageOverlay.Show(message);
        }
    }
}
