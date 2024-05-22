using BookingApp.Application.Events;
using BookingApp.Domain.Model;
using BookingApp.LogicServices.Driver;
using BookingApp.WPF.View.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.Driver
{
    class VacationRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private int _statusId;
        public int StatusId
        {
            get { return _statusId; }
            set
            {
                _statusId = value;
                OnPropertyChanged(nameof(StatusId));
            }
        }
        private ObservableCollection<VacationType> _vacTypes = new ObservableCollection<VacationType>();
        public ObservableCollection<VacationType> VacTypes
        {
            get { return this._vacTypes; }
            set
            {
                _vacTypes = value;
                OnPropertyChanged(nameof(VacTypes));
            }
        }

        private VacationType _selectedType;
        public VacationType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        private int DriverId;
        private readonly VacationService service;
        public VacationRequestViewModel(int driverId) 
        {
            DriverId = driverId;
            service = new VacationService(DriverId, null);
            VacTypes = new ObservableCollection<VacationType>(service.GetTypes());

        }

        public void Button_Confirm(object sender, EventArgs e, Page owner)
        {
            DateOnly timeNow = DateOnly.FromDateTime(DateTime.Now);
            timeNow.AddDays(2);
            bool allowed = false;
            if(DateOnly.FromDateTime(StartDate) > timeNow)
            {
                allowed = service.sendRequest(DateOnly.FromDateTime(StartDate), DateOnly.FromDateTime(EndDate), SelectedType.Id, false);
            }
            else
            {
                allowed = service.sendRequest(DateOnly.FromDateTime(StartDate), DateOnly.FromDateTime(EndDate), SelectedType.Id, true);
                if(allowed)
                {
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("Your vacation is approwed!", "Notification"));
                }
                else
                {
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("Your vacation is dennied!", "Notification"));
                }
                owner.NavigationService.GoBack();
            }
        }
    }
}
