using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerOverview.xaml
    /// </summary>
    public partial class OwnerOverview : Window
    {
        public User LoggedInOwner { get; set; }

        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        public OwnerOverview(User owner)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInOwner = owner;
            _accommodationRepository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetByUser(LoggedInOwner));
        }

        private void ShowCreateAccommodationForm(object sender, RoutedEventArgs e)
        {
            AccommodationForm accommodationForm = new AccommodationForm(LoggedInOwner);
            accommodationForm.Show();
        }

        private void ShowViewAccommodation(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                MessageBox.Show(this, "Please choose an accommodation to view!");
            }
        }
    }
}
