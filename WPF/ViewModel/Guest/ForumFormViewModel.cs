using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class ForumFormViewModel : INotifyPropertyChanged
    {
        private ForumService _forumService;
        private LocationService _locationService;

        private List<ForumDTO> _forums;
        private List<LocationDTO> _locations;

        public List<ForumDTO> Forums
        {
            get { return _forums; }
            set
            {
                _forums = value;
                OnPropertyChanged();
            }
        }

        public List<LocationDTO> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        public ForumFormViewModel(User loggedInGuest)
        {
            InitializeServices();
            LoadForums();
            LoadLocations();
            CheckIfCreator(loggedInGuest);
        }

        private void CheckIfCreator(User loggedInGuest)
        {
            //_forumService.UserIsCreator(loggedInGuest.Id)
        }

        private void InitializeServices()
        {
            _forumService = new ForumService(Injector.CreateInstance<IForumRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
        }

        private void LoadForums()
        {
            Forums = _forumService.GetAll().Select(forum => new ForumDTO(forum)).ToList();
        }

        private void LoadLocations()
        {
            Locations = _locationService.GetAll().Select(loc => new LocationDTO(loc)).ToList();
        }

        public string GetLocationNameById(int locationId)
        {
            var location = Locations.FirstOrDefault(loc => loc.Id == locationId);
            return location != null ? $"{location.City}, {location.Country}" : "Unknown Location";
        }

        public void OpenCreateForumWindow()
        {
            var createForumWindow = new CreateForum();
            createForumWindow.ShowDialog();
            LoadForums();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

