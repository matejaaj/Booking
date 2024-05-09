using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class FreeDatesDTO : INotifyPropertyChanged
    {
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

        public FreeDatesDTO(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            TimeSpan = startDate.ToString("dd.MM.yyyy") + "   -   " + endDate.ToString("dd.MM.yyyy");
        }
    }
}
