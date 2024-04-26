using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Repository;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TouristTabsViewModel
    {
        public User Tourist { get; }

        public ToursMainTabViewModel ToursMainViewModel { get; }
        public DriveMainTabViewModel DriveMainViewModel { get; }
        
        public MyToursViewModel View { get; }

        public TouristTabsViewModel(User loggedUser)
        {
            Tourist = loggedUser;

            ToursMainViewModel = new ToursMainTabViewModel(loggedUser);

            View = new MyToursViewModel(loggedUser);
        }
    }
}
