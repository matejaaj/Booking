using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class MyToursViewModel
    {
        private ObservableCollection<TourInstanceViewModel> _tours;
        private User _tourist;

        private TourService _tourService;
        private TourInstanceService _tourInstanceServis;
        private TourGuestService _tourGuestService;
        private TourReservationService _tourReservationService;

        public MyToursViewModel(User loggedUser)
        {
            _tourist = loggedUser;
            InitializeServices();
            CreateViewModels();
        }

        private void CreateViewModels()
        {
              
        }

        private void InitializeServices()
        {
            _tourService = new TourService();
            _tourInstanceServis = new TourInstanceService();
            _tourGuestService = new TourGuestService();
        }
    }
}
