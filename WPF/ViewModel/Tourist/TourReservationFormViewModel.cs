using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
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

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourReservationFormViewModel : INotifyPropertyChanged
    {
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


        public ObservableCollection<KeyValuePair<int, string>> StartTimes { get; set; } = new ObservableCollection<KeyValuePair<int, string>>();
        public ObservableCollection<int> NumberOfPeopleOptions { get; set; } = new ObservableCollection<int> { 1, 2, 3 };
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
        }
        private void InitializeServices()
        {
            _tourInstanceService = new TourInstanceService();
            _tourReservationService = new TourReservationService();
            _tourGuestService = new TourGuestService();
            _voucherService = new VoucherService();
        }
        private void FillCollections()
        {
            FillStartTimes();
            FillNumberOfPeopleOptions();
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
        private void FillNumberOfPeopleOptions()
        {
            NumberOfPeopleOptions.Clear();
            NumberOfPeopleOptions.Add(1);
            NumberOfPeopleOptions.Add(2);
            NumberOfPeopleOptions.Add(3);
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
            }
        }
        private void CheckIfFull()
        {
            if (_selectedTourInstance != null && NumberOfPeople > _selectedTourInstance.RemainingSlots)
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
        public void UseVoucher()
        {
            if (SelectedVoucher.HasValue)
            {
                _voucherService.Delete(SelectedVoucher.Value.Key);
                MessageBox.Show($"Voucher expiring on {SelectedVoucher.Value.Value} has been successfully used and deleted.");
                SelectedVoucher = null;
            }
        }
        public List<TourGuest> CollectGuestData()
        {
            return GuestInputs.Select(guest => new TourGuest(guest.FirstName + " " + guest.LastName, guest.Age, _selectedTourInstance.Id, _touristId, 0)).ToList();
        }
        public void UpdateTourCapacity()
        {
            if (_selectedTourInstance != null)
            {
                _selectedTourInstance.RemainingSlots -= NumberOfPeople;
                _tourInstanceService.Update(_selectedTourInstance);
            }
        }
        public void SaveReservation()
        {
            var guests = CollectGuestData();
            if (guests.Any())
            {
                UpdateTourCapacity();
                UseVoucher();
                TourReservation tourReservation = new TourReservation(_selectedTourInstance.Id, _touristId);
                _tourGuestService.SaveMultiple(guests);
                _tourReservationService.Save(tourReservation);
                MessageBox.Show("Reservation successful");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
