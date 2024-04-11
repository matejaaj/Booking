using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.Repository;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class TourAttendanceOverviewViewModel
    {
        public ObservableCollection<TourGuest> NotPresentTourists { get; set; }
        public TourGuest SelectedTourist { get; set; }

        private readonly TourGuestRepository _tourGuestRepository;
        private int _checkpointId;

        public TourAttendanceOverviewViewModel(int checkpointId, ObservableCollection<TourGuest> notPresentTourists)
        {
            _checkpointId = checkpointId;
            _tourGuestRepository = new TourGuestRepository();
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
                _tourGuestRepository.Update(SelectedTourist);
                NotPresentTourists.Remove(SelectedTourist);
            }
        }
    }
}
