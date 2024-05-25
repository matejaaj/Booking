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
            }
        }

        public TourRequestDTO CurrentTourRequest => ComplexTourRequest.TourSegments[_currentIndex];

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }

        public ComplexTourRequestDetailsViewModel()
        {
            NextCommand = new RelayCommand(_ => Next(), _ => CanGoNext());
            PreviousCommand = new RelayCommand(_ => Previous(), _ => CanGoPrevious());
        }

        private void Next()
        {
            if (CanGoNext())
            {
                _currentIndex++;
                OnPropertyChanged(nameof(CurrentTourRequest));
            }
        }

        private bool CanGoNext() => _currentIndex < ComplexTourRequest.TourSegments.Count - 1;

        private void Previous()
        {
            if (CanGoPrevious())
            {
                _currentIndex--;
                OnPropertyChanged(nameof(CurrentTourRequest));
            }
        }

        private bool CanGoPrevious() => _currentIndex > 0;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}