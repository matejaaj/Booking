using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookingApp.Application;
using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.View;
using BookingApp.Application.UseCases;
using BookingApp.WPF.View.Tourist;
using System.Collections.Generic;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class MyToursViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourInstanceViewModel> Tours { get; set; }
        public User Tourist { get; }

        private TourService _tourService;
        private TourInstanceService _tourInstanceService;
        private TourGuestService _tourGuestService;
        private TourReservationService _tourReservationService;
        private CheckpointService _checkPointService;
        private VoucherService _voucherService;
        private TourReviewService _tourReviewService;
        private ImageService _imageService;

        public ICommand MoreDetailsCommand { get; }
        public ICommand RateTourCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MyToursViewModel(User loggedUser, TourService tourService, TourInstanceService tourInstanceService, CheckpointService checkpointService, ImageService imageService, LocationService locationService, LanguageService languageService, TourGuestService tourGuestService, TourReservationService tourReservationService, VoucherService voucherService, TourReviewService tourReviewService)
        {
            _tourService = tourService;
            _tourInstanceService = tourInstanceService;
            _tourGuestService = tourGuestService;
            _tourReservationService = tourReservationService;
            _checkPointService = checkpointService;
            _voucherService = voucherService;
            _tourReviewService = tourReviewService;
            _imageService = imageService;

            MoreDetailsCommand = new RelayCommand(MoreDetailsExecute);
            RateTourCommand = new RelayCommand(RateTourExecute);

            Tourist = loggedUser;
            Tours = new ObservableCollection<TourInstanceViewModel>();

            CreateViewModels();


        }

        private void RateTourExecute(object parameter)
        {
            var tourInstanceViewModel = parameter as TourInstanceViewModel;
            if (tourInstanceViewModel != null)
            {
                if (!tourInstanceViewModel.IsFinished)
                {
                    MessageBox.Show(TranslationSource.Instance["TourNotFinishedMessage"]);
                    return;
                }

                if (CheckIfAlreadyReviewed(Tourist.Id, tourInstanceViewModel.Id))
                {
                    MessageBox.Show(TranslationSource.Instance["ReviewExistsMessage"]);
                    return;
                }

                var reviewWindow = new ReviewTourWindow(tourInstanceViewModel, Tourist.Id);
                reviewWindow.Show();
            }
        }

        private void MoreDetailsExecute(object parameter)
        {
            var tour = parameter as TourInstanceViewModel;
            if (tour != null)
            {
                var detailsWindow = new MyTourMoreDetailsWindow(tour);
                detailsWindow.Show();
            }
        }

        private void CreateViewModels()
        {
            foreach (var tourInstanceId in GetMyTourInstanceIds())
            {
                var tourInstance = _tourInstanceService.GetById(tourInstanceId);
                var tour = _tourService.GetById(tourInstance.TourId);
                var checkpoints = _checkPointService.GetAllByTourId(tour.Id);
                var tourGuests = _tourGuestService.GetAllByTouristForTourInstance(Tourist.Id, tourInstance.Id).ToList();
                var image = _imageService.GetImagesByEntityAndType(tour.Id, ImageResourceType.TOUR).First();
                var viewModel = new TourInstanceViewModel
                {
                    Id = tourInstanceId,
                    IsFinished = tourInstance.IsCompleted,
                    Guests = tourGuests,
                    Date = tourInstance.StartTime,
                    Name = tour.Name,
                    CheckpointNames = checkpoints.Select(cp => cp.Name).ToList(),
                    CurrentCheckpoint = tourInstance.CurrentCheckpoint,
                    ImagePath = image.Path,
                };

                Tours.Add(viewModel);
            }
        }

        private List<int> GetMyTourInstanceIds()
        {
            var tourReservations = _tourReservationService.GetAllByUserId(Tourist.Id);
            var tourInstanceIds = tourReservations.Select(reservation => reservation.TourInstanceId).ToList();
            return tourInstanceIds;
        }

        public bool CheckIfAlreadyReviewed(int userId, int tourId)
        {
            return _tourReviewService.HasUserReviewedTour(userId, tourId);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
