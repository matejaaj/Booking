using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourInstanceViewModel
    {
        public TourInstanceViewModel()
        {
        }

        private int id;
        private string name;
        private string currentCheckpoint;
        private DateTime date;
        private List<TourGuest> guests = new List<TourGuest>();
        private List<string> checkpointNames = new List<string>();
        private bool isFinished;


        public int Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string CurrentCheckpoint
        {
            get => currentCheckpoint;
            set
            {
                if (currentCheckpoint != value)
                {
                    currentCheckpoint = value;
                    OnPropertyChanged(nameof(CurrentCheckpoint));
                }
            }
        }
        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        public List<TourGuest> Guests
        {
            get => guests;
            set
            {
                if (guests != value)
                {
                    guests = value;
                    OnPropertyChanged(nameof(Guests));
                }
            }
        }
        public List<string> CheckpointNames
        {
            get => checkpointNames;
            set
            {
                if (checkpointNames != value)
                {
                    checkpointNames = value;
                    OnPropertyChanged(nameof(CheckpointNames));
                }
            }
        }
        public bool IsFinished
        {
            get => isFinished;
            set
            {
                if (isFinished != value)
                {
                    isFinished = value;
                    OnPropertyChanged(nameof(IsFinished));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
