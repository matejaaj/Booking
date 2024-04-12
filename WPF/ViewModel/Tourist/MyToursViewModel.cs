using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class MyToursViewModel
    {
        public ObservableCollection<TourInstanceViewModel> Tours { get; set; }
        private User _tourist;

        private TourService _tourService;
        private TourInstanceService _tourInstanceService;
        private TourGuestService _tourGuestService;
        private TourReservationService _tourReservationService;
        private CheckpointService _checkPointService;

        public MyToursViewModel(User loggedUser)
        {
            _tourist = loggedUser;
            Tours = new ObservableCollection<TourInstanceViewModel>();
            InitializeServices();
            CreateViewModels();
        }

        private void CreateViewModels()
        {
            var tourReservations = _tourReservationService.GetAllByUserId(_tourist.Id);
            var tourInstanceIds = tourReservations.Select(reservation => reservation.TourInstanceId).ToList();
            var tourInstanceViewModels = new ObservableCollection<TourInstanceViewModel>();
            foreach (var tourInstanceId in tourInstanceIds)
            {
                var tourInstance = _tourInstanceService.GetById(tourInstanceId);
                var tour = _tourService.GetById(tourInstance.TourId);
                var checkpoints = _checkPointService.GetAllByTourId(tour.Id);
                var tourGuests = _tourGuestService.GetAllByTouristForTourInstance(_tourist.Id, tourInstance.Id).ToList();

                var viewModel = new TourInstanceViewModel
                { 
                    Guests = tourGuests,
                    Date = tourInstance.StartTime,
                    Name = tour.Name,
                    CheckpointNames = checkpoints.Select(cp => cp.Name).ToList(),
                    CurrentCheckpoint = tourInstance.CurrentCheckpoint,
                };

                Tours.Add(viewModel);
            }
        }

        private void InitializeServices()
        {
            _tourService = new TourService();
            _tourInstanceService = new TourInstanceService();
            _tourGuestService = new TourGuestService();
            _checkPointService = new CheckpointService();
            _tourReservationService = new TourReservationService();
        }
    }
}
