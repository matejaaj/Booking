using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for AddCheckpoint.xaml
    /// </summary>
    public partial class AddCheckpoint : Window
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<BookingApp.Model.Checkpoint> _checkpoints;
        private int _checkpointId;
        private int _tourId;
        public AddCheckpoint(List<Checkpoint> checkpoints, int checkpointId, int tourId)
        {
            InitializeComponent();
            DataContext = this;
            _checkpointId = checkpointId;
            _tourId = tourId;
            _checkpoints = checkpoints;
        }

        private string _name;
        public string CheckpointName
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(CheckpointName))
            {
                Checkpoint newCheckpoint = new Checkpoint(_checkpointId, _name, _tourId);
                _checkpoints.Add(newCheckpoint);

                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Not added", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
