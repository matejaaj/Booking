using BookingApp.Application.Events;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class ViewDriveViewModel: INotifyPropertyChanged
    {
        private bool _selectedOption;
        public bool SelectedAtLocation
        {
            get { return _selectedOption; }
            set
            {
                if (_selectedOption != value)
                {
                    _selectedOption = value;
                    OnPropertyChanged(nameof(SelectedAtLocation));
                }
            }
        }

        private bool _selectedDeleyed;
        public bool SelectedDeleyed
        {
            get { return _selectedDeleyed; }
            set
            {
                if (_selectedDeleyed != value)
                {
                    _selectedDeleyed = value;
                    OnPropertyChanged(nameof(SelectedDeleyed));
                }
            }
        }

        private string _txtDelayMinutes;
        public string TxtDelayMinutes
        {
            get { return _txtDelayMinutes; }
            set
            {
                if (_txtDelayMinutes != value)
                {
                    _txtDelayMinutes = value;
                    OnPropertyChanged(nameof(TxtDelayMinutes));
                }
            }
        }


        public bool IsAtLocation => SelectedAtLocation == true;
        public DriveReservation reservation { get; set; }
        public DriveReservationService Repo { get; set; }
        public event EventHandler ReservationConfirmed;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAtLocation)
            {
                int delayMinutes = int.Parse(TxtDelayMinutes);
                if (delayMinutes < 0)
                {
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("Can't put negative delay!", "Error"));
                    return;
                }
                reservation.DelayMinutesDriver = delayMinutes;
            }
            else
            {
                reservation.DelayMinutesDriver = -1;
            }

            Repo.Update(reservation);
            ReservationConfirmed?.Invoke(this, EventArgs.Empty);
        }


        public void rbAtLocation_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
