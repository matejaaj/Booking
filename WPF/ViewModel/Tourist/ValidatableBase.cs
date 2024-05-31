using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ValidationBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public string Error => null;

        protected virtual string Validate(string propertyName)
        {
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
