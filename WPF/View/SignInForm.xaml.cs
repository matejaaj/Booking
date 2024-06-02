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
using BookingApp.Application.UseCases;
using Accessibility;
using BookingApp.WPF.View.Tourist;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;
        private readonly OwnerService _ownerService;

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
            _ownerService = new OwnerService(Injector.CreateInstance<IOwnerRepository>());
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
                            Domain.Model.Owner owner = _ownerService.GetByUsername(Username);
                            //OwnerOverview ownerOverview = new OwnerOverview(owner);
                            //ownerOverview.Show();
                            OwnerMainWindow ownerMainWindow = new OwnerMainWindow(owner);
                            ownerMainWindow.Show();
                            break;
                        case Role.GUEST:
                            GuestMainWindow guestOverview = new GuestMainWindow(user);
                            guestOverview.Show();
                            break;
                        case Role.GUIDE:
                            //GuideOverview guideOverview = new GuideOverview();
                            //guideOverview.Show();
                            GuideMainWindow mainWindow = new GuideMainWindow(user);
                            mainWindow.Show();
                            break;
                        case Role.TOURIST:
                            TouristTabsWindow touristMainWindow = new TouristTabsWindow(user);
                            touristMainWindow.Show();
                            break;
                        case Role.DRIVER:
                            MainWindow mainDriverWindow = new MainWindow(user);
                            mainDriverWindow.Show();
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
