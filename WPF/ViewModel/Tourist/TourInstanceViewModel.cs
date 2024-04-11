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
        private int id;
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

        private string name;
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

        private DateTime date;
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

        private List<string> guestList = new List<string>();
        public List<string> GuestList
        {
            get => guestList;
            set
            {
                if (guestList != value)
                {
                    guestList = value;
                    OnPropertyChanged(nameof(GuestList));
                }
            }
        }

        private List<string> checkpointNames = new List<string>();
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

        public TourInstanceViewModel()
        {

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
