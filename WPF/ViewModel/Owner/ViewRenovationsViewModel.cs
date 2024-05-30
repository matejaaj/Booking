using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class ViewRenovationsViewModel : INotifyPropertyChanged
    {
        public static RenovationService renovationService { get; set; }
        public static AccommodationService accommodationService { get; set; }
        public static LocationService locationService { get; set; }
        public static AccommodationReservationService accommodationReservationService { get; set; }
        public Domain.Model.Owner LoggedInOwner { get; set; }
        
        private ObservableCollection<RenovationDTO> _pastRenovations;
        public ObservableCollection<RenovationDTO> PastRenovations
        {
            get { return _pastRenovations; }
            set
            {
                _pastRenovations = value;
                OnPropertyChanged(nameof(PastRenovations));
            }
        }
        public RenovationDTO SelectedFutureOrCurrentRenovation { get; set; }

        private ObservableCollection<RenovationDTO> _futureAndCurrentRenovations;
        public ObservableCollection<RenovationDTO> FutureAndCurrentRenovations
        {
            get { return _futureAndCurrentRenovations; }
            set
            {
                _futureAndCurrentRenovations = value;
                OnPropertyChanged(nameof(FutureAndCurrentRenovations));
            }
        }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewRenovationsViewModel(Domain.Model.Owner loggedInOwner) 
        {
            InitializeServices();
            LoggedInOwner = loggedInOwner;
            FutureAndCurrentRenovations = new ObservableCollection<RenovationDTO>();
            PastRenovations = new ObservableCollection<RenovationDTO>();
            CancelCommand = new CancelCommand(OnCancel, CanCancel);
            Update();
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void OnCancel(object parameter)
        {
            var selectedRenovation = parameter as RenovationDTO;
            if (selectedRenovation != null)
            {
                renovationService.DeleteById(selectedRenovation.Id);
                Update();
            }
        }

        private void Update()
        {
            PastRenovations = new ObservableCollection<RenovationDTO>(renovationService.GetPastRenovations(LoggedInOwner));
            FutureAndCurrentRenovations = new ObservableCollection<RenovationDTO>(renovationService.GetCurrentAndFutureRenovations(LoggedInOwner));
        }

        private void InitializeServices()
        {
            accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            renovationService = new RenovationService(accommodationService, accommodationReservationService, locationService, Injector.CreateInstance<IRenovationRepository>());
        }
    }
}
