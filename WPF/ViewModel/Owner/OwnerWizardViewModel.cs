using BookingApp.Commands;
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

        private View.Owner.OwnerWizardWindow _ownerWizardWindow;

        public ICommand ButtonCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OwnerWizardViewModel(View.Owner.OwnerWizardWindow ownerWizardWindow)
        {
            ButtonCommand = new RelayCommand(ButtonClick);
            _ownerWizardWindow = ownerWizardWindow;
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
               _ownerWizardWindow.Close();
            }
        }
    }
}
