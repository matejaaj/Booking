using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestRatingFormViewModel(AccommodationReservation reservation)
        {
            _accommodationReservationService = new AccommodationReservationService();
            _guestRatingService = new GuestRatingService();
            _reservation = reservation;
        }

        public void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                GuestRating newGuestRating = new GuestRating(_reservation.ReservationId, Cleanliness, RulesRespect, Comment);
                _guestRatingService.Save(newGuestRating);
                _reservation.IsRated = true;
                _accommodationReservationService.Update(_reservation);
            }
        }

        private bool IsValid()
        {
            if (Cleanliness < 1 || Cleanliness > 5)
            {
                MessageBox.Show("Please enter a cleanliness rating between 1 and 5.");
                return false;
            }

            if (RulesRespect < 1 || RulesRespect > 5)
            {
                MessageBox.Show("Please enter a rules respect rating between 1 and 5.");
                return false;
            }

            return true;
        }
    }
}
