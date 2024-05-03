using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class BookingDTO : INotifyPropertyChanged
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

        private string startDate { get; set; }

        public string StartDate
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

        private string endDate { get; set; }

        public string EndDate
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

        private string isRated { get; set; }

        public string IsRated
        {
            get { return isRated; }
            set
            {
                if (isRated != value)
                {
                    isRated = value;
                    OnPropertyChanged("IsRated");
                }
            }
        }

        private int days { get; set; }

        public int Days
        {
            get { return days; }
            set
            {
                if (days != value)
                {
                    days = value;
                    OnPropertyChanged("Days");
                }
            }
        }

        private int guests { get; set; }

        public int Guests
        {
            get { return guests; }
            set
            {
                if (guests != value)
                {
                    guests = value;
                    OnPropertyChanged("Guests");
                }
            }
        }

        public BookingDTO(AccommodationReservation reservation)
        {
            Id = reservation.Id;
            StartDate = reservation.StartDate.ToString("dd.MM.yyyy");
            EndDate = reservation.EndDate.ToString("dd.MM.yyyy");
            Guests = reservation.GuestNumber;
            Days = reservation.Days;
            if(reservation.IsRated)
            {
                IsRated = "Rated";
            }
            else
            {
                IsRated = "Not Rated";
            }
        }

        public override string ToString()
        {
            return $"{StartDate}-{EndDate}, Days: {Days}, Guests: {Guests}, {IsRated}";
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
