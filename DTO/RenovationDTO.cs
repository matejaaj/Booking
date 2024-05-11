using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class RenovationDTO : INotifyPropertyChanged
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
        private DateTime startDate { get; set; }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime endDate { get; set; }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }
        private string accommodationNameAndLocation { get; set; }
        public string AccommodationNameAndLocation
        {
            get { return accommodationNameAndLocation; }
            set
            {
                if (accommodationNameAndLocation != value)
                {
                    accommodationNameAndLocation = value;
                    OnPropertyChanged("AccommodationNameAndLocation");
                }
            }
        }

        private bool isPast { get; set; }
        public bool IsPast
        {
            get { return isPast; }
            set
            {
                if (isPast != value)
                {
                    isPast = value;
                    OnPropertyChanged("IsPast");
                }
            }
        }

        private string timeSpan { get; set; }
        public string TimeSpan
        {
            get { return timeSpan; }
            set
            {
                if (timeSpan != value)
                {
                    timeSpan = value;
                    OnPropertyChanged("TimeSpan");
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

        public RenovationDTO(int id,DateTime startDate, DateTime endDate, string accommodationNameAndLocation)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            AccommodationNameAndLocation = accommodationNameAndLocation;
            if(endDate < DateTime.Today)
            {
                IsPast = true;
            }
            else
            {
                IsPast = false;
            }
            TimeSpan = startDate.ToString("dd.MM.yyyy") + "   -   " + endDate.ToString("dd.MM.yyyy");
        }
    }
}
