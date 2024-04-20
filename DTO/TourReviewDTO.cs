using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourReviewDTO : INotifyPropertyChanged
    {

        private readonly TourGuestService _tourGuestService;
        private readonly CheckpointService _checkpointService;

        private string _tourGuest;
        private int _id;
        private int _tourId;
        private int _touristId;
        private int _tourGuestId;
        private int _rating;
        private string _comment;
        private bool _isValid;
        private string _checkpoint;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Checkpoint
        {
            get { return _checkpoint; }
            set
            {
                if( _checkpoint != value)
                {
                    _checkpoint = value;
                    OnPropertyChanged(nameof(Checkpoint));
                }
            }
        }

        public int TourId
        {
            get { return _tourId; }
            set
            {
                if (_tourId != value)
                {
                    _tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

        public int TouristId
        {
            get { return _touristId; }
            set
            {
                if (_touristId != value)
                {
                    _touristId = value;
                    OnPropertyChanged(nameof(TouristId));
                }
            }
        }

        public int TourGuestId
        {
            get { return _tourGuestId; }
            set
            {
                if (_tourGuestId != value)
                {
                    _tourGuestId = value;
                    OnPropertyChanged(nameof(TourGuestId));
                }
            }
        }

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }
        public string TourGuest
        {
            get { return _tourGuest; }
            set
            {
                if(_tourGuest != value)
                {
                    _tourGuest = value;
                    OnPropertyChanged(nameof(_tourGuest));
                }
            }
        }



        public TourReviewDTO(TourReview review)
        {
            Id = review.Id;
            TourId = review.TourInstanceId;
            TouristId = review.TouristId;
            TourGuestId = review.TourGuestId;
            Rating = review.Rating;
            Comment = review.Comment;
            IsValid = review.IsValid;

            _tourGuestService = new TourGuestService();
            _checkpointService = new CheckpointService();

            TourGuest guest = _tourGuestService.GetById(TourGuestId);
            TourGuest = guest.Name;

            Checkpoint = _checkpointService.GetById(guest.CheckpointId).Name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public TourReview ToTourReview()
        {
            return new TourReview(Id, TouristId, TourGuestId, Rating, Comment, IsValid);
        }
    }
}
