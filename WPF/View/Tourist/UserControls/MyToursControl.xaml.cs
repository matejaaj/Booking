using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
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
    /// Interaction logic for MyToursControl.xaml
    /// </summary>
    public partial class MyToursControl : UserControl
    {

        public static readonly DependencyProperty TouristProperty = DependencyProperty.Register(
            "Tourist", typeof(User), typeof(MyToursControl), new PropertyMetadata(null));

        public User Tourist
        {
            get { return (User)GetValue(TouristProperty); }
            set { SetValue(TouristProperty, value); }
        }

        public MyToursControl()
        {
            InitializeComponent();
        }


    }
}
