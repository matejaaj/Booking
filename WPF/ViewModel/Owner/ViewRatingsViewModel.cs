using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class ViewRatingsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AccommodationRatingDTO> _ratings;
        public ObservableCollection<AccommodationRatingDTO> Ratings
        {
            get { return _ratings; }
            set
            {
                _ratings = value;
                OnPropertyChanged(nameof(Ratings));
            }
        }
        public List<AccommodationReservation> AccommodationReservations { get; set; }
        public Domain.Model.Owner LoggedInOwner { get; set; }
        private static AccommodationReservationService _accommodationReservationService;
        private static UserService _userService;
        private static AccommodationAndOwnerRatingService _accommodationAndOwnerRatingService;
        private static AccommodationService _accommodationService;

        public ViewRatingsViewModel(Domain.Model.Owner loggedInOwner, List<AccommodationReservation> ownerAccommodationReservations) 
        { 
            LoggedInOwner = loggedInOwner;
            InitializeServices();
            Ratings = new ObservableCollection<AccommodationRatingDTO>();
            AccommodationReservations = ownerAccommodationReservations;
            Update();
        }

        private void InitializeServices()
        {
            _accommodationReservationService = new AccommodationReservationService();
            _userService = new UserService();
            _accommodationAndOwnerRatingService = new AccommodationAndOwnerRatingService();
            _accommodationService = new AccommodationService(); 
        }

        private void Update()
        {
            Ratings.Clear();
            
            foreach (var a in AccommodationReservations)
            {
                if (a.IsRated && a.IsAccommodationAndOwnerRated)
                {
                    var guest = _userService.GetById(a.GuestId);
                    var rating = _accommodationAndOwnerRatingService.GetByReservationId(a.ReservationId);
                    var accommodation = _accommodationService.GetById(a.AccommodationId);
                    var RatingDTO = new AccommodationRatingDTO(LoggedInOwner, guest, rating, accommodation);
                    Ratings.Add(RatingDTO);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
