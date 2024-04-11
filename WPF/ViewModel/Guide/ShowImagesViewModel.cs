using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class ShowImagesViewModel
    {
        public ObservableCollection<string> ImagePaths { get; set; }

        public ShowImagesViewModel(List<Domain.Model.Image> images)
        {
            ImagePaths = new ObservableCollection<string>(images.Select(image => image.Path));
        }
    }
}
