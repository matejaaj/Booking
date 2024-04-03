using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.Driver;
using BookingApp.WPF.View.Guest;
using BookingApp.WPF.View.Guide;
using BookingApp.WPF.View.Owner;
using BookingApp.View.Tourist;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    switch (user.Role)
                    {
                        case Role.OWNER:
                            OwnerOverview ownerOverview = new OwnerOverview(user);
                            ownerOverview.Show();
                            break;
                        case Role.GUEST:
                            GuestOverview guestOverview = new GuestOverview(user);
                            guestOverview.Show();
                            break;
                        case Role.GUIDE:
                            GuideOverview guideOverview = new GuideOverview();
                            guideOverview.Show();
                            break;
                        case Role.TOURIST:
                            TouristMainWindow touristMainWindow = new TouristMainWindow(user);
                            touristMainWindow.Show();
                            break;
                        case Role.DRIVER:
                            DriverOverview driverOverview = new DriverOverview(user);
                            driverOverview.Show();
                            break;
                        default:
                            break;
                    }
                    Close();
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
