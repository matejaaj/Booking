using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        private string _year;
        private List<ReportItem> _reports = new List<ReportItem>();
        public ObservableCollection<ReportItem> ListReport { get; set; }
        private readonly TripRepository tripRepository;
        private readonly DriveReservationRepository driveReservationRepository;
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
        public Stats(int driverId)
        {
            InitializeComponent();
            this.driverId = driverId;
            tripRepository = new TripRepository();
            driveReservationRepository = new DriveReservationRepository();
            ListReport = new ObservableCollection<ReportItem>();
            DataContext = this;
        }

        private void initList(TripRepository tripRepository, DriveReservationRepository driveReservationRepository, bool yearly, int yearSelected = 2024)
        {
            var tripsDriver = tripRepository.GetAll()
                .Where(t => driveReservationRepository.GetByDriver(driverId).Select(d => d.Id).Contains(t.DriveReservationId)
                            && t.StartTime.Year == yearSelected)
                .ToList();
            Reports.Clear();
            ListReport.Clear();
            if (yearly)
            {
                var yearReport = GenerateYearlyReport(tripsDriver, yearSelected);
                Reports.Add(yearReport);
                ListReport.Add(yearReport);
            }
            else
            {
                var monthlyReports = GenerateMonthlyReports(tripsDriver, yearSelected);
                foreach (var report in monthlyReports)
                {
                    Reports.Add(report);
                    ListReport.Add(report);
                }
            }
        }

        private ReportItem GenerateYearlyReport(List<Trip> tripsDriver, int year)
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
                AveragePrice = (double) averagePrice
            };
        }


        private IEnumerable<ReportItem> GenerateMonthlyReports(List<Trip> tripsDriver, int year)
        {
            return Enumerable.Range(1, 12).Select(month =>
            {
                var monthlyTrips = tripsDriver.Where(t => t.StartTime.Month == month).ToList();
                var numberOfRides = monthlyTrips.Count;
                var averageDuration = numberOfRides > 0 ? monthlyTrips.Average(t => (t.EndTime?.Subtract(t.StartTime))?.TotalMinutes ?? 0) : 0;
                var averagePrice = numberOfRides > 0 ? monthlyTrips.Average(t => t.FinalPrice) : 0;

                return new ReportItem
                {
                    YearMonth = $"{year} - {month}",
                    NumberOfRides = numberOfRides,
                    AverageDuration = averageDuration,
                    AveragePrice = (double) averagePrice
                };
            })
            .Where(r => r.NumberOfRides > 0);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnYearly_Click(object sender, RoutedEventArgs e)
        {
            int y = 0;
            if (Year != null && !Year.Equals("") && int.TryParse(Year, out y))
                initList(tripRepository, driveReservationRepository, true, y);
            else
                initList(tripRepository, driveReservationRepository, true, DateTime.Now.Year);
        }

        private void btnMonthly_Click(object sender, RoutedEventArgs e)
        {
            int y = 0;
            if(Year != null && !Year.Equals("") && int.TryParse(Year, out y))
                initList(tripRepository, driveReservationRepository, false, y);
            else
                MessageBox.Show("Not correct year entered!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    public class ReportItem
    {
        public string YearMonth { get; set; } // Represents either a year or a month within a year
        public int NumberOfRides { get; set; }
        public double? AverageDuration { get; set; } // Consider the appropriate data type
        public double AveragePrice { get; set; } // Consider the appropriate data type
    }
}
