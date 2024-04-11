using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class ShowTourInstancesViewModel
    {
        public ObservableCollection<string> TourInstances { get; set; }

        public ShowTourInstancesViewModel(List<TourInstance> instances)
        {
            TourInstances = new ObservableCollection<string>(instances.Select(instance => instance.StartTime.ToString()));
        }
    }
}
