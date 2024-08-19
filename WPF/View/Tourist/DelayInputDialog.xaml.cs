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

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for DelayInputDialog.xaml
    /// </summary>
    public partial class DelayInputDialog : Window
    {
        public int? DelayMinutes { get; private set; }

        public DelayInputDialog()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DelayTextBox.Text, out int delay))
            {
                DelayMinutes = delay;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show(TranslationSource.Instance["ValidationNumberOfPeople"]);

            }
        }
    }
}
