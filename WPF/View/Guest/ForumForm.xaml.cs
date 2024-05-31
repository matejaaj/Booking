using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.ViewModel.Guest;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest
{
    public partial class ForumForm : Page
    {
        private readonly ForumFormViewModel _viewModel;

        public ForumForm(User loggedInGuest)
        {
            InitializeComponent();
            _viewModel = new ForumFormViewModel(loggedInGuest);
            DataContext = _viewModel;
        }

        private void CreateForumButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenCreateForumWindow();
        }
    }
}
