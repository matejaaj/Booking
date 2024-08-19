using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Tourist.Factories
{
    public class DriveReservationViewModelFactory
    {
        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private DetailedLocationService _detailedLocationService;
        private User _user;
        

        public DriveReservationViewModelFactory(User loggedUser, DriveReservationService driveReservationService, UserService userService, DetailedLocationService detailedLocationService)
        {
            _user = loggedUser;
            _driveReservationService = driveReservationService;
            _userService = userService;
            _detailedLocationService = detailedLocationService;
        }

        public List<DriveReservationViewModel> CreateViewModels()
        {
            List<DriveReservationViewModel> viewModels = new List<DriveReservationViewModel>();
            foreach (var reservation in _driveReservationService.GetActiveReservationsForUser(_user.Id))
            {
                string driver;
                if (reservation.DriverId == 0) 
                    driver = "?";
                else 
                    driver = _userService.GetById(reservation.DriverId).Username;

                DriveReservationViewModel viewModel = new DriveReservationViewModel()
                {
                    DriveReservationId = reservation.Id,
                    Driver = driver,
                    Time = reservation.DepartureTime,
                    TimeDisplay = reservation.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    StartAddress = _detailedLocationService.GetById(reservation.PickupLocationId).Address,
                    EndAddress = _detailedLocationService.GetById(reservation.DropoffLocationid).Address,
                    DelayDriver = reservation.DelayMinutesDriver,
                    DelayTourist = reservation.DelayMinutesTourist,
                    Status = GetReservationStatus(reservation.DriveReservationStatusId, reservation.DelayMinutesDriver, reservation.DelayMinutesTourist)

                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }


        private string GetReservationStatus(int reservationStatusId, double delayDriver, double delayTourist)
        {
            string status;
            if (delayDriver != 0)
            {
                status = string.Format(TranslationSource.Instance["DriverDelayMessage"], delayDriver);
            }
            else
            {
                switch (reservationStatusId)
                {
                    case 12:
                        status = TranslationSource.Instance["DriverNotFoundMessage"];
                        break;
                    case 4:
                        status = TranslationSource.Instance["DriverArrivedMessage"];
                        break;
                    case 5:
                    case 2:
                    case 13:
                        status = TranslationSource.Instance["DriveAcceptedMessage"];
                        break;
                    default:
                        status = TranslationSource.Instance["UnknownStatusMessage"];
                        break;
                }
            }
            return status;
        }
    }
}
