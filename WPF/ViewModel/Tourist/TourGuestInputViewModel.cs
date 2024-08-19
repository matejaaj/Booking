using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourGuestInputViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _firstName;
        private string _lastName;
        private int _age;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                            result = TranslationSource.Instance["ValidationFirstNameRequired"];
                        else if (!Regex.IsMatch(FirstName, @"^[a-zA-Z]+$"))
                            result = TranslationSource.Instance["ValidationFirstNameFormat"];
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                            result = TranslationSource.Instance["ValidationFirstNameRequired"];
                        else if (!Regex.IsMatch(LastName, @"^[a-zA-Z]+$"))
                            result = TranslationSource.Instance["ValidationFirstNameFormat"];
                        break;
                    case nameof(Age):
                        if (Age <= 0)
                            result = TranslationSource.Instance["ValidationAgeFormat"];
                        break;
                }
                return result;
            }
        }

        public string Error => null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
