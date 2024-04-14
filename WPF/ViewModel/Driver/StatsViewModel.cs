using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class StatsViewModel : INotifyPropertyChanged
    {
        private string _year;
        private List<ReportItem> _reports = new List<ReportItem>();
        public ObservableCollection<ReportItem> ListReport { get; set; }
        private readonly TripRepository tripRepository;
        private readonly DriveReservationService driveReservationRepository;
        private int driverId;

        public string Year
        {
            get { return _year; }
            set
            {
                if (value != _year)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<ReportItem> Reports
        {
            get { return _reports; }
            set
            {
                _reports = value;
                OnPropertyChanged();
            }
        }

        public StatsViewModel(int driverId, DriveReservationService service)
        {
            this.driverId = driverId;
            tripRepository = new TripRepository();
            driveReservationRepository = service;
            ListReport = new ObservableCollection<ReportItem>();
        }

        private void initList(bool yearly, int yearSelected = 2024)
        {
            var tripsDriver = tripRepository.GetAll()
                .Where(t => driveReservationRepository.GetByDriver(driverId).Select(d => d.Id).Contains(t.DriveReservationId)
                            && t.StartTime.Year == yearSelected)
                .ToList();
            Reports.Clear();
            ListReport.Clear();

            IEnumerable<ReportItem> reports = yearly ? new[] { GenerateYearlyReport(tripsDriver, yearSelected) } : GenerateMonthlyReports(tripsDriver, yearSelected);

            foreach (var report in reports)
            {
                Reports.Add(report);
                ListReport.Add(report);
            }
        }



        public ReportItem GenerateYearlyReport(List<Trip> tripsDriver, int year)
        {
            var numberOfRides = tripsDriver.Count;
            var averageDuration = numberOfRides > 0
                ? tripsDriver.Average(t => (t.EndTime?.Subtract(t.StartTime))?.TotalMinutes ?? 0)
                : 0;
            var averagePrice = numberOfRides > 0
                ? tripsDriver.Average(t => t.FinalPrice)
                : 0;

            return new ReportItem
            {
                YearMonth = year.ToString(),
                NumberOfRides = numberOfRides,
                AverageDuration = averageDuration,
                AveragePrice = (double)averagePrice
            };
        }


        public IEnumerable<ReportItem> GenerateMonthlyReports(List<Trip> tripsDriver, int year)
        {
            return Enumerable.Range(1, 12).Select(month =>
            {
                var monthlyTrips = tripsDriver.Where(t => t.StartTime.Month == month).ToList();
                var numberOfRides = monthlyTrips.Count;
                return new ReportItem
                {
                    YearMonth = $"{year} - {month}",
                    NumberOfRides = numberOfRides,
                    AverageDuration = monthlyTrips.Any() ? monthlyTrips.Average(t => (t.EndTime?.Subtract(t.StartTime))?.TotalMinutes ?? 0) : 0,
                    AveragePrice = (double) (monthlyTrips.Any() ? monthlyTrips.Average(t => t.FinalPrice) : 0)
                };
            })
            .Where(r => r.NumberOfRides > 0);
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void btnYearly_Click(object sender, RoutedEventArgs e)
        {
            initList(true, ParseYear());
        }

        public void btnMonthly_Click(object sender, RoutedEventArgs e)
        {
            int year = ParseYear();
            if (year == 0)
            {
                MessageBox.Show("Not correct year entered!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            initList(false, year);
        }

        private int ParseYear()
        {
            if (int.TryParse(Year, out int year))
                return year;
            return 0; // return 0 when parsing fails
        }

    }
    public class ReportItem
    {
        public string YearMonth { get; set; } 
        public int NumberOfRides { get; set; }
        public double? AverageDuration { get; set; } 
        public double AveragePrice { get; set; } 
    }
}
