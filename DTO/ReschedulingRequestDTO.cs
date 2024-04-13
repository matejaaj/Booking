using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ReschedulingRequestDTO : INotifyPropertyChanged
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

        private string originalPeriod { get; set; }
        public string OriginalPeriod
        {
            get { return originalPeriod; }
            set
            {
                if (originalPeriod != value)
                {
                    originalPeriod = value;
                    OnPropertyChanged("OriginalPeriod");
                }
            }
        }

        private string newPeriod { get; set; }
        public string NewPeriod
        {
            get { return newPeriod; }
            set
            {
                if (newPeriod != value)
                {
                    newPeriod = value;
                    OnPropertyChanged("NewPeriod");
                }
            }
        }

        private string status { get; set; }
        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private bool isAvailable { get; set; }
        public bool IsAvailable
        {
            get { return isAvailable; }
            set
            {
                if (isAvailable != value)
                {
                    isAvailable = value;
                    OnPropertyChanged("IsAvailable");
                }
            }
        }

        private DateTime newStartDate { get; set; }
        public DateTime NewStartDate
        {
            get { return newStartDate; }
            set
            {
                if (newStartDate != value)
                {
                    newStartDate = value;
                    OnPropertyChanged("NewStartDate");
                }
            }
        }

        private DateTime newEndDate { get; set; }
        public DateTime NewEndDate
        {
            get { return newEndDate; }
            set
            {
                if (newEndDate != value)
                {
                    newEndDate = value;
                    OnPropertyChanged("NewEndDate");
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

        public ReschedulingRequestDTO(ReservationModificationRequest request, User guest, Accommodation accommodation, bool isAvailable)
        {
            Id = request.Id;
            GuestName = guest.Username;
            AccommodationName = accommodation.Name;
            OriginalPeriod = request.OldStartDate.ToShortDateString() + "-" + request.OldEndDate.ToShortDateString();
            NewPeriod = request.NewStartDate.ToShortDateString() + "-" + request.NewEndDate.ToShortDateString();
            NewStartDate = request.NewStartDate; 
            NewEndDate = request.NewEndDate;
            IsAvailable = isAvailable;
            Status = request.Status.ToString();
        }
    }
}
