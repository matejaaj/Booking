using BookingApp.LogicServices.Driver;
using BookingApp.WPF.ViewModel.Driver;
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
    /// Interaction logic for VacationRequest.xaml
    /// </summary>
    public partial class VacationRequest : Page
    {
        private VacationRequestViewModel VM;
        public VacationRequest(int driverId)
        {
            VM = ViewModelInjector.GetInstance(driverId, new VacationService(driverId, null));
            InitializeComponent();
            DataContext = VM;
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            VM.Button_Confirm(sender, e, this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
