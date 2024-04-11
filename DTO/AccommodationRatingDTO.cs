using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationRatingDTO : INotifyPropertyChanged
    {
        private int id { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private int cleanliness { get; set; }

        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                if (cleanliness != value)
                {
                    cleanliness = value;
                    OnPropertyChanged("Cleanliness");
                }
            }
        }

        private int ownerEthics { get; set; }

        public int OwnerEthics
        {
            get { return ownerEthics; }
            set
            {
                if (ownerEthics != value)
                {
                    ownerEthics = value;
                    OnPropertyChanged("OwnerEthics");
                }
            }
        }

        private string comment { get; set; }
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private string guestName { get; set; }
        public string GuestName
        {
            get { return guestName; }
            set
            {
                if (guestName != value)
                {
                    guestName = value;
                    OnPropertyChanged("GuestName");
                }
            }
        }

        private string accommodationName { get; set; }
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public AccommodationRatingDTO(User loggedInOwner, User guest, AccommodationAndOwnerRating rating, Accommodation accommodation)
        {
            Cleanliness = rating.Cleanliness;
            OwnerEthics = rating.OwnershipEthics;
            Comment = rating.Comment;
            GuestName = guest.Username;
            AccommodationName = accommodation.Name;
        }
    }
}
