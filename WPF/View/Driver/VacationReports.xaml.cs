using BookingApp.Domain.Model;
using BookingApp.LogicServices.Driver;
using BookingApp.WPF.ViewModel.Driver;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Driver
{
   
    public partial class VacationReports : Page
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _yearStart;
        public int YearStart
        {
            get { return _yearStart; }
            set
            {
                _yearStart = value;
                OnPropertyChanged(nameof(YearStart));
            }
        }

        private int _yearEnd;
        public int YearEnd
        {
            get { return _yearEnd; }
            set
            {
                _yearEnd = value;
                OnPropertyChanged(nameof(YearEnd));
            }
        }

        private ObservableCollection<DriverOnVacation> vacations = new ObservableCollection<DriverOnVacation>();
        public ObservableCollection<DriverOnVacation> Vacations
        {
            get { return this.vacations; }
            set
            {
                vacations = value;
                OnPropertyChanged(nameof(Vacations));
            }
        }

        private VacationRequestViewModel VM {  get; set; }
        
        public VacationReports(int driverId)
        {
            VM = ViewModelInjector.GetInstance(driverId, new VacationService(driverId, null));
            InitializeComponent();
            foreach(DriverOnVacation d in VM.getVacationsForDriver())
            {
                vacations.Add(d);
            }
            DataContext = this;
        }

        private void btnGenerate(object sender, RoutedEventArgs e)
        {
            VM.GeneratePDF(vacations.ToList(), YearStart, YearEnd);
        }
    }
}
