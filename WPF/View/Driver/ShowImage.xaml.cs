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

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for ShowImage.xaml
    /// </summary>
    public partial class ShowImage : Page
    {
        public List<string> ImagePaths { get; set; }
        public ShowImage(List<Domain.Model.Image> images)
        {
            InitializeComponent();
            DataContext = this;
            ImagePaths = new List<string>();

            foreach (Domain.Model.Image image in images)
            {
                ImagePaths.Add(image.Path);
            }

            ImagesListView.ItemsSource = ImagePaths;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
