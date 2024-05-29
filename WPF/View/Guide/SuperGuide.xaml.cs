using BookingApp.Domain.Model;
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

namespace BookingApp.WPF.View.Guide
{
    public partial class SuperGuide : Page
    {
        private SuperGuideViewModel _viewModel { get; set; }

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
           "PageTitle", typeof(string), typeof(SuperGuide), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty PageIconProperty = DependencyProperty.Register(
            "PageIcon", typeof(string), typeof(SuperGuide), new PropertyMetadata(default(string)));

        public string PageIcon
        {
            get { return (string)GetValue(PageIconProperty); }
            set { SetValue(PageIconProperty, value); }
        }

        public SuperGuide(User user)
        {
            InitializeComponent();
            _viewModel = new SuperGuideViewModel(user);
            DataContext = _viewModel;
            this.PageTitle = "SUPER GUIDE";
            this.PageIcon = "../../../Resources/Images/Guide/badge.png";
        }
        private void btn_ShowProgress(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowProgress();
        }
    }

}
