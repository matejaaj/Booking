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

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for AddStartDate.xaml
    /// </summary>
    public partial class AddStartDate : Window
    {
        private List<TourInstance> _startDates;
        private int _tourId;
        private int _capacity;
        public AddStartDate(List<TourInstance> startDates, int tourId, int capacity)
        {
            InitializeComponent();
            DataContext = this;
            LoadTimeComboboxes();

            _tourId = tourId;
            _capacity = capacity;
            _startDates = startDates;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (startDatePicker.SelectedDate.HasValue && startHour.SelectedItem != null && startMinute.SelectedValue != null)
            {
                DateTime startDate = startDatePicker.SelectedDate.Value;
                int hour = int.Parse(startHour.SelectedItem.ToString());
                int minute = int.Parse(startMinute.SelectedItem.ToString());
                startDate = startDate.AddHours(hour).AddMinutes(minute);

                TourInstance newStartDate = new TourInstance(_tourId, _capacity, startDate);

                _startDates.Add(newStartDate);

                MessageBox.Show("Successfully added.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Not added", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadTimeComboboxes()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                startHour.Items.Add(hour.ToString("00"));
            }

            for (int minute = 0; minute < 60; minute++)
            {
                startMinute.Items.Add(minute.ToString("00"));
            }

            startHour.SelectedIndex = 0;
            startMinute.SelectedIndex = 0;
        }
    }
}
