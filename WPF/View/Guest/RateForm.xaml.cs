using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.View.Guide;
using BookingApp.WPF.ViewModel.Guest;
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

namespace BookingApp.WPF.View.Guest
{
    public partial class RateForm : Window
    {
        private RateFormViewModel _viewModel;

        public RateForm(AccommodationReservation reservation)
        {
            InitializeComponent();
            _viewModel = new RateFormViewModel(reservation, new AccommodationAndOwnerRatingService(Injector.CreateInstance<IAccommodationAndOwnerRatingRepository>()), new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>()));
            DataContext = _viewModel;
        }

        private void AddPictures_Click(object sender, RoutedEventArgs e)
        {
            AddImage addImageWindow = new AddImage(_viewModel.Images, _viewModel.AccommodationId, ImageResourceType.ACCOMMODATION);
            addImageWindow.Owner = this;
            addImageWindow.ShowDialog();
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            int cleanliness = (int)CleanlinessSlider.Value;
            int ownersCorrectness = (int)OwnersCorrectnessSlider.Value;
            string comment = CommentsTextBox.Text;

            _viewModel.RateAccommodationAndOwner(cleanliness, ownersCorrectness, comment);
            Close();
        }
    }
}