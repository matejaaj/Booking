using BookingApp.Application.UseCases;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using GalaSoft.MvvmLight.Command;
using BookingApp.Application;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class GroupDriveFormViewModel : BaseDriveFormViewModel, IDataErrorInfo
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

        private readonly DetailedLocationService _detailedLocationService;
        private readonly DriveReservationService _driveReservationService;
        private readonly User _tourist;
        private readonly LanguageService _languageService;
        private readonly GroupDriveReservationService _groupDriveReservationService;
        public ICommand CloseWindowCommand { get; }
        public ICommand ReserveCommand { get; }

        public GroupDriveFormViewModel(User user, DetailedLocationService detailedLocationService, DriveReservationService driveReservationService, ICommand closeCommand)
        {
            _tourist = user;
            _detailedLocationService = detailedLocationService;
            _driveReservationService = driveReservationService;
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _groupDriveReservationService = new GroupDriveReservationService(Injector.CreateInstance<IGroupDriveReservationRepository>());
            CloseWindowCommand = closeCommand;
            ReserveCommand = new BookingApp.Commands.RelayCommand(ReserveGroupDrive, CanReserveGroupDrive);
            SelectedLanguage = new KeyValuePair<int, string>(-1, string.Empty);


            FillLanguages();
            ValidateAllProperties();
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

        public void ReserveGroupDrive(object parameter)
        {
            DateTime departure = CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(SelectedCountry.Key, StartAddress);
            DetailedLocation end = new DetailedLocation(SelectedCountry.Key, EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            GroupDriveReservation reservation = new GroupDriveReservation(NumberOfPeople, SelectedLanguage.Key,
                start.Id, end.Id, departure, _tourist.Id, 14);

            _groupDriveReservationService.Save(reservation);
            MessageBox.Show(TranslationSource.Instance["GroupSucessful"]);

            if (parameter is Window window)
            {
                window.Close();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case nameof(SelectedCountry):
                        if (SelectedCountry.Value == string.Empty || !Countries.Any(c => c.Value == SelectedCountry.Value))
                            result = TranslationSource.Instance["ValidationCountry"];
                        break;
                    case nameof(SelectedCity):
                        if (SelectedCity.Value == string.Empty || !Cities.Any(c => c.Value == SelectedCity.Value))
                            result = TranslationSource.Instance["ValidationCity"];
                        break;
                    case nameof(StartAddress):
                        if (string.IsNullOrWhiteSpace(StartAddress))
                            result = TranslationSource.Instance["ValidationStartLocationRequired"];
                        else if (!Regex.IsMatch(StartAddress, @"^[a-zA-Z0-9\s]+$"))
                            result = TranslationSource.Instance["ValidationStartLocationFormat"];
                        break;
                    case nameof(EndAddress):
                        if (string.IsNullOrWhiteSpace(EndAddress))
                            result = TranslationSource.Instance["ValidationEndLocationRequired"];
                        else if (!Regex.IsMatch(EndAddress, @"^[a-zA-Z0-9\s]+$"))
                            result = TranslationSource.Instance["ValidationEndLocationFormat"];
                        break;
                    case nameof(SelectedDate):
                        if (SelectedDate < DateTime.Today)
                            result = TranslationSource.Instance["ValidationDate"];
                        break;
                    case nameof(SelectedHour):
                        if (string.IsNullOrWhiteSpace(SelectedHour))
                            result = TranslationSource.Instance["ValidationHour"];
                        else if (!Regex.IsMatch(SelectedHour, @"^[0-2][0-9]$"))
                            result = TranslationSource.Instance["ValidationHour"];
                        break;
                    case nameof(SelectedMinute):
                        if (string.IsNullOrWhiteSpace(SelectedMinute))
                            result = TranslationSource.Instance["ValidationMinute"];
                        else if (!Regex.IsMatch(SelectedMinute, @"^[0-5][0-9]$"))
                            result = TranslationSource.Instance["ValidationMinute"];
                        break;
                    case nameof(NumberOfPeople):
                        if (NumberOfPeople <= 0)
                            result = TranslationSource.Instance["ValidationNumberOfPeople"];
                        break;
                    case nameof(SelectedLanguage):
                        if (SelectedLanguage.Key == -1)
                            result = TranslationSource.Instance["ValidationLanguage"];
                        break;
                }

                return result;
            }
        }

        public string Error => null;

        private bool CanReserveGroupDrive(object parameter)
        {
            return Countries.Any(c => c.Value == SelectedCountry.Value) &&
                   Cities.Any(c => c.Value == SelectedCity.Value) &&
                   !string.IsNullOrWhiteSpace(StartAddress) &&
                   !string.IsNullOrWhiteSpace(EndAddress) &&
                   SelectedDate >= DateTime.Today &&
                   !string.IsNullOrWhiteSpace(SelectedHour) &&
                   !string.IsNullOrWhiteSpace(SelectedMinute) &&
                   NumberOfPeople > 0 &&
                   SelectedLanguage.Key != -1;
        }

        private void ValidateAllProperties()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    OnPropertyChanged(property.Name);
                }
            }
        }
    }
}
