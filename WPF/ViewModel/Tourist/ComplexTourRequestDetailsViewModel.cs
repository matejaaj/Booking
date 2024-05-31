using BookingApp.DTO;
using BookingApp.Commands;
using System.ComponentModel;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ComplexTourRequestDetailsViewModel : INotifyPropertyChanged
    {
        private int _currentIndex;
        private ComplexTourRequestDTO _complexTourRequest;

        public ComplexTourRequestDTO ComplexTourRequest
        {
            get => _complexTourRequest;
            set
            {
                _complexTourRequest = value;
                OnPropertyChanged(nameof(ComplexTourRequest));
                OnPropertyChanged(nameof(CurrentTourRequest));
                OnPropertyChanged(nameof(CurrentIndexDisplay));
            }
        }

        public TourRequestDTO CurrentTourRequest => ComplexTourRequest.TourSegments[_currentIndex];

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }

        public string CurrentIndexDisplay => $"{_currentIndex + 1} / {ComplexTourRequest.TourSegments.Count}";

        public ComplexTourRequestDetailsViewModel()
        {
            NextCommand = new RelayCommand(_ => Next());
            PreviousCommand = new RelayCommand(_ => Previous());
        }

        private void Next()
        {
            _currentIndex = (_currentIndex + 1) % ComplexTourRequest.TourSegments.Count;
            OnPropertyChanged(nameof(CurrentTourRequest));
            OnPropertyChanged(nameof(CurrentIndexDisplay));
        }

        private void Previous()
        {
            _currentIndex = (_currentIndex - 1 + ComplexTourRequest.TourSegments.Count) % ComplexTourRequest.TourSegments.Count;
            OnPropertyChanged(nameof(CurrentTourRequest));
            OnPropertyChanged(nameof(CurrentIndexDisplay));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
