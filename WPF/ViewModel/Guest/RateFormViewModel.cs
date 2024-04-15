using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class RateFormViewModel : INotifyPropertyChanged
    {
        public List<Image> Images { get; set; }

        private readonly AccommodationAndOwnerRatingService _ratingService;
        private readonly AccommodationReservationService _reservationService;
        private readonly AccommodationReservation _reservation;
        public int AccommodationId { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RateFormViewModel(AccommodationReservation reservation, AccommodationAndOwnerRatingService ratingService, AccommodationReservationService reservationService)
        {
            Images = new List<Image>();
            _reservation = reservation;
            AccommodationId = reservation.AccommodationId;
            _ratingService = ratingService;
            _reservationService = reservationService;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddImage(Image image)
        {
            Images.Add(image);
            OnPropertyChanged(nameof(Images));
        }

        public void RateAccommodationAndOwner(int cleanliness, int ownersCorrectness, string comment)
        {

                var newRating = new AccommodationAndOwnerRating(_reservation.Id, cleanliness, ownersCorrectness, comment);
                _ratingService.Save(newRating);
                _reservation.IsAccommodationAndOwnerRated = true;
                _reservationService.Update(_reservation);

            MessageBox.Show("Accommodation and owner successfully rated.");
        }
    }
}