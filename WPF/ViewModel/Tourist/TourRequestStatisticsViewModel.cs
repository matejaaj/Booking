using BookingApp.Application.UseCases.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;  


namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourStatisticsViewModel : INotifyPropertyChanged
    {
        private string _selectedYear;
        private ObservableCollection<string> _years = new ObservableCollection<string>();

        private readonly TourRequestStatFactory _statFactory;
        private readonly int _userId;

        public TourStatisticsViewModel(int userId, TourRequestService request, TourRequestSegmentService segment)
        {
            _statFactory = new TourRequestStatFactory(request, segment);
            _userId = userId;
            InitializeYears();
            PieChartData = new SeriesCollection();
            SelectedYear = "Sve godine";
        }


        public ObservableCollection<string> Years
        {
            get { return _years; }
            set
            {
                if (_years != value)
                {
                    _years = value;
                    OnPropertyChanged(nameof(Years));
                }
            }
        }


        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    LoadStatsForYear(value);
                    OnPropertyChanged();
                }
            }
        }

        private void InitializeYears()
        {
            Years.Add("Sve godine");

            int currentYear = DateTime.Now.Year;
            int startYear = _statFactory.GetEarliestYear(_userId);

            Years.Clear();
            Years.Add("Sve godine");
            for (int year = startYear; year <= currentYear; year++)
            {
                Years.Add(year.ToString());
            }
        }

        public SeriesCollection PieChartData { get; set; }

        private void LoadStatsForYear(string year)
        {
            TourRequestStat stats;
            if (year == "Sve godine")
            {
                stats = _statFactory.CreateUserStatForAllYears(_userId);
            }
            else
            {
                stats = _statFactory.CreateUserStatForYear(int.Parse(year), _userId);
            }


            var newChartData = new SeriesCollection
            {
                new PieSeries { Title = "Accepted", Values = new ChartValues<int> { stats.AcceptedCount }, DataLabels = true },
                new PieSeries { Title = "Rejected", Values = new ChartValues<int> { stats.RejectedCount }, DataLabels = true }
            };

            PieChartData = newChartData;
            OnPropertyChanged(nameof(PieChartData));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}