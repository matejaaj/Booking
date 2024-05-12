using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class SuperGuestOverviewViewModel
    {
        private SuperGuestService _superGuestService;
        public ObservableCollection<SuperGuest> SuperGuests { get; set; }
        public SuperGuestOverviewViewModel(User loggedInGuest)
        {
            InitializeServices();
            _superGuestService.FindSuperGuests(loggedInGuest);
            SuperGuests = new ObservableCollection<SuperGuest>(_superGuestService.GetAll());
        }
        private void InitializeServices()
        {
            var accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            var accommodationReservationService = new AccommodationReservationService(accommodationService, Injector.CreateInstance<IAccommodationReservationRepository>());
            _superGuestService = new SuperGuestService(Injector.CreateInstance<ISuperGuestRepository>(),accommodationReservationService);
        }
    }
}
