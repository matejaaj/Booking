using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.DTO
{
    public class AccommodationPageDTO : INotifyPropertyChanged
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

        private string name { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string location { get; set; }
        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private List<string> images { get; set; }
        public List<string> Images
        {
            get { return images; }
            set
            {
                if (images != value)
                {
                    images = value;
                    OnPropertyChanged("Images");
                }
            }
        }

        private string thumbnail { get; set; }
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                if (thumbnail != value)
                {
                    thumbnail = value;
                    OnPropertyChanged("Thumbnail");
                }
            }
        }

        private string type { get; set; }
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private int maxGuests { get; set; }
        public int MaxGuests
        {
            get { return maxGuests; }
            set
            {
                if (maxGuests != value)
                {
                    maxGuests = value;
                    OnPropertyChanged("MaxGuests");
                }
            }
        }

        private int minReservations { get; set; }
        public int MinReservations
        {
            get { return minReservations; }
            set
            {
                if (minReservations != value)
                {
                    minReservations = value;
                    OnPropertyChanged("MinReservations");
                }
            }
        }

        private int cancelThershold { get; set; }
        public int CancelThershold
        {
            get { return cancelThershold; }
            set
            {
                if (cancelThershold != value)
                {
                    cancelThershold = value;
                    OnPropertyChanged("CancelThershold");
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

        public AccommodationPageDTO()
        {
            Images = new List<string>();
        }

        public AccommodationPageDTO(Accommodation a, Location location, List<string> images)
        {
            Id = a.AccommodationId;
            Name = a.Name;
            Location = location.ToString();
            Images = images;
            if(images.Count > 0)
            {
                Thumbnail = images[0];
            }
            else
            {
                Thumbnail = "../../../Resources/Images/hotelpark.jpg";
            }
            MaxGuests = a.MaxGuests;
            CancelThershold = a.CancelThershold;
            MinReservations = a.MinReservations;
            Type = a.Type.ToString();
            
        }
    }
}
