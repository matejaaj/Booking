using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TourAttendanceOverviewViewModel
    {
        public ObservableCollection<TourGuest> NotPresentTourists { get; set; }
        public TourGuest SelectedTourist { get; set; }

        private readonly TourGuestService _tourGuestService;
        private int _checkpointId;

        public TourAttendanceOverviewViewModel(int checkpointId, ObservableCollection<TourGuest> notPresentTourists)
        {
            _checkpointId = checkpointId;
            _tourGuestService = new TourGuestService();
            NotPresentTourists = notPresentTourists;
        }

        public void MarkAsPresent()
        {
            if (SelectedTourist == null)
            {
                MessageBox.Show("Select tourist!");
            }
            else
            {
                SelectedTourist.CheckpointId = _checkpointId;
                _tourGuestService.Update(SelectedTourist);
                NotPresentTourists.Remove(SelectedTourist);
            }
        }
    }
}
