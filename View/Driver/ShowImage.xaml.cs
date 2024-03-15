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

namespace BookingApp.View.Driver
{
    /// <summary>
    /// Interaction logic for ShowImage.xaml
    /// </summary>
    public partial class ShowImage : Window
    {
        public List<string> ImagePaths { get; set; }
        public ShowImage(List<BookingApp.Model.Image> images)
        {
            InitializeComponent();
            DataContext = this;
            ImagePaths = new List<string>();

            foreach (BookingApp.Model.Image image in images)
            {
                ImagePaths.Add(image.Path);
            }

            ImagesListView.ItemsSource = ImagePaths;
        }
    }
}
