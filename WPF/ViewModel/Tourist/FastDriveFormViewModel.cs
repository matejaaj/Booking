using BookingApp.Application.UseCases;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using BookingApp.Commands;
using System.Text.RegularExpressions;
using System.Linq;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class FastDriveFormViewModel : BaseDriveFormViewModel, IDataErrorInfo
    {
        private readonly DetailedLocationService _detailedLocationService;
        private readonly DriveReservationService _driveReservationService;
        private readonly User _tourist;

        public ICommand CloseWindowCommand { get; }
        public ICommand ReserveCommand { get; }

        public FastDriveFormViewModel(User user, DetailedLocationService detailedLocationService, DriveReservationService driveReservationService, ICommand closeCommand)
        {
            _detailedLocationService = detailedLocationService;
            _driveReservationService = driveReservationService;
            _tourist = user;
            CloseWindowCommand = closeCommand;
            ReserveCommand = new RelayCommand(ReserveFastDrive, CanReserveDrive);

            ValidateAllProperties();
        }

        public void ReserveFastDrive(object parameter)
        {
            DateTime departure = CreateDateTimeFromSelections();

            DetailedLocation start = new DetailedLocation(SelectedCountry.Key, StartAddress);
            DetailedLocation end = new DetailedLocation(SelectedCountry.Key, EndAddress);

            _detailedLocationService.Save(start);
            _detailedLocationService.Save(end);

            DriveReservation reservation = new DriveReservation(start.Id, end.Id, departure, 0, _tourist.Id, 12, 0, 0);
            _driveReservationService.Save(reservation);
            MessageBox.Show(TranslationSource.Instance["FastSuccessful"]);

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
                }
                return result;
            }
        }

        public string Error => null;

        private bool CanReserveDrive(object parameter)
        {
            return Countries.Any(c => c.Value == SelectedCountry.Value) &&
                   Cities.Any(c => c.Value == SelectedCity.Value) &&
                   !string.IsNullOrWhiteSpace(StartAddress) &&
                   !string.IsNullOrWhiteSpace(EndAddress) &&
                   SelectedDate >= DateTime.Today &&
                   !string.IsNullOrWhiteSpace(SelectedHour) &&
                   !string.IsNullOrWhiteSpace(SelectedMinute);
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
