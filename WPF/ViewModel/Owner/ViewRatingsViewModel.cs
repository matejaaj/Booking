using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
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

        public ViewRatingsViewModel(Domain.Model.Owner loggedInOwner) 
        { 
            LoggedInOwner = loggedInOwner;
            InitializeServices();
            Ratings = new ObservableCollection<AccommodationRatingDTO>();
            AccommodationReservations = _accommodationReservationService.GetByOwner(loggedInOwner);
           // AccommodationReservations = ownerAccommodationReservations;
            Update();
        }

        private void InitializeServices()
        {
            _userService = new UserService(Injector.CreateInstance<IUserRepository>());
            _accommodationAndOwnerRatingService = new AccommodationAndOwnerRatingService(Injector.CreateInstance<IAccommodationAndOwnerRatingRepository>());
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _accommodationReservationService = new AccommodationReservationService(_accommodationService, Injector.CreateInstance<IAccommodationReservationRepository>());
        }

        private void Update()
        {
            Ratings.Clear();
            
            foreach (var a in AccommodationReservations)
            {
                if (a.IsRated && a.IsAccommodationAndOwnerRated)
                {
                    var guest = _userService.GetById(a.GuestId);
                    var rating = _accommodationAndOwnerRatingService.GetByReservationId(a.Id);
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
