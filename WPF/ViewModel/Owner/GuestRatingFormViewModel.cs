using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class GuestRatingFormViewModel : INotifyPropertyChanged
    {
        private AccommodationReservation _reservation;
        private static AccommodationReservationService _accommodationReservationService;
        private static GuestRatingService _guestRatingService;

        private int _cleanliness;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _rulesRespect;
        public int RulesRespect
        {
            get => _rulesRespect;
            set
            {
                if (value != _rulesRespect)
                {
                    _rulesRespect = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand Confirm { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestRatingFormViewModel(AccommodationReservation reservation)
        {
            _accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            _guestRatingService = new GuestRatingService(Injector.CreateInstance<IGuestRatingRepository>());
            _reservation = reservation;
            Confirm = new RelayCommand(ConfirmClick);
        }

        private void ConfirmClick(object obj)
        {
            GuestRating newGuestRating = new GuestRating(_reservation.Id, Cleanliness, RulesRespect, Comment);
            _guestRatingService.Save(newGuestRating);
            _reservation.IsRated = true;
            _accommodationReservationService.Update(_reservation);
        }
    }
}
