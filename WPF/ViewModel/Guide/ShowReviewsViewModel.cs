using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Guide
{

    internal class ShowReviewsViewModel
    {
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        private List<TourInstance> allTourInstances;
        public TourInstance SelectedInstance { get; set; }
        private  TourInstanceService _tourInstanceService;
        public ShowReviewsViewModel(TourDTO tourDTO)
        {
            InitializeServices();

            allTourInstances = _tourInstanceService.GetAllByTourId(tourDTO.Id);
            TourInstances = new ObservableCollection<TourInstance>();

            foreach(var instance in allTourInstances)
            {
                if (instance.IsCompleted)
                    TourInstances.Add(instance);
            }
        }

        public void ShowReviews()
        {
            Reviews reviewsWindow = new Reviews(SelectedInstance);
            reviewsWindow.Show();
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
        }
    }
}
