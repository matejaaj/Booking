using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class DriveOverviewViewModel : INotifyPropertyChanged
    {
        public Boolean enabled;
        public event EventHandler Finished;

        public DriveReservation Reservation { get; set; }
        public DispatcherTimer Timer { get; set; }
        private TripRepository tripRepository { get; set; }
        private DriveReservationService driveReservationRepository { get; set; }
        private Trip trip { get; set; }

        public DriveOverviewViewModel(DriveReservationService service)
        {
            Price = 0;
            enabled = true;
            Timer = new DispatcherTimer();
            Timer.Interval = System.TimeSpan.Parse("00:00:01");
            Timer.Tick += Timer_Tick;
            tripRepository = new TripRepository();
            trip = new Trip();
            trip.Status = TripStatus.DriverArrived;
            driveReservationRepository = service;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _startingPrice;
        public int StartingPrice
        {
            get => _startingPrice;
            set
            {
                if (value != _startingPrice)
                {
                    _startingPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _price;
        public int Price
        {
            get => _price;
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        public void btnStart(object sender, RoutedEventArgs e)
        {
            if (StartingPrice != null && StartingPrice > 0 && enabled)
            {
                Price = StartingPrice;
                enabled = false;
                Timer.Start();
                trip.StartTime = System.DateTime.Now;
                trip.DriveReservationId = Reservation.Id;
                trip.Status = TripStatus.TripStarted;

            }
            else
            {
                MessageBox.Show("Error!");
            }
        }



        public void btnFinish(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            trip.StartPrice = StartingPrice;
            trip.FinalPrice = Price;
            trip.EndTime = System.DateTime.Now;
            trip.Status = TripStatus.TripEnded;
            tripRepository.Save(trip);
            MessageBox.Show(String.Format("Total price for this ride is: {0}", Price), "Drive");
            Reservation.DriveReservationStatusId = 6;
            driveReservationRepository.Update(Reservation);
            Finished?.Invoke(this, EventArgs.Empty);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Price += 5;
        }
    }
}
