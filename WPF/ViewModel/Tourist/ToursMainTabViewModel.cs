using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ToursMainTabViewModel
    {
        public MyToursViewModel MyToursViewModel { get;  }
        public AllToursViewModel AllToursViewModel { get;  }

        public ToursMainTabViewModel(User loggedUser)
        {
            MyToursViewModel = new MyToursViewModel(loggedUser);
        }
        
    }
}
