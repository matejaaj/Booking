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
        private KeyValuePair<int, string> _selectedLanguage;

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

        private VehicleService _vehicleService;
        private DetailedLocationService _detailedLocationService;
        private LocationService _locationService;
        private DriveReservationService _driveReservationService;
        private UserService _userService;
        private User _tourist;
        private LanguageService _languageService;
        private GroupDriveReservationService _groupDriveReservationService;

        public GroupDriveFormViewModel(User user, UserService userService, VehicleService vehicleService, DetailedLocationService detailedLocationService, LocationService locationService, DriveReservationService driveReservationService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _detailedLocationService = detailedLocationService;
            _locationService = locationService;
            _driveReservationService = driveReservationService;
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _groupDriveReservationService = new GroupDriveReservationService(Injector.CreateInstance<IGroupDriveReservationRepository>());
            _tourist = user;

            FillLanguages();
        }

        private void FillLanguages()
        {
            var languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            var languages = languageService.GetAll();
            Languages.Clear();
            foreach (var language in languages)
            {
                Languages.Add(new KeyValuePair<int, string>(language.Id, language.Name));
            }
        }

        public void ReserveGroupDrive()
        {

        }

    }
}
