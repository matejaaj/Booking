using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookingApp.Application.UseCases.Factories;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourReservationFormViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private TourReservationFactory _tourReservationFactory;
        private int _touristId;
        private TourInstance _selectedTourInstance;
        private TourDTO _selectedTour;
        private KeyValuePair<int, string>? _selectedStartTime;
        private KeyValuePair<int, string>? _selectedVoucher;
        private int _numberOfPeople;
        private TourInstanceService _tourInstanceService;
        private TourReservationService _tourReservationService;
        private TourGuestService _tourGuestService;
        private User _tourist;
        private VoucherService _voucherService;

        public ICommand SubmitCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ObservableCollection<KeyValuePair<int, string>> StartTimes { get; set; } =
            new ObservableCollection<KeyValuePair<int, string>>();

        public ObservableCollection<KeyValuePair<int, string>> Vouchers { get; set; } =
            new ObservableCollection<KeyValuePair<int, string>>();

        public ObservableCollection<TourGuestInputViewModel> GuestInputs { get; } =
            new ObservableCollection<TourGuestInputViewModel>();

        public KeyValuePair<int, string>? SelectedStartTime
        {
            get => _selectedStartTime;
            set
            {
                _selectedStartTime = value;
                OnPropertyChanged(nameof(SelectedStartTime));
                OnStartTimeChanged();
            }
        }

        private bool _isNumberOfPeopleEnabled;

        public bool IsNumberOfPeopleEnabled
        {
            get => _isNumberOfPeopleEnabled;
            set
            {
                _isNumberOfPeopleEnabled = value;
                OnPropertyChanged(nameof(IsNumberOfPeopleEnabled));
            }
        }

        public KeyValuePair<int, string>? SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                _selectedVoucher = value;
                OnPropertyChanged(nameof(SelectedVoucher));
            }
        }

        public int NumberOfPeople
        {
            get => _numberOfPeople;
            set
            {
                if (_numberOfPeople != value)
                {
                    _numberOfPeople = value;
                    OnPropertyChanged(nameof(NumberOfPeople));
                    CheckIfFull();
                }
            }
        }

        public TourReservationFormViewModel(TourDTO selectedTour, User loggedUser)
        {
            _tourist = loggedUser;
            _touristId = loggedUser.Id;
            _selectedTour = selectedTour;
            InitializeServices();
            FillCollections();
            SubmitCommand = new RelayCommand(_ => SaveReservation(), _ => CanSubmit());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void InitializeServices()
        {
            _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(),
                _tourReservationService, _voucherService, _tourGuestService);
            _tourReservationFactory = new TourReservationFactory(_tourGuestService, _tourInstanceService,
                _voucherService, _tourReservationService);
        }

        private void FillCollections()
        {
            FillStartTimes();
            FillVouchers();
        }

        private void FillStartTimes()
        {
            var tourInstances = _tourInstanceService.GetAllByTourId(_selectedTour.Id);
            foreach (var instance in tourInstances.Where(instance => instance.StartTime > DateTime.Now))
            {
                StartTimes.Add(new KeyValuePair<int, string>(instance.Id,
                    instance.StartTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)));

            }
        }

        private void FillVouchers()
        {
            var vouchers = _voucherService.GetVouchersByTouristId(_touristId);
            foreach (var voucher in vouchers)
            {
                var prefix = TranslationSource.Instance["VoucherUntil"];
                Vouchers.Add(new KeyValuePair<int, string>(voucher.Id, prefix + $"{voucher.ExpiryDate:dd.MM.yyyy}"));
            }
        }

        private void OnStartTimeChanged()
        {
            if (_selectedStartTime.HasValue)
            {
                var selectedId = _selectedStartTime.Value.Key;
                _selectedTourInstance = _tourInstanceService.GetById(selectedId);
                NumberOfPeople = 0;
                OnPropertyChanged(nameof(NumberOfPeople));
            }

            IsNumberOfPeopleEnabled = _selectedStartTime.HasValue;
        }

        public void CheckIfFull()
        {


            if (!_tourInstanceService.CheckAvailability(_selectedTourInstance.Id, NumberOfPeople))
            {
                string message = string.Format(TranslationSource.Instance["InsufficientSpots"], _selectedTourInstance.RemainingSlots);
                MessageBox.Show(message);
                NumberOfPeople = 0;
            }
            else
            {
                GenerateInputFields(NumberOfPeople);
            }
        }


        private void GenerateInputFields(int numberOfPeople)
        {
            GuestInputs.Clear();
            for (int i = 0; i < numberOfPeople; i++)
            {
                GuestInputs.Add(new TourGuestInputViewModel());
            }
        }

        public List<TourGuest> CollectGuestData()
        {
            return GuestInputs.Select(guest => new TourGuest(guest.FirstName + " " + guest.LastName, guest.Age,
                _selectedTourInstance.Id, _touristId, 0)).ToList();

        }

        public void SaveReservation()
        {
            var guests = CollectGuestData();
            if (!guests.Any()) return;

            int voucherId = -1;
            if (SelectedVoucher.HasValue)
            {
                voucherId = SelectedVoucher.Value.Key;
                SelectedVoucher = null;
            }

            _tourReservationFactory.CreateTourReservation(guests, voucherId, _selectedTourInstance.Id, NumberOfPeople, _touristId);
            MessageBox.Show(TranslationSource.Instance["ReservationSuccessful"]);


            PDFTourReservationGenerator pdfGenerator = new PDFTourReservationGenerator();
            pdfGenerator.GenerateAndSavePdf(guests, voucherId, NumberOfPeople, _tourist.Username, _selectedTour, _selectedStartTime );

            Cancel();
        }


        private void Cancel()
        {
            System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this)
                ?.Close();
        }

        private bool CanSubmit()
        {
            return _selectedTourInstance != null && NumberOfPeople > 0 && GuestInputs.All(guest =>
                !string.IsNullOrWhiteSpace(guest.FirstName) && !string.IsNullOrWhiteSpace(guest.LastName) &&
                guest.Age > 0);
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case nameof(SelectedStartTime):
                        if (!SelectedStartTime.HasValue)
                            result = TranslationSource.Instance["ValidationStartTime"];
                        break;
                    case nameof(NumberOfPeople):
                        if (!SelectedStartTime.HasValue)
                            result = TranslationSource.Instance["ValidationNumberOfPeopleStartTime"];
                        else if (NumberOfPeople <= 0)
                            result = TranslationSource.Instance["ValidationNumberOfPeople"];
                        break;
                }

                if (GuestInputs != null)
                {
                    foreach (var guest in GuestInputs)
                    {
                        result = guest[columnName];
                        if (!string.IsNullOrEmpty(result))
                            break;
                    }
                }

                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}