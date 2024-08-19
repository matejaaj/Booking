using BookingApp.WPF.ViewModel.Tourist;
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

namespace BookingApp.WPF.View.Tourist.UserControls
{
    /// <summary>
    /// Interaction logic for FastDriveControl.xaml
    /// </summary>
    public partial class FastDriveControl : UserControl
    {
        public FastDriveControl()
        {
            InitializeComponent();
        }

        private void AutoCompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;
            if (autoCompleteBox != null && autoCompleteBox.SelectedItem == null)
            {
                var viewModel = DataContext as FastDriveFormViewModel;
                if (viewModel != null)
                {
                    if (autoCompleteBox.Name == "autoCompleteCountry")
                    {
                        viewModel.ValidateCountry(autoCompleteBox.Text);
                    }
                    else if (autoCompleteBox.Name == "autoCompleteCity")
                    {
                        viewModel.ValidateCity(autoCompleteBox.Text);
                    }
                    else if (autoCompleteBox.Name == "autoCompleteHour")
                    {
                        viewModel.ValidateHour(autoCompleteBox.Text);
                    }
                    else if (autoCompleteBox.Name == "autoCompleteMinute")
                    {
                        viewModel.ValidateMinute(autoCompleteBox.Text);
                    }
                }
            }
        }
    }
}
