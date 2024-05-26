using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public ObservableCollection<Domain.Model.Image> Images { get; set; }
        private List<Domain.Model.Image> _images;
        private int _entityId;
        private int _userId;
        private ImageResourceType _imageResourceType;
        private ImageService _imageService;

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

        public AddImageViewModel(List<Domain.Model.Image> images, int entityId, ImageResourceType imageResourceType, int userId)
        {
            Images = new ObservableCollection<Domain.Model.Image>(images);
            _images = images;
            _entityId = entityId;
            _imageResourceType = imageResourceType;
            _userId = userId;
            _imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
        }

        public void AddImage()
        {
            if (!string.IsNullOrEmpty(Source))
            {
                Domain.Model.Image newImage = new Domain.Model.Image(_source, _entityId, _imageResourceType, _userId);
                Images.Add(newImage);
                _images.Add(newImage);
                if (_imageService.GetAll().Find(i => i.Path == newImage.Path)==null)
                {
                    _imageService.Save(newImage);
                }
            }
        }
    }
}
