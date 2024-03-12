using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for GuestOverview.xaml
    /// </summary>
    public class GetLocationConverter : IValueConverter
    {
        private readonly LocationRepository _locationRepository = new LocationRepository();

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int locationId)
            {
                return _locationRepository.GetLocationById(locationId);
            }
            return null;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class GuestOverviewViewModel : INotifyPropertyChanged
    {
        private string _searchName;
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
            }
        }

        private string _searchLocation;
        public string SearchLocation
        {
            get { return _searchLocation; }
            set
            {
                _searchLocation = value;
                OnPropertyChanged(nameof(SearchLocation));
            }
        }

        private string _searchType;
        public string SearchType
        {
            get { return _searchType; }
            set
            {
                _searchType = value;
                OnPropertyChanged(nameof(SearchType));
            }
        }

        private int _searchGuestNumber;
        public int SearchGuestNumber
        {
            get { return _searchGuestNumber; }
            set
            {
                _searchGuestNumber = value;
                OnPropertyChanged(nameof(SearchGuestNumber));
            }
        }

        private int _searchReservationDays;
        public int SearchReservationDays
        {
            get { return _searchReservationDays; }
            set
            {
                _searchReservationDays = value;
                OnPropertyChanged(nameof(SearchReservationDays));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class GuestOverview : Window
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public GuestOverviewViewModel ViewModel { get; set; }
        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _repository;
        public GuestOverview(User guest)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = guest;
            _repository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_repository.GetAll());
            ViewModel = new GuestOverviewViewModel();
            ((CollectionViewSource)Resources["FilteredAccommodations"]).Filter += FilterAccommodations;
        }

        private void FilterAccommodations(object sender, FilterEventArgs e)
        {
            Accommodation accommodation = e.Item as Accommodation;
            if (accommodation != null)
            {
                string searchText = ViewModel.SearchName;
                if (searchText != null && searchText.Length > 0)
                {
                    if (accommodation.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        e.Accepted = true;
                        return;
                    }
                }
            }

            e.Accepted = false;
        }

        /* public void SearchingAccommodations()
         {
             if (searchingCriteria)
             {

             }
         }*/

    }
}
