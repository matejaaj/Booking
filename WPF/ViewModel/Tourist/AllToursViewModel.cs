using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.DTO.Factories;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class AllToursViewModel
    {
        public User Tourist { get; }
        private TourDTOFactory _tourDTOFactory;

        private TourService _tourService;
        private TourInstanceService _tourInstanceService;
        private TourGuestService _tourGuestService;
        private TourReservationService _tourReservationService;
        private CheckpointService _checkPointService;
        private VoucherService _voucherService;
        private LocationService _locationService;
        private LanguageService _languageService;
        private ImageService _imageService;
        


        public List<TourDTO> Tours { get; private set; }

        public ICommand ShowTourDetailsCommand { get; }


        public AllToursViewModel(User loggedUser, TourService tourService, TourInstanceService tourInstanceService, CheckpointService checkpointService, ImageService imageService, LocationService locationService, LanguageService languageService, TourGuestService tourGuestService, TourReservationService tourReservationService, VoucherService voucherService)
        {
            Tourist = loggedUser;

            _tourService = tourService;
            _tourInstanceService = tourInstanceService;
            _tourGuestService = tourGuestService;
            _tourReservationService = tourReservationService;
            _checkPointService = checkpointService;
            _voucherService = voucherService;
            _locationService = locationService;
            _languageService = languageService;
            _imageService = imageService;

            ShowTourDetailsCommand = new RelayCommand(ShowTourDetails);

            _tourDTOFactory = new TourDTOFactory(_locationService, _languageService, _imageService, _checkPointService,
                _tourInstanceService);


            Tours = _tourDTOFactory.CreateTourDTOs(_tourService.GetAll());
        }

        private void ShowTourDetails(object parameter)
        {
            if (parameter is TourDTO tour)
            {
                var window = new TourDetailsWindow(tour, Tourist);
                window.Show();
            }
        }
    }
}
