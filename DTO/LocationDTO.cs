using BookingApp.Domain.Model;
using System.ComponentModel;

namespace BookingApp.DTO
{
    public class LocationDTO : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                    OnPropertyChanged(nameof(CityCountry));
                }
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                    OnPropertyChanged(nameof(CityCountry));
                }
            }
        }

        public string CityCountry => $"{City}, {Country}"; 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LocationDTO() { }

        public LocationDTO(Location location)
        {
            Id = location.Id;
            City = location.City;
            Country = location.Country;
        }

        public override string ToString()
        {
            return CityCountry;
        }
    }
}
