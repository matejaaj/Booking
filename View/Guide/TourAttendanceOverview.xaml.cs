using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Model;
using BookingApp.Repository;

namespace BookingApp.View.Guide
{
    public partial class TourAttendanceOverview : Window
    {
        public ObservableCollection<TourGuest> NotPresentTourists { get; set; }
        public TourGuest SelectedTourist { get; set; }
        private List<TourGuest> tourGuests { get; set; }

        private TourGuestRepository _tourGuestRpository;
        private int _checkpointId;

        public TourAttendanceOverview(int checkpointId, ObservableCollection<TourGuest> notPresentTourists)
        {
            InitializeComponent();
            DataContext = this;
            _checkpointId = checkpointId;

            _tourGuestRpository = new TourGuestRepository();

            NotPresentTourists = notPresentTourists;
        }

        private void btnMarkAsPresent_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTourist == null)
            {
                MessageBox.Show("Select tourist!");
            }
            else
            {
                SelectedTourist.CheckpointId = _checkpointId;
                _tourGuestRpository.Update(SelectedTourist);
                NotPresentTourists.Remove(SelectedTourist);
            }
        }
    }
}
