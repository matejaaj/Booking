using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookingApp.DTO
{
    public class ForumDTO : INotifyPropertyChanged
    {
        private readonly LocationService _locationService;

        public int Id { get; set; }

        private int _locationId;
        public int LocationId
        {
            get { return _locationId; }
            set
            {
                if (_locationId != value)
                {
                    _locationId = value;
                    OnPropertyChanged(nameof(LocationId));
                    Location = GetLocationNameById(_locationId);
                }
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ForumDTO()
        {
            Location = "not set";
        }

        public ForumDTO(Forum forum)
        {
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());

            Id = forum.Id;
            LocationId = forum.LocationId;
            Comment = forum.Comment;

            Location = GetLocationNameById(LocationId);
        }

        private string GetLocationNameById(int locationId)
        {
            var location = _locationService.GetLocationById(locationId);
            return location != null ? $"{location.City}, {location.Country}" : "Unknown Location";
        }

        public Forum ToForum()
        {
            return new Forum
            {
                Id = this.Id,
                LocationId = this.LocationId,
                Comment = this.Comment
            };
        }

        public override string ToString()
        {
            return $"{Id}-{Location}-{Comment}";
        }
    }
}
