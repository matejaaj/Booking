using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System.Windows;
using BookingApp.Application;
using GalaSoft.MvvmLight.Command;

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

        private readonly DetailedLocationService _detailedLocationService;
        private readonly DriveReservationService _driveReservationService;
        private readonly User _tourist;
        private readonly LanguageService _languageService;
        private readonly GroupDriveReservationService _groupDriveReservationService;
        public ICommand CloseWindowCommand { get; }
        public ICommand ReserveCommand { get;  }

        public GroupDriveFormViewModel(User user, DetailedLocationService detailedLocationService, DriveReservationService driveReservationService, ICommand closeCommand)
        {
            _tourist = user;
            _detailedLocationService = detailedLocationService;
            _driveReservationService = driveReservationService;
            _languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            _groupDriveReservationService = new GroupDriveReservationService(Injector.CreateInstance<IGroupDriveReservationRepository>());
            CloseWindowCommand = closeCommand;
            ReserveCommand = new BookingApp.Commands.RelayCommand(ReserveGroupDrive);


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
            MessageBox.Show("Group drive reservation successful!");

            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}