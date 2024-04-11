using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class AddCheckpointViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Checkpoint> _checkpoints;
        private int _tourId;

        public AddCheckpointViewModel(List<Checkpoint> checkpoints, int tourId)
        {
            _checkpoints = checkpoints;
            _tourId = tourId;
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

        public void Confirm()
        {
            if (!string.IsNullOrEmpty(CheckpointName))
            {
                Checkpoint newCheckpoint = new Checkpoint(_name, _tourId);
                _checkpoints.Add(newCheckpoint);
                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Not added", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
