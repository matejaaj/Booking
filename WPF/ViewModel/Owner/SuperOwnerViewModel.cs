using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class SuperOwnerViewModel : INotifyPropertyChanged
    {
        private string validationMessage;
        public string ValidationMessage
        {
            get { return validationMessage; }
            set
            {
                validationMessage = value;
                OnPropertyChanged(nameof(ValidationMessage));
            }
        }

        private int ratingsNumber { get; set; }
        public int RatingsNumber
        {
            get { return ratingsNumber; }
            set
            {
                ratingsNumber = value;
                OnPropertyChanged(nameof(RatingsNumber));
            }
        }

        private double averageScore { get; set; }
        public double AverageScore
        {
            get { return averageScore; }
            set
            {
                averageScore = value;
                OnPropertyChanged(nameof(AverageScore));
            }
        }
        public Domain.Model.Owner LoggedInOwner { get; set; }
        public List<AccommodationReservation> OwnerAccommodationReservations { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public bool isSuperOwner { get; set; }
        private static AccommodationService _accommodationService;
        private static AccommodationReservationService _accommodationReservationService;
        private static AccommodationAndOwnerRatingService _accommodationAndOwnerRatingService;
        private static OwnerService _ownerService;
        public ICommand SuperOwnerCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SuperOwnerViewModel(Domain.Model.Owner loggedInOwner) 
        {
            InitializeServices();
            LoggedInOwner = loggedInOwner;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetByUser(LoggedInOwner));
            InitializeAccommodationReservaions();
            CalculateRating();
            SuperOwnerCommand = new RelayCommand(SuperOwnerClick);
        }

        private void SuperOwnerClick(object obj)
        {
            if (LoggedInOwner.SuperOwner)
            {
                ValidationMessage = $"You are already a Super owner!";
            }
            else
            {
                if (isSuperOwner == false)
                {
                    ValidationMessage = $"You do not meet the requirements to become a Super owner!";
                }
                else
                {
                    ValidationMessage = $"You have become a Super owner!";
                    LoggedInOwner.SuperOwner = true;
                    _ownerService.Update(LoggedInOwner);
                }
            }
        }


        private void InitializeServices()
        {
            _accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>());
            _accommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            _accommodationAndOwnerRatingService = new AccommodationAndOwnerRatingService(_accommodationReservationService, Injector.CreateInstance<IAccommodationAndOwnerRatingRepository>());
            _ownerService = new OwnerService(Injector.CreateInstance<IOwnerRepository>());
        }

        private void CalculateRating()
        {
            RatingsNumber = GetRatings().Count();
            var individualAverages = _accommodationAndOwnerRatingService.CalculateIndividualAverages(GetRatings());
            AverageScore = _accommodationAndOwnerRatingService.CalculateAverageScore(individualAverages, RatingsNumber);
            CheckSuperOwner();
        }

        private void CheckSuperOwner()
        {
            LoggedInOwner.NumberOfRatings = RatingsNumber;
            LoggedInOwner.AverageRating = AverageScore;
            if (RatingsNumber > 50 && AverageScore > 4.5)
            {
                isSuperOwner = true;
            }
            else
            {
                LoggedInOwner.SuperOwner = false;
            }
            _ownerService.Update(LoggedInOwner);
        }

        private void InitializeAccommodationReservaions()
        {
            OwnerAccommodationReservations = _accommodationReservationService.GetByAccommodationIds(Accommodations);
        }

        private List<AccommodationAndOwnerRating> GetRatings()
        {
            return _accommodationAndOwnerRatingService.GetByReservations(OwnerAccommodationReservations);
        }
    }
}
