using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class GroupDriveFormViewModel : BaseDriveFormViewModel
    {
        private int _numberOfPeople;
        public int NumberOfPeople
        {
            get { return _numberOfPeople; }
            set
            {
                _numberOfPeople = value;
                OnPropertyChanged(nameof(NumberOfPeople));
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> _languages = new ObservableCollection<KeyValuePair<int, string>>();
        public ObservableCollection<KeyValuePair<int, string>> Languages
        {
            get { return _languages; }
            set
            {
                if (_languages != value)
                {
                    _languages = value;
                    OnPropertyChanged(nameof(Languages));
                }
            }
        }
        private KeyValuePair<int, string> _selectedLanguage;
        public KeyValuePair<int, string> SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (!Equals(_selectedLanguage, value))
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }



        private DetailedLocationService _detailedLocationService;
        private DriveReservationService _driveReservationService;
        private User _tourist;
        private LanguageService _languageService;
        private GroupDriveReservationService _groupDriveReservationService;

        public GroupDriveFormViewModel(User user, DetailedLocationService detailedLocationService)
        {
            _tourist = user;
            _detailedLocationService = detailedLocationService;
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _groupDriveReservationService = new GroupDriveReservationService(Injector.CreateInstance<IGroupDriveReservationRepository>());

            FillLanguages();
        }

        private void FillLanguages()
        {
            var languages = _languageService.GetAll();
            Languages.Clear();
            foreach (var language in languages)
            {
                Languages.Add(new KeyValuePair<int, string>(language.Id, language.Name));
            }
        }

        public void ReserveGroupDrive()
        {
            DateTime departure = CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(SelectedCountry.Key, StartAddress);
            DetailedLocation end = new DetailedLocation(SelectedCountry.Key, EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            GroupDriveReservation reservation = new GroupDriveReservation(NumberOfPeople, SelectedLanguage.Key,
                start.Id, end.Id, departure, _tourist.Id, 14);

            _groupDriveReservationService.Save(reservation);
        }

    }
}
