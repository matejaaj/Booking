using BookingApp.Application.UseCases;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BookingApp.Domain.Model;
using System.ComponentModel;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class AllToursOverviewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourDTO> AllTours { get; set; }
        public ObservableCollection<Checkpoint> SelectedTourCheckpoints { get; set; }

        private  TourService _tourService;
        private  CheckpointService _checkpointService;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private TourDTO _selectedTour;
        public TourDTO SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                    LoadSelectedTourCheckpoints();
                }
            }
        }

        private void LoadSelectedTourCheckpoints()
        {
            if (SelectedTour != null)
            {
                SelectedTourCheckpoints.Clear();
                var checkpoints = _checkpointService.GetAllByTourId(SelectedTour.Id);
                foreach (var checkpoint in checkpoints)
                {
                    SelectedTourCheckpoints.Add(checkpoint);
                }
            }
        }

        public AllToursOverviewViewModel() 
        {
            InitializeServices();

            AllTours = new ObservableCollection<TourDTO>();
            SelectedTourCheckpoints = new ObservableCollection<Checkpoint>();

            LoadTours();
        }

        private void LoadTours()
        {
            var tours = _tourService.GetAll();

            foreach(var tour in tours)
            {
                TourDTO tourDTO = new TourDTO(tour);
                AllTours.Add(tourDTO);
            }
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>(), _tourGuestService, _voucherService);
            var _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _checkpointService = new CheckpointService(Injector.CreateInstance<ICheckpointRepository>(), _tourInstanceService);
            _tourService = new TourService(Injector.CreateInstance<ITourRepository>(), _tourGuestService, _tourInstanceService);
        }
    }
}
