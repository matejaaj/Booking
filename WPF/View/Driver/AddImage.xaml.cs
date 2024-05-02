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

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for AddImage.xaml
    /// </summary>
    public partial class AddImage : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Domain.Model.Image> _images;
        private int _imageId;
        private int _vehicleId;
        private ImageResourceType _type;
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

        public AddImage(List<Domain.Model.Image> images, int vehicleId,ImageResourceType type)
        {
            InitializeComponent();
            DataContext = this;
            _images = images;
            _vehicleId = vehicleId;
            _type = type;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Source))
            {
                Domain.Model.Image newImage = new BookingApp.Domain.Model.Image(_source, _vehicleId, _type, -1);
                _images.Add(newImage);
                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Not added", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            NavigationService.GoBack();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

