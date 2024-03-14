using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.View.Driver
{
    public class LocationConverterDriver : IValueConverter
    {
        public DetailLocationRepository _detailedLocationRepository = new DetailLocationRepository();
        public LocationRepository _locationRepository = new LocationRepository();
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            // Proveravamo da li je 'value' tipa int i nije null
            if (value is int detailLocationId)
            {
                // Koristimo ID da dohvatimo detaljnu lokaciju iz odgovarajućeg repozitorijuma
                DetailedLocation detailedLocation = _detailedLocationRepository.GetDetailedLocationById(detailLocationId);

                if (detailedLocation != null)
                {
                    // Pronalazimo lokaciju koristeći LocationId iz DetailedLocation
                    Location location = _locationRepository.GetLocationById(detailedLocation.LocationId);
                    if (location != null)
                    {
                        // Formiramo i vraćamo string sa gradom, državom i adresom
                        return $"{location.City}, {location.Country}, {detailedLocation.Address}";
                    }
                }
            }
            // Vraćamo opšti opis ako lokacija nije pronađena
            return "Nepoznata Lokacija";
        }



        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

       

        
    }

}