using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class SuperGuideViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged("SelectedLanguage");
                }
            }
        }

        private int _numberOfGuidedTours;
        public int NumberOfGuidedTours
        {
            get => _numberOfGuidedTours;
            set
            {
                if (_numberOfGuidedTours != value)
                {
                    _numberOfGuidedTours = value;
                    OnPropertyChanged("NumberOfGuidedTours");
                }
            }
        }

        private double _averageGrade;
        public double AverageGrade
        {
            get => _averageGrade;
            set
            {
                if (_averageGrade != value)
                {
                    _averageGrade = value;
                    OnPropertyChanged("AverageGrade");
                }
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                if(_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private string _validTo;
        public string ValidTo
        {
            get => _validTo;
            set
            {
                if(_validTo != value)
                {
                    _validTo = value;
                    OnPropertyChanged("ValidTo");
                }
            }
        }


        public List<Language> Languages { get; set; }
        private User user { get; set; }

        private LanguageService _languageService;
        private TourService _tourService;
        private TourInstanceService _tourInstanceService;
        private TourReviewService _tourReviewService;
        private SuperGuideService _superGuideService;

        public ObservableCollection<KeyValuePair<string, int>> GuidedToursData { get; set; }

        public SuperGuideViewModel(User user)
        {
            InitializeServices();
            this.user = user;
            Languages = _languageService.GetAll();
            GuidedToursData = new ObservableCollection<KeyValuePair<string, int>>(_tourInstanceService.GetGuidedToursPerMonth(user.Id));
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _tourService = new TourService(Injector.CreateInstance<ITourRepository>(), _tourGuestService, _tourInstanceService);
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _tourReviewService = new TourReviewService(Injector.CreateInstance<ITourReviewRepository>());
            _superGuideService = new SuperGuideService(Injector.CreateInstance<ISuperGuideRepository>());
        }

        public void ShowProgress()
        {
            if (SelectedLanguage == null)
                return;

            SuperGuide superGuide = _superGuideService.CheckIsSuperGuide(user.Id, SelectedLanguage.Id);

            if (superGuide == null)
            {
                HandleNoSuperGuide();
            }
            else
            {
                HandleSuperGuide(superGuide);
            }
        }

        private void HandleNoSuperGuide()
        {
            NumberOfGuidedTours = _tourService.GetTotalGuidedTours(user.Id, SelectedLanguage.Id);
            List<int> instanceIds = _tourService.GetInstances(user.Id, SelectedLanguage.Id);
            AverageGrade = _tourReviewService.GetAverageGrade(instanceIds);
            CheckStatus();
        }

        private void HandleSuperGuide(SuperGuide superGuide)
        {
            if (superGuide.ExpirationDate < DateTime.Now)
            {
                _superGuideService.Delete(superGuide);
                HandleNoSuperGuide();
            }
            else
            {
                SetAchievedStatus(superGuide);
            }
        }

        private void SetAchievedStatus(SuperGuide superGuide)
        {
            Status = "STATUS: ACHIEVED";
            ValidTo = superGuide.ExpirationDate.ToString("yyyy.MM.dd");
            NumberOfGuidedTours = _tourService.GetTotalGuidedToursSinceSuperStatus(user.Id, SelectedLanguage.Id, superGuide.AccuiredDate);
            List<int> instanceIds = _tourService.GetInstancesSinceSuperStatus(user.Id, SelectedLanguage.Id, superGuide.AccuiredDate);
            AverageGrade = _tourReviewService.GetAverageGrade(instanceIds);
        }

        private void CheckStatus()
        {
            if (NumberOfGuidedTours >= 20 && AverageGrade >= 4.0)
            {
                Status = "STATUS: ACHIEVED";
                _superGuideService.Save(new SuperGuide(user.Id, SelectedLanguage.Id, DateTime.Now, DateTime.Now.AddYears(1)));
                ValidTo = DateTime.Now.AddYears(1).ToString("yyyy.MM.dd");
            }
            else
            {
                Status = "STATUS: NOT ACHIEVED";
                ValidTo = string.Empty;
            }
        }


    }
}
