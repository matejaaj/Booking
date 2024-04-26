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
        private TourReviewService _tourReveiewService;

        public MyToursViewModel(User loggedUser)
        {
            Tourist = loggedUser;
            Tours = new ObservableCollection<TourInstanceViewModel>();
            InitializeServices();
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
            return _tourReveiewService.HasUserReviewedTour(userId, tourId);
        }

        private void InitializeServices()
        {
            _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>(), _tourGuestService, _voucherService);
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _checkPointService = new CheckpointService(Injector.CreateInstance<ICheckpointRepository>(), _tourInstanceService);
            _tourService = new TourService(Injector.CreateInstance<ITourRepository>(), _tourGuestService, _tourInstanceService);
            _tourReveiewService = new TourReviewService(Injector.CreateInstance<ITourReviewRepository>());
        }
    }
}
