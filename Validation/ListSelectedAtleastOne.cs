using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Validation
{
    internal class ListSelectedAtleastOne : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value == null)
                {
                    return new ValidationResult(false, "Please select something!");
                }
                var s = value as List<object>;
                if(s?.Count > 0)
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please select something!");
            }
            catch
            {
                return new ValidationResult(false, "An error occured!");
            }
            
            
        }
    }
}
