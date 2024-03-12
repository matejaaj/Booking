using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.View.Guest
{
    public class LocationConverter : IValueConverter
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
}
