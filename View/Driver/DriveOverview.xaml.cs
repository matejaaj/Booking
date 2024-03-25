using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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
using System.Windows.Threading;

namespace BookingApp.View.Driver
{
    /// <summary>
    /// Interaction logic for DriveOverview.xaml
    /// </summary>
    public partial class DriveOverview : Window
    {
        public Boolean enabled;

        public DriveReservation Reservation { get; set; }
        public DispatcherTimer Timer { get; set; }
        public DriveOverview()
        {
            InitializeComponent();
            StartingPrice = 250;
            Price = 0;
            enabled = true;
            Timer = new DispatcherTimer();
            Timer.Interval = System.TimeSpan.Parse("00:00:01");
            Timer.Tick += Timer_Tick;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _startingPrice;
        public int StartingPrice
        {
            get => _startingPrice;
            set
            {
                if (value != _startingPrice)
                {
                    _startingPrice = value;
                    OnPropertyChanged(nameof(StartingPrice));
                }
            }
        }

        private int _price;
        public int Price
        {
            get => _price;
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnStart(object sender, RoutedEventArgs e)
        {
            if (StartingPrice != null && StartingPrice > 0 && enabled)
            {
                Price = StartingPrice;
                enabled = false;
                Timer.Start();
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {

        }

        private void btnFinish(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Price += 5;
            price.Content = Price.ToString();
        }
    }
}
