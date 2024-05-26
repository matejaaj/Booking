using BookingApp.Commands;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class OwnerWizardViewModel : INotifyPropertyChanged
    {
        private string _wizardImage = "../../../Resources/Images/wizard1.png"; 
        public string WizardImage
        {
            get { return _wizardImage; }
            set
            {
                _wizardImage = value;
                OnPropertyChanged(nameof(WizardImage));
            }
        }

        private string _buttonContent = "Next"; 
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }

        private Domain.Model.Owner _loggedInOwner;
        private View.Owner.OwnerWizardPage _currentPage;

        public ICommand ButtonCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OwnerWizardViewModel(Domain.Model.Owner owner, View.Owner.OwnerWizardPage ownerWizardPage)
        {
            ButtonCommand = new RelayCommand(ButtonClick);
            _loggedInOwner = owner;
            _currentPage = ownerWizardPage;
        }

        private void ButtonClick(object obj)
        {
            if (WizardImage.Equals("../../../Resources/Images/wizard1.png"))
            {
                WizardImage = "../../../Resources/Images/wizard2.png";
            }else if (WizardImage.Equals("../../../Resources/Images/wizard2.png"))
            {
                WizardImage = "../../../Resources/Images/wizard3.png";
                ButtonContent = "Finish";
            }
            else
            {
                _currentPage.NavigationService.Navigate(new AccommodationsPage(_loggedInOwner));
            }
        }
    }
}
