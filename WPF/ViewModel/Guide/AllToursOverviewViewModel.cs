using BookingApp.Application.UseCases;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BookingApp.Domain.Model;
using System.ComponentModel;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class AllToursOverviewViewModel : INotifyPropertyChanged
    {

        private List<Tour> _tours;
        public ObservableCollection<TourDTO> AllTours { get; set; }
        public ObservableCollection<Checkpoint> SelectedTourCheckpoints { get; set; }

        private readonly TourService _tourService;
        private readonly CheckpointService _checkpointService;

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
            _tourService = new TourService();
            _checkpointService = new CheckpointService();

            AllTours = new ObservableCollection<TourDTO>();
            SelectedTourCheckpoints = new ObservableCollection<Checkpoint>();

            LoadTours();
        }

        private void LoadTours()
        {
            _tours = _tourService.GetAll();

            foreach(var tour in _tours)
            {
                TourDTO tourDTO = new TourDTO(tour);
                AllTours.Add(tourDTO);
            }
        }
    }
}
