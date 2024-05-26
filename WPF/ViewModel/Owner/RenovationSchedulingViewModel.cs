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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class RenovationSchedulingViewModel : INotifyPropertyChanged
    {
        public AccommodationPageDTO SelectedAccommodation { get; set; }
        private Accommodation accommodation;
        public Accommodation Accommodation
        {
            get => accommodation;
            set
            {
                if (value != accommodation)
                {
                    accommodation = value;
                    OnPropertyChanged("Accommodation");
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        private int estimatedDuration;
        public int EstimatedDuration
        {
            get => estimatedDuration;
            set
            {
                if (value != estimatedDuration)
                {
                    estimatedDuration = value;
                    OnPropertyChanged("EstimatedDuration");
                }
            }
        }

        private ObservableCollection<FreeDatesDTO> _freeDates;
        public ObservableCollection<FreeDatesDTO> FreeDates
        {
            get { return _freeDates; }
            set
            {
                _freeDates = value;
                OnPropertyChanged(nameof(FreeDates));
            }
        }

        private FreeDatesDTO _selectedFreeDate;
        public FreeDatesDTO SelectedFreeDate
        {
            get { return _selectedFreeDate; }
            set
            {
                _selectedFreeDate = value;
                OnPropertyChanged(nameof(SelectedFreeDate));
            }
        }

        public static RenovationService renovationService { get; set; }
        public static AccommodationService accommodationService { get; set; }
        public static AccommodationReservationService accommodationReservationService {get;set;}

        public ICommand SearchCommand { get; }
        public ICommand ScheduleCommand { get; }

        private RenovationSchedulingPage _currentPage;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RenovationSchedulingViewModel(Domain.Model.Accommodation accommodation, AccommodationPageDTO selectedAccommodation, RenovationSchedulingPage renovationSchedulingPage)
        {
            InitializeServices();
            Accommodation = accommodation;
            SelectedAccommodation = selectedAccommodation;
            FreeDates = new ObservableCollection<FreeDatesDTO>();
            SearchCommand = new RelayCommand(Search);
            ScheduleCommand = new CancelCommand(OnSchedule, CanSchedule);
            _currentPage = renovationSchedulingPage;
        }

        private void Search(object obj)
        {
            if (!ValidateFields())
            {
                MessageBox.Show("Please enter dates and a duration estimate",
                 "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (areDatesPast())
            {
                MessageBox.Show("Please enter a future date",
                 "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                FreeDates.Clear();
                FindAvailableDates();
            }
        }

        private bool CanSchedule(object obj)
        {
            return true;
        }

        private void OnSchedule(object obj)
        {
            var schedule = obj as FreeDatesDTO;
            SelectedFreeDate = schedule;
            renovationService.Save(new Renovation(SelectedFreeDate.StartDate, SelectedFreeDate.EndDate, Accommodation.AccommodationId));
            NavigateToAccommodationViewPage(_currentPage);
        }

        private void InitializeServices()
        {
            accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            renovationService = new RenovationService(accommodationService, accommodationReservationService, Injector.CreateInstance<IRenovationRepository>());
        }

        private bool areDatesPast()
        {
            if (StartDate < DateTime.Today || EndDate < DateTime.Today)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateFields()
        {
            if(StartDate == null || EndDate == null || EstimatedDuration == null || !canDurationFit())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool canDurationFit()
        {
            var timeSpan = EndDate - StartDate;
            if (timeSpan.TotalDays <= EstimatedDuration)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void FindAvailableDates()
        {
           List<FreeDatesDTO> dates =  renovationService.FindAvailableDates(StartDate, EndDate, EstimatedDuration, Accommodation);
           FreeDates = new ObservableCollection<FreeDatesDTO>(dates);
        }

        private void NavigateToAccommodationViewPage(RenovationSchedulingPage renovationSchedulingPage)
        {
            ViewAccommodationPage page = new ViewAccommodationPage(SelectedAccommodation);
            renovationSchedulingPage.NavigationService.Navigate(page);
        }
    }
}
