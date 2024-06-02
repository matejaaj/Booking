using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        public static MainWindowViewModel viewModel { get; set; }

        public OwnerMainWindow(Domain.Model.Owner owner)
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel(owner, MainFrame, this);
            DataContext = viewModel;
        }
    }
}
