using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist.Factories;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveMainTabViewModel
    {
        public ObservableCollection<DriveReservationViewModel> Reservations { get; private set; }

        private User _user;

        private DriveReservationViewModelFactory factory;

        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private DetailedLocationService _detailedLocationService;

        public DriveMainTabViewModel(User user, DriveReservationService driveReservationService, UserService userService, DetailedLocationService detailedLocationService)
        {
            _user = user;
            _driveReservationService = driveReservationService;
            _userService = userService;
            _detailedLocationService = detailedLocationService;

            factory = new DriveReservationViewModelFactory(_user, driveReservationService, userService,
                detailedLocationService);

            Reservations = new ObservableCollection<DriveReservationViewModel>(factory.CreateViewModels());
        }
    }
}
