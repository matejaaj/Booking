using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Application.UseCases.Factories;
using System.Windows.Input;
using BookingApp.Commands;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourReservationFormViewModel : INotifyPropertyChanged
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
        private VoucherService _voucherService;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ObservableCollection<KeyValuePair<int, string>> StartTimes { get; set; } = new ObservableCollection<KeyValuePair<int, string>>();
        public ObservableCollection<KeyValuePair<int, string>> Vouchers { get; set; } = new ObservableCollection<KeyValuePair<int, string>>();
        public ObservableCollection<TourGuestInputViewModel> GuestInputs { get; } = new ObservableCollection<TourGuestInputViewModel>();


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
            _touristId = loggedUser.Id;
            _selectedTour = selectedTour;
            InitializeServices();
            FillCollections();
            SubmitCommand = new RelayCommand(_ => SaveReservation(), _ => CanSubmit());
            CancelCommand = new RelayCommand(_ => Cancel());
        }
        private void InitializeServices()
        {
            VoucherService voucher = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _tourInstanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);

            _tourReservationFactory = new TourReservationFactory(_tourGuestService, _tourInstanceService, _voucherService, _tourReservationService);
        }
        private void FillCollections()
        {
            FillStartTimes();
            FillVouchers();
        }
        private void FillStartTimes()
        {
            var tourInstances = _tourInstanceService.GetAllByTourId(_selectedTour.Id);
            foreach (var instance in tourInstances)
            {
                StartTimes.Add(new KeyValuePair<int, string>(instance.Id, instance.StartTime.ToString("g", CultureInfo.InvariantCulture)));
            }
        }

        private void FillVouchers()
        {
            var vouchers = _voucherService.GetVouchersByTouristId(_touristId);
            foreach (var voucher in vouchers)
            {
                Vouchers.Add(new KeyValuePair<int, string>(voucher.Id, $"važi do {voucher.ExpiryDate:dd.MM.yyyy}"));
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


        private void CheckIfFull()
        {
            if(_selectedTourInstance == null)
            {
                MessageBox.Show("Select time first");
                return;
            }

            if (!_tourInstanceService.CheckAvailability(_selectedTourInstance.Id, NumberOfPeople))
            {
                MessageBox.Show($"Insufficient spots available for the selected tour. Remaining spots for the selected date: {_selectedTourInstance.RemainingSlots}.");
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
            return GuestInputs.Select(guest => new TourGuest(guest.FirstName + " " + guest.LastName, guest.Age, _selectedTourInstance.Id, _touristId, 0)).ToList();
        }

        public void SaveReservation()
        {
            var guests = CollectGuestData();
            if(!guests.Any()) return;

            int voucherId = -1;
            if (SelectedVoucher.HasValue)
            {
                voucherId = SelectedVoucher.Value.Key;
                SelectedVoucher = null;
            }

            _tourReservationFactory.CreateTourReservation(guests, voucherId, _selectedTourInstance.Id, NumberOfPeople, _touristId);
            MessageBox.Show("Reservation successful");
 
        }

        private void Cancel()
        {
            // Logic to close the window
            System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this)?.Close();
        }

        private bool CanSubmit()
        {
            // Logic to determine if the submit action can be executed
            return _selectedTourInstance != null && NumberOfPeople > 0 && GuestInputs.All(guest => !string.IsNullOrWhiteSpace(guest.FirstName) && !string.IsNullOrWhiteSpace(guest.LastName) && guest.Age > 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
