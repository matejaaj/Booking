using BookingApp.Model;
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

namespace BookingApp.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window
    {
        public Tour SelectedTour { get; set; }

        public TourReservationWindow(Tour selectedTour)
        {
            SelectedTour = selectedTour;
            InitializeComponent();
        }




        private void cmbNumberOfPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spPersonInputs.Children.Clear(); // Čisti prethodne TextBox-ove

            if (cmbNumberOfPersons.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Content.ToString(), out int numberOfPersons))
                {
                    for (int i = 0; i < numberOfPersons; i++)
                    {

                        var txtFirstName = new TextBox { Margin = new Thickness(0, 0, 0, 10)};
                        spPersonInputs.Children.Add(txtFirstName);

                        // Kreiranje TextBox-a za prezime
                        var txtLastName = new TextBox { Margin = new Thickness(0, 0, 0, 20)};
                        spPersonInputs.Children.Add(txtLastName);
                    }
                }
            }
        }
    }
}
