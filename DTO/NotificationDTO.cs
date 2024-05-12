using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.ComponentModel;
using System.Timers;

namespace BookingApp.DTO
{
    public class NotificationDTO : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        private DateTime dateIssued;
        public DateTime DateIssued
        {
            get { return dateIssued; }
            set
            {
                if (dateIssued != value)
                {
                    dateIssued = value;
                    OnPropertyChanged(nameof(DateIssued));
                }
            }
        }

        private int targetUserId;
        public int TargetUserId
        {
            get { return targetUserId; }
            set
            {
                if (targetUserId != value)
                {
                    targetUserId = value;
                    OnPropertyChanged(nameof(TargetUserId));
                }
            }
        }

        private Timer _timer;

        public NotificationDTO(int id, string title, string text, DateTime dateIssued, int targetUserId)
        {
            Id = id;
            Title = title;
            Text = text;
            DateIssued = dateIssued;
            TargetUserId = targetUserId;

            _timer = new Timer(60000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(TimeElapsed));
        }

        public string TimeElapsed
        {
            get
            {
                var elapsed = DateTime.Now - DateIssued;
                if (elapsed.TotalDays >= 1)
                    return $"{(int)elapsed.TotalDays} days ago";
                else if (elapsed.TotalHours >= 1)
                    return $"{(int)elapsed.TotalHours} hours ago";
                else
                    return $"{(int)elapsed.TotalMinutes} minutes ago";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ~NotificationDTO()
        {
            _timer.Stop();
        }
    }
}
