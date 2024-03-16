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
    public partial class ActiveTourOverview : Window
    {   
        public  ObservableCollection<Checkpoint> NotVisitedCheckpoints { get; set; }
        public  ObservableCollection<Checkpoint> VisitedCheckpoints { get; set; }

        private List<Checkpoint> allCheckpoints;
        public Checkpoint SelectedCheckpoint { get; set; }

        private readonly CheckpointRepository _checkpointRepository;
        public ActiveTourOverview(int tourId)
        {
            InitializeComponent();
            DataContext = this;
            _checkpointRepository = new CheckpointRepository();
            allCheckpoints = _checkpointRepository.GetAll();
            NotVisitedCheckpoints = new ObservableCollection<Checkpoint>();
            VisitedCheckpoints = new ObservableCollection<Checkpoint>();
            foreach (var checkpoint in allCheckpoints)
            {
                if (checkpoint.TourId == tourId)
                    NotVisitedCheckpoints.Add(checkpoint);
            }
            var firstCheckpoint = NotVisitedCheckpoints.First();
            VisitedCheckpoints.Add(firstCheckpoint);
            NotVisitedCheckpoints.Remove(firstCheckpoint);
        }

        private void btnMarkAsVisited_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
