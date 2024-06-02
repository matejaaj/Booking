using BookingApp.Commands;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using BookingApp.Domain.Model;
using BookingApp.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourDetailsViewModel : INotifyPropertyChanged
    {
        private TourDTO _tour;
        private int _currentImageIndex;
        private User _user;

        public TourDTO Tour
        {
            get { return _tour; }
            set
            {
                if (_tour != value)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(Tour));
                }
            }
        }

        public string CurrentImage => Tour.Images != null && Tour.Images.Any() ? Tour.Images[_currentImageIndex] : null;

        public ICommand NextImageCommand { get; }
        public ICommand PreviousImageCommand { get; }
        public ICommand ReservationCommand { get; }

        public TourDetailsViewModel(TourDTO tour, User user)
        {
            _tour = tour;
            _currentImageIndex = 0;
            _user = user;

            NextImageCommand = new RelayCommand(NextImage, CanExecuteImageChange);
            PreviousImageCommand = new RelayCommand(PreviousImage, CanExecuteImageChange);
            ReservationCommand = new RelayCommand(ReserveTour);
        }

        private bool CanExecuteImageChange(object parameter)
        {
            return Tour.Images != null && Tour.Images.Count > 1;
        }

        private void NextImage(object parameter)
        {
            _currentImageIndex = (_currentImageIndex + 1) % Tour.Images.Count;
            OnPropertyChanged(nameof(CurrentImage));
        }

        private void PreviousImage(object parameter)
        {
            _currentImageIndex = (_currentImageIndex - 1 + Tour.Images.Count) % Tour.Images.Count;
            OnPropertyChanged(nameof(CurrentImage));
        }

        private void ReserveTour(object parameter)
        {
            var reservationWindow = new TourReservationForm(Tour, _user);
            reservationWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}