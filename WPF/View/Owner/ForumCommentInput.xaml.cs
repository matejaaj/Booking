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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for ForumCommentInput.xaml
    /// </summary>
    public partial class ForumCommentInput : Window
    {
        private ForumCommentInputViewModel viewModel;
        public ForumCommentInput(Domain.Model.Owner _loggedInOwner, DTO.ForumDisplayOwnerDTO _forum)
        {
            InitializeComponent();
            viewModel = new ForumCommentInputViewModel(_loggedInOwner, _forum);
            DataContext = viewModel;

            viewModel.RequestClose += (s, e) => this.Close();
        }
    }
}
