using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Driver;
using BookingApp.View.Guest;
using BookingApp.View.Guide;
using BookingApp.View.Owner;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
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
                            
                            break;
                        case Role.DRIVER:
                            DriverOverview driverOverview = new DriverOverview();
                            driverOverview.Show();
                            break;
                        default:
                            CommentsOverview commentsOverview = new CommentsOverview(user);
                            commentsOverview.Show();
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
