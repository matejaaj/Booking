using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Validation
{
    public class OnlyNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value == null)
                {
                    return new ValidationResult(false, "Please enter a valid number.");
                }
                if (value is string)
                {
                    var s = value as string;
                    int r;
                    if (int.TryParse(s, out r))
                    {
                        return new ValidationResult(true, null);
                    }
                    return new ValidationResult(false, "Please enter a valid number.");
                }
                else
                {
                    var s = "" + value;
                    int r;
                    if (int.TryParse(s, out r))
                    {
                        return new ValidationResult(true, null);
                    }
                    return new ValidationResult(false, "Please enter a valid number.");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
