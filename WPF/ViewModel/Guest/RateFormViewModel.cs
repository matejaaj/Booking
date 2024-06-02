using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class RateFormViewModel : INotifyPropertyChanged
    {
        public List<Image> Images { get; set; }

        private AccommodationAndOwnerRatingService _ratingService;
        private AccommodationReservationService _reservationService;
        private readonly AccommodationReservation _reservation;
        public int AccommodationId { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RateFormViewModel(AccommodationReservation reservation)
        {
            Images = new List<Image>();
            _reservation = reservation;
            AccommodationId = reservation.AccommodationId;
            InitializeServices();
        }

        private void InitializeServices()
        {
            var _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _reservationService = new AccommodationReservationService(_accommodationService,Injector.CreateInstance<IAccommodationReservationRepository>());
            _ratingService = new AccommodationAndOwnerRatingService(_reservationService, Injector.CreateInstance<IAccommodationAndOwnerRatingRepository>());
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

        public void RateAccommodationAndOwner(int cleanliness, int ownersCorrectness, string comment, bool isRenovationRecommended, int renovationRecommendationId)
        {
            var newRating = new AccommodationAndOwnerRating(_reservation.Id, cleanliness, ownersCorrectness, comment, isRenovationRecommended, renovationRecommendationId);
            _ratingService.Save(newRating);
            _reservation.IsAccommodationAndOwnerRated = true;
            _reservationService.Update(_reservation);

            MessageBox.Show("Accommodation and owner successfully rated.");
        }
    }
}
