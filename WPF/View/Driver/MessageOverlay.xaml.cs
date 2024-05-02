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

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for MessageOverlay.xaml
    /// </summary>
    public partial class MessageOverlay : UserControl
    {
        public MessageOverlay()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        public void Show(string message)
        {
            MessageText.Text = message;
            this.Visibility = Visibility.Visible;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
