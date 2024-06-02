using BookingApp.Domain.Model;
using BookingApp.DTO;
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
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        public NotificationControl()
        {
            InitializeComponent();

        }


        private void ButtonDeleteNotification_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is NotificationDTO dto)
            {
                var viewModel = DataContext as TouristTabsViewModel;
                if (viewModel != null)
                {
                    viewModel.DeleteNotification(dto.Id);
                }
            }
        }
    }
}
