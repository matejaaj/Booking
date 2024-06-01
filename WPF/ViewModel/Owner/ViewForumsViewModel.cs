using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class ViewForumsViewModel : INotifyPropertyChanged
    {
        private Domain.Model.Owner _loggedInOwner;
        private ViewForumsPage _currentPage;
        private ForumService _forumService;
        private UserService _userService;
        private LocationService _locationService;
        private AccommodationService _accommodationService;
        public ICommand ItemClickedCommand { get; }

        private ObservableCollection<ForumDisplayOwnerDTO> _forums;
        public ObservableCollection<ForumDisplayOwnerDTO> Forums
        {
            get { return _forums; }
            set
            {
                _forums = value;
                OnPropertyChanged(nameof(Forums));
            }
        }
        public ViewForumsViewModel(Domain.Model.Owner loggedInOwner, ViewForumsPage viewForumsPage)
        {
            _loggedInOwner = loggedInOwner;
            _currentPage = viewForumsPage;
            _forumService = new ForumService(Injector.CreateInstance<IForumRepository>());
            _userService = new UserService(Injector.CreateInstance<IUserRepository>());
            _locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            ItemClickedCommand = new RelayCommand(ShowForum);
            Update();
        }

        private void Update()
        {
            Forums = new ObservableCollection<ForumDisplayOwnerDTO>();
            List<int> locationIds = _accommodationService.GetLocationIdsByOwner(_loggedInOwner);
            List<Forum> forums = _forumService.GetByLocationIds(locationIds);
            foreach (var forum in forums)
            {
                Location location = _locationService.GetLocationById(forum.LocationId);
                User guest = _userService.GetById(forum.UserId);
                Forums.Add(new ForumDisplayOwnerDTO(guest, location, forum));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowForum(object obj)
        {
            var dto = (ForumDisplayOwnerDTO)obj;
            _currentPage.NavigationService.Navigate(new ViewForumPage(_loggedInOwner, dto));
            //throw new NotImplementedException();
        }
    }
}
