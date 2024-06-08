using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Validation
{
    public class DateRangeValidationRule : ValidationRule
    {
        public DateTime MinDate { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            MinDate = DateTime.Now;
            if (value is DateTime date)
            {
                if (date < MinDate)
                {
                    return new ValidationResult(false, $"Date must be after {MinDate}.");
                }
                else
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "Invalid date format.");
        }
    }
}
