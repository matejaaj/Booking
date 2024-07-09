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
using BookingApp.Domain.Model;
using BookingApp.DTO;

namespace BookingApp.WPF.View.Tourist.UserControls
{
    /// <summary>
    /// Interaction logic for AllToursControl.xaml
    /// </summary>
    public partial class AllToursControl : UserControl
    {
        public static readonly DependencyProperty TouristProperty = DependencyProperty.Register(
            "Tourist", typeof(User), typeof(AllToursControl), new PropertyMetadata(null));

        public User Tourist
        {
            get { return (User)GetValue(TouristProperty); }
            set { SetValue(TouristProperty, value); }
        }


        public AllToursControl()
        {
            InitializeComponent();
        }

    }
}
