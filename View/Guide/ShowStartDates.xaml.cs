using BookingApp.Model;
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

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for ShowStartDates.xaml
    /// </summary>
    public partial class ShowStartDates : Window
    {

        public List<string> StartDates { get; set; }
        public ShowStartDates(List<TourStartDate> startDates)
        {
            InitializeComponent();
            DataContext = this;
            StartDates = new List<string>();

            foreach(var startDate in startDates)
            {
                StartDates.Add(startDate.StartTime.ToString());
            }

            StartDatesListView.ItemsSource = StartDates;
        }
    }
}
