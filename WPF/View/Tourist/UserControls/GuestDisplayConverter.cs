using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

public class GuestDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var guests = value as IEnumerable<string>;
        if (guests == null)
            return null;

        if (guests.Count() > 3)
        {
            return guests.Take(3).Append("...");
        }
        return guests;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}