using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ForumDisplayOwnerDTO : INotifyPropertyChanged
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

        private string dateOpened { get; set; }

        public string DateOpened
        {
            get { return dateOpened; }
            set
            {
                if (dateOpened != value)
                {
                    dateOpened = value;
                    OnPropertyChanged("DateOpened");
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

        private string display { get; set; }
        public string Display
        {
            get { return display; }
            set
            {
                if (display != value)
                {
                    display = value;
                    OnPropertyChanged("Display");
                }
            }
        }

        private string iconPath { get; set; }
        public string IconPath
        {
            get { return iconPath; }
            set
            {
                if (iconPath != value)
                {
                    iconPath = value;
                    OnPropertyChanged("IconPath");
                }
            }
        }

        private bool isUseful { get; set; }

        public bool IsUseful
        {
            get { return isUseful; }
            set
            {
                if (isUseful != value)
                {
                    isUseful = value;
                    OnPropertyChanged("IsUseful");
                }
            }
        }

        private bool isCancelled { get; set; }

        public bool IsCancelled
        {
            get { return isCancelled; }
            set
            {
                if (isCancelled != value)
                {
                    isCancelled = value;
                    OnPropertyChanged("IsCancelled");
                }
            }
        }

        public ForumDisplayOwnerDTO(User guest, Location location, Forum forum)
        {
            GuestName = guest.Username;
            Location = "Forum for: " + location.Country + ", " + location.City;
            DateOpened = forum.DateOpened.ToString("dd.MM.yyyy");
            Display = "Opened by " + guest.Username + " on " + DateOpened;
            Id = forum.Id;
            IsCancelled = forum.IsCancelled;
            IsUseful = forum.IsUsefull;
            if (IsUseful)
            {
                IconPath = "../../../Resources/Images/forum1.png";
            }
            else
            {
                IconPath = "../../../Resources/Images/forum.png";
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
    }
}
