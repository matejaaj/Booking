using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class MyTourMoreDetailsViewModel
    {
        public TourInstanceViewModel TourInstance { get; set; }
        public MyTourMoreDetailsViewModel(TourInstanceViewModel tourInstance)
        {
            TourInstance = tourInstance;
        }
    }
}
