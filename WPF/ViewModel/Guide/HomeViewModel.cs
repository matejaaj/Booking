using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Application;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View.Owner;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class HomeViewModel
    {
        private TourInstanceService _instanceService;
        private UserService _userService;
        private User user { get; set; }
        private GuideMainWindow mainWindow { get; set; }


        public HomeViewModel(User user, GuideMainWindow mainWindow)
        {
            InitializeServices();
            this.user = user;
            this.mainWindow = mainWindow;
        }

        private void InitializeServices()
        {
            var _voucherService = new VoucherService(Injector.CreateInstance<IVoucherRepository>());
            var _tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>());
            var _tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            _instanceService = new TourInstanceService(Injector.CreateInstance<ITourInstanceRepository>(), _tourReservationService, _voucherService, _tourGuestService);
            _userService = new UserService(Injector.CreateInstance<IUserRepository>());
        }

        public void QuitJob()
        {
            _instanceService.QuitJob(user);
            _userService.Delete(user);
            SignOut();
        }

        public void SignOut()
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            mainWindow.Close();
        }

    }
}
