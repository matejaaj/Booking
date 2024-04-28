using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class DriveReservationViewModel : INotifyPropertyChanged
    {
        private int _driveReservationId;
        private string _startAddress;
        private string _endAddress;
        private DateTime _time;
        private string _driver;
        private string _status;
        private double _delayTourist;
        private double _delayDriver;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int DriveReservationId
        {
            get { return _driveReservationId; }
            set
            {
                if (_driveReservationId != value)
                {
                    _driveReservationId = value;
                    OnPropertyChanged(nameof(DriveReservationId));
                }
            }
        }

        public string StartAddress
        {
            get { return _startAddress; }
            set
            {
                if (_startAddress != value)
                {
                    _startAddress = value;
                    OnPropertyChanged(nameof(StartAddress));
                }
            }
        }

        public string EndAddress
        {
            get { return _endAddress; }
            set
            {
                if (_endAddress != value)
                {
                    _endAddress = value;
                    OnPropertyChanged(nameof(EndAddress));
                }
            }
        }

        public DateTime Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        public string Driver
        {
            get { return _driver; }
            set
            {
                if (_driver != value)
                {
                    _driver = value;
                    OnPropertyChanged(nameof(Driver));
                }
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public double DelayTourist
        {
            get { return _delayTourist; }
            set
            {
                if (_delayTourist != value)
                {
                    _delayTourist = value;
                    OnPropertyChanged(nameof(DelayTourist));
                }
            }
        }

        public double DelayDriver
        {
            get { return _delayDriver; }
            set
            {
                if (_delayDriver != value)
                {
                    _delayDriver = value;
                    OnPropertyChanged(nameof(DelayDriver));
                }
            }
        }

        public DriveReservationViewModel()
        {

        }
    }
}
