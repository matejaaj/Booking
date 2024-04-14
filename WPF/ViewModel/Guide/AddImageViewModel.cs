using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class AddImageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Domain.Model.Image> _images;
        private int _entityId;
        private ImageResourceType _imageResourceType;

        private string _source;
        public string Source
        {
            get => _source;
            set
            {
                if (value != _source)
                {
                    _source = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddImageViewModel(List<Domain.Model.Image> images, int entityId, ImageResourceType imageResourceType)
        {
            _images = images;
            _entityId = entityId;
            _imageResourceType = imageResourceType;
        }

        public void Confirm()
        {
            if (!string.IsNullOrEmpty(Source))
            {
                Domain.Model.Image newImage = new Domain.Model.Image(_source, _entityId, _imageResourceType, -1);
                _images.Add(newImage);
                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Not added", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
