using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Domain.Model.Owner LoggedInOwner { get; set; }
        private string pageName { get; set; }
        public string PageName
        {
            get { return pageName; }
            set
            {
                pageName = value;
                OnPropertyChanged(nameof(PageName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static System.Windows.Controls.Frame _mainFrame;

        public MainWindowViewModel(Domain.Model.Owner owner, System.Windows.Controls.Frame mainFrame)
        {
            LoggedInOwner = owner;
            PageName = "Accommodations";
            mainFrame.Navigate(new AccommodationsPage(owner));
            _mainFrame = mainFrame;
        }

/*        private void ShowRatings_Click(object sender, RoutedEventArgs e)
        {
            PageName = "Ratings";
            ViewRatingsPage page = new ViewRatingsPage(LoggedInOwner);
            MainFrame.Navigate(page);
            SideMenu.Visibility = Visibility.Collapsed;
        }*/
    }
}
