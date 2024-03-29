using BookingApp.Model;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourDTO : INotifyPropertyChanged
    {
        private int id { get; set; }

        public int StudentId
        {
            get { return id; }
            set { 
                if(id != value)
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
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string description { get; set; }
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set
            {
                if (locationId != value)
                {
                    locationId = value;
                    OnPropertyChanged("LocationId");
                }
            }
        }

        private int languageId;
        public int LanguageId
        {
            get { return languageId; }
            set
            {
                if (languageId != value)
                {
                    languageId = value;
                    OnPropertyChanged("LanguageId");
                }
            }
        }

        private int maximumCapacity;
        public int MaximumCapacity
        {
            get { return maximumCapacity; }
            set
            {
                if (maximumCapacity != value)
                {
                    maximumCapacity = value;
                    OnPropertyChanged("MaximumCapacity");
                }
            }
        }

        private float durationHours;
        public float DurationHours
        {
            get { return durationHours; }
            set
            {
                if (durationHours != value)
                {
                    durationHours = value;
                    OnPropertyChanged("DurationHours");
                }
            }
        }
        private string location;
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
        private string language;
        public string Language
        {
            get { return language; }
            set
            {
                if (language != value)
                {
                    language = value;
                    OnPropertyChanged("Language");
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


        public TourDTO()
        {
            Language = "not set";
            Location = "not set";
        }

        public TourDTO(Tour tour)
        {
            id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            LocationId = tour.LocationId;
            LanguageId = tour.LanguageId;
            MaximumCapacity = tour.MaximumCapacity;
            DurationHours = tour.DurationHours;

            Language = "not set";
            Location = "not set";

        }

        public Tour ToTour()
        {
            return new Tour(name, description, locationId, languageId, maximumCapacity, durationHours);
        }


    }
}
