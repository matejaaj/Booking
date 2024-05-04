using BookingApp.WPF.ViewModel.Owner;
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

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for ViewRenovationsPage.xaml
    /// </summary>
    public partial class ViewRenovationsPage : Page
    {
        public static ViewRenovationsViewModel viewModel { get; set; }
        public ViewRenovationsPage(Domain.Model.Owner loggedInOwner)
        {
            InitializeComponent();
            viewModel = new ViewRenovationsViewModel(loggedInOwner);
            DataContext = viewModel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CancelButton_Click(sender, e);
        }
    }
}
