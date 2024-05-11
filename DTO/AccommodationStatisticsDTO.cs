using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class AccommodationStatisticsDTO : INotifyPropertyChanged
    {

        private int year { get; set; }

        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        private int bookings { get; set; }
        public int Bookings
        {
            get { return bookings; }
            set
            {
                if (bookings != value)
                {
                    bookings = value;
                    OnPropertyChanged("Bookings");
                }
            }
        }

        private int cancelations { get; set; }
        public int Cancelations
        {
            get { return cancelations; }
            set
            {
                if (cancelations != value)
                {
                    cancelations = value;
                    OnPropertyChanged("Cancelations");
                }
            }
        }

        private int reschedulings { get; set; }
        public int Reschedulings
        {
            get { return reschedulings; }
            set
            {
                if (reschedulings != value)
                {
                    reschedulings = value;
                    OnPropertyChanged("Reschedulings");
                }
            }
        }

        private int renovationSuggestions { get; set; }
        public int RenovationSuggestions
        {
            get { return renovationSuggestions; }
            set
            {
                if (renovationSuggestions != value)
                {
                    renovationSuggestions = value;
                    OnPropertyChanged("RenovationSuggestions");
                }
            }
        }

        private string month { get; set; }
        public string Month
        {
            get { return month; }
            set
            {
                if (month != value)
                {
                    month = value;
                    OnPropertyChanged("Month");
                }
            }
        }

        private double busyness { get; set; }
        public double Busyness
        {
            get { return busyness; }
            set
            {
                if (busyness != value)
                {
                    busyness = value;
                    OnPropertyChanged("Busyness");
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

        public AccommodationStatisticsDTO(int year, int bookings, int cancelations, int reschedulings, int renovationSuggestions, string month, int totalDays)
        {
            Year = year;
            Bookings = bookings;
            Cancelations = cancelations;
            Reschedulings = reschedulings;
            RenovationSuggestions = renovationSuggestions;
            if (month.Equals("N/A")) {
                Month = month;
                if (IsLeapYear(year))
                {
                    Busyness = totalDays/366.0 ;
                }
                else
                {
                    Busyness = totalDays/365.0;
                }
             }
            else
                InitializeMonth(month, totalDays);

            if (bookings == 0)
                Busyness = totalDays;

        }

        static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        private void InitializeMonth(string month, int totalDays)
        {
            switch (month)
            {
                case "1":
                    Month = "January";
                    Busyness = totalDays / 31.0;
                    break;
                case "2":
                    Month = "February";
                    if (IsLeapYear(Year))
                        Busyness = totalDays / 29.0;
                    else
                        Busyness = totalDays / 28.0;
                    break;
                case "3":
                    Month = "March";
                    Busyness = totalDays / 31.0;
                    break;
                case "4":
                    Month = "April";
                    Busyness = totalDays / 30.0;
                    break;
                case "5":
                    Month = "May";
                    Busyness = totalDays / 31.0;
                    break;
                case "6":
                    Month = "June";
                    Busyness = totalDays / 30.0;
                    break;
                case "7":
                    Month = "July";
                    Busyness = totalDays / 31.0;
                    break;
                case "8":
                    Month = "August";
                    Busyness = totalDays / 31.0;
                    break;
                case "9":
                    Month = "September";
                    Busyness = totalDays / 30.0;
                    break;
                case "10":
                    Month = "October";
                    Busyness = totalDays / 31.0;
                    break;
                case "11":
                    Month = "November";
                    Busyness = totalDays/30.0;
                    break;
                case "12":
                    Month = "December";
                    Busyness = totalDays/31.0;
                    break;
                default:
                    Month = "N/A";
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Year}-{Bookings}-{Cancelations}-{Reschedulings}-{RenovationSuggestions}";
        }
    }
}
