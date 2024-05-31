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
    /// Interaction logic for ViewForumPage.xaml
    /// </summary>
    public partial class ViewForumPage : Page
    {
        private ViewForumViewModel viewModel;
        public ViewForumPage(Domain.Model.Owner _loggedInOwner, DTO.ForumDisplayOwnerDTO forum)
        {
            InitializeComponent();
            viewModel = new ViewForumViewModel(_loggedInOwner, forum, this);
            DataContext = viewModel;
        }
    }
}
