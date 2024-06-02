using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.WPF.ViewModel.Guest;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BookingApp.WPF.View.Guest
{
    public partial class CreateForum : Window
    {
        private readonly CreateForumViewModel _viewModel;

        public CreateForum()
        {
            InitializeComponent();
            _viewModel = new CreateForumViewModel();
            DataContext = _viewModel;
            LoadLocations();
        }

        private void LoadLocations()
        {
            LocationComboBox.ItemsSource = _viewModel.LoadLocations();
            LocationComboBox.DisplayMemberPath = "CityCountry";
            LocationComboBox.SelectedValuePath = "Id";
        }

        private void CreateForumButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationComboBox.SelectedItem is LocationDTO selectedLocation)
            {
                _viewModel.CreateForum(selectedLocation, CommentTextBox.Text);
                Close();
            }
        }
    }
}
