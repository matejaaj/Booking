using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class ShowCheckpointsViewModel
    {
        public ObservableCollection<string> CheckpointNames { get; set; }

        public ShowCheckpointsViewModel(List<Checkpoint> checkpoints)
        {
            CheckpointNames = new ObservableCollection<string>(checkpoints.Select(checkpoint => checkpoint.Name));
        }
    }
}
