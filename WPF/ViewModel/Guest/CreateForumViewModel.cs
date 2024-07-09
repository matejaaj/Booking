using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class CreateForumViewModel
    {
        private readonly ForumService _forumService;
        private readonly LocationService _locationService;

        public CreateForumViewModel()
        {
            _forumService = new ForumService(Injector.CreateInstance<IForumRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
        }

        public void CreateForum(LocationDTO selectedLocation, string comment)
        {
            if (selectedLocation != null && !string.IsNullOrWhiteSpace(comment))
            {
                _forumService.SaveForum(selectedLocation, comment);
                MessageBox.Show("Forum successfully created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a location and enter a comment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public List<LocationDTO> LoadLocations()
        {
            var locations = _locationService.GetAll();
            return locations.Select(loc => new LocationDTO(loc)).ToList();
        }
    }
}
