using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class RespondViewModel : INotifyPropertyChanged 
    {
        public static DriveReservation Reservation { get; set; }

        private readonly DriveReservationService driveReservationRepository;

        public event EventHandler ReservationConfirmed;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _time;
        public String Time 
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        
        public RespondViewModel(DriveReservationService driveReservationRepository, DriveReservation driveReservation)
        {
            this.driveReservationRepository = driveReservationRepository;
            Time = driveReservation.DepartureTime.ToString();
            Reservation = driveReservation;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Respond_Accept(object sender, RoutedEventArgs e)
        {
            Reservation.DriveReservationStatusId = 2;
            driveReservationRepository.Update(Reservation);
            ReservationConfirmed.Invoke(this, EventArgs.Empty);

        }
    }
}
