using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization;
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
        public delegate void DialogResultHandler(bool result);
        public event DialogResultHandler DialogResultReceived;

        public MessageOverlay()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        public void Show(string message, string title)
        {
            MessageText.Text = message;
            TxtTitle.Text = title;
            BtnOk.Visibility = Visibility.Visible;
            BtnConfirm.Visibility = Visibility.Collapsed;
            BtnCancel.Visibility = Visibility.Collapsed;
            this.Visibility = Visibility.Visible;
        }

        public void ShowDialog(string message, string title) 
        {
            MessageText.Text = message;
            TxtTitle.Text = title;
            BtnOk.Visibility = Visibility.Collapsed;
            BtnCancel.Visibility = Visibility.Visible;
            BtnConfirm.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Visible;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DialogResultReceived?.Invoke(true);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DialogResultReceived?.Invoke(false);
        }
    }
}
