using BookingApp.Domain.Model;
using System.ComponentModel;
using System;

namespace BookingApp.DTO
{
    public class ReservationDisplayDTO : INotifyPropertyChanged
    {
        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        private string requestStatusMessage;
        public string RequestStatusMessage
        {
            get { return requestStatusMessage; }
            set
            {
                if (requestStatusMessage != value)
                {
                    requestStatusMessage = value;
                    OnPropertyChanged(nameof(RequestStatusMessage));
                }
            }
        }

        private string ownerComment;
        public string OwnerComment
        {
            get { return ownerComment; }
            set
            {
                if (ownerComment != value)
                {
                    ownerComment = value;
                    OnPropertyChanged(nameof(OwnerComment));
                }
            }
        }

        private AccommodationReservation reservation;
        public AccommodationReservation Reservation
        {
            get { return reservation; }
            set
            {
                if (reservation != value)
                {
                    reservation = value;
                    OnPropertyChanged(nameof(Reservation));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReservationDisplayDTO(string accommodationName, DateTime startDate, DateTime endDate, AccommodationReservation reservation, string ownerComment, string requestStatusMessage)
        {
            AccommodationName = accommodationName;
            StartDate = startDate;
            EndDate = endDate;
            Reservation = reservation;
            OwnerComment = ownerComment;
            RequestStatusMessage = requestStatusMessage;
        }
    }
}
