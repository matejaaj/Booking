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

namespace BookingApp.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideOverview.xaml
    /// </summary>
    public partial class GuideOverview : Window
    {
        public GuideOverview()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void btnShowTourForm_Click(object sender, RoutedEventArgs e)
        {
            TourForm tourForm = new TourForm();
            tourForm.ShowDialog();
        }

        private void btnShowTodayTours_Click(object sender, RoutedEventArgs e)
        {
            TodayToursOverview todayToursOverview = new TodayToursOverview();
            todayToursOverview.ShowDialog();
        }
    }
 
}
