using BookingApp.WPF.ViewModel.Guide;
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
using BookingApp.Domain.Model;

namespace BookingApp.WPF.View.Guide
{
    public partial class Home : Page
    {
        private readonly HomeViewModel _viewModel;

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
           "PageTitle", typeof(string), typeof(Home), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof(string), typeof(Home), new PropertyMetadata(default(string)));

        public string PageIcon
        {
            get { return (string)GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
        }

        public Home(User user, GuideMainWindow mainwindow)
        {
            InitializeComponent();
            _viewModel = new HomeViewModel(user, mainwindow);
            DataContext = this;
            this.PageTitle = "HOME";
            this.PageIcon = "../../../Resources/Images/Guide/house.png";
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SignOut();
        }
        private void btnQuitJob_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.QuitJob();
        }
    }
}
