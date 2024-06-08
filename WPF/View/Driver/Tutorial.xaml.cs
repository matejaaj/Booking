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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Driver
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Page
    {
        public Tutorial()
        {
            InitializeComponent();
            videoPlayer.MediaOpened += VideoPlayer_MediaOpened;
        }
        private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            videoPlayer.Position = TimeSpan.Zero;
            videoPlayer.Pause();
            statusTextBlock.Text = "Video Loaded.";
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Play();
            statusTextBlock.Text = "Playing video...";
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Pause();
            statusTextBlock.Text = "Video paused.";
        }

        

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
            videoPlayer.Position = TimeSpan.Zero;
            statusTextBlock.Text = "Video reset.";
        }

        private void videoPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            statusTextBlock.Text = "Media Failed: " + e.ErrorException.Message;
        }

        private void videoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            videoPlayer.Position = TimeSpan.Zero;
            videoPlayer.Pause();
            statusTextBlock.Text = "Media Loaded Successfully.";
        }
    }
}
