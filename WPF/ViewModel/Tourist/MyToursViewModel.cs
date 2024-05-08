using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model.BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class MyToursViewModel
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

        public MyToursViewModel(User loggedUser, TourService tourService, TourInstanceService tourInstanceService, CheckpointService checkpointService, ImageService imageService, LocationService locationService, LanguageService languageService, TourGuestService tourGuestService, TourReservationService tourReservationService, VoucherService voucherService, TourReviewService tourReviewService)
        {
            _tourService = tourService;
            _tourInstanceService = tourInstanceService;
            _tourGuestService = tourGuestService;
            _tourReservationService = tourReservationService;
            _checkPointService = checkpointService;
            _voucherService = voucherService;
            _tourReviewService = tourReviewService;


            Tourist = loggedUser;
            Tours = new ObservableCollection<TourInstanceViewModel>();

            CreateViewModels();
        }
        private void CreateViewModels()
        {
            foreach (var tourInstanceId in GetMyTourInstanceIds())
            {
                var tourInstance = _tourInstanceService.GetById(tourInstanceId);
                var tour = _tourService.GetById(tourInstance.TourId);
                var checkpoints = _checkPointService.GetAllByTourId(tour.Id);
                var tourGuests = _tourGuestService.GetAllByTouristForTourInstance(Tourist.Id, tourInstance.Id).ToList();

                var viewModel = new TourInstanceViewModel
                { 
                    Id = tourInstanceId,
                    IsFinished = tourInstance.IsCompleted,
                    Guests = tourGuests,
                    Date = tourInstance.StartTime,
                    Name = tour.Name,
                    CheckpointNames = checkpoints.Select(cp => cp.Name).ToList(),
                    CurrentCheckpoint = tourInstance.CurrentCheckpoint,
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

    }
}
