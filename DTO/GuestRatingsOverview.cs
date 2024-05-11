using System.ComponentModel;

namespace BookingApp.DTO
{
    public class GuestRatingsOverviewDTO : INotifyPropertyChanged
    {
        private int cleanliness;
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

        private int rulesRespect;
        public int RulesRespect
        {
            get { return rulesRespect; }
            set
            {
                if (rulesRespect != value)
                {
                    rulesRespect = value;
                    OnPropertyChanged("RulesRespect");
                }
            }
        }

        private string comment;
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

        private string accommodationName;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestRatingsOverviewDTO(int cleanliness, int rulesRespect, string comment, string accommodationName)
        {
            Cleanliness = cleanliness;
            RulesRespect = rulesRespect;
            Comment = comment;
            AccommodationName = accommodationName;
        }
    }
}
