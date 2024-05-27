using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.LogicServices.Driver
{
    public class ViewModelInjector
    {
        static VacationRequestViewModel? VacationRequestViewModel { get; set; }

        public static VacationRequestViewModel GetInstance(int id, VacationService vacService)
        {
            if(VacationRequestViewModel == null)
                return new VacationRequestViewModel(id, vacService);
            return VacationRequestViewModel;
        }
    }
}
