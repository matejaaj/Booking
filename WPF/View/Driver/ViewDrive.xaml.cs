using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.Driver;
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

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for ViewDrive.xaml
    /// </summary>
    public partial class ViewDrive : Page
    {
        public ViewDriveViewModel VM { get; set; }

        public ViewDrive()
        {
            VM = new ViewDriveViewModel();
            InitializeComponent();
            DataContext = VM;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            VM.Confirm_Click(sender, e);
            
            NavigationService.GoBack();
        }

        private void rbAtLocation_Checked(object sender, RoutedEventArgs e)
        {
            VM.rbAtLocation_Checked(sender, e);
        }
    }
}
