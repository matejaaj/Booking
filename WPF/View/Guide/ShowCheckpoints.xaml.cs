using System;
using System.Collections.Generic;
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
using BookingApp.Domain.Model;

namespace BookingApp.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for ShowCheckpoints.xaml
    /// </summary>
    /// 

    public partial class ShowCheckpoints : Window
    {
        public List<string> CheckpointNames { get; set; }
        public ShowCheckpoints(List<Checkpoint> checkpoints)
        {
            InitializeComponent();
            DataContext = this;
            CheckpointNames = new List<string>();

            foreach(var checkpoint in checkpoints)
            {
                CheckpointNames.Add(checkpoint.Name);
            }

            CheckpointsListView.ItemsSource = CheckpointNames;
        }
    }
}
