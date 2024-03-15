using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Xml.Linq;

namespace BookingApp.View.Owner
{
    /// <summary>
    /// Interaction logic for GuestRatingForm.xaml
    /// </summary>
    public partial class GuestRatingForm : Window
    {
        private AccommodationReservation _reservation;
        private static GuestRatingRepository _repository { get; set; }
        private static AccommodationReservationRepository _accommodationReservationRepository { get; set; }

        private int _cleanliness;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _rulesRespect;
        public int RulesRespect
        {
            get => _rulesRespect;
            set
            {
                if (value != _rulesRespect)
                {
                    _rulesRespect = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public GuestRatingForm(AccommodationReservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            _repository = new GuestRatingRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _reservation = reservation;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                GuestRating newGuestRating = new GuestRating(_reservation.ReservationId, Cleanliness, RulesRespect, Comment);
                _repository.Save(newGuestRating);
                _reservation.IsRated = true;
                _accommodationReservationRepository.Update(_reservation);
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateFields()
        {
            if(string.IsNullOrWhiteSpace(txtCleanliness.Text) ||
                   string.IsNullOrWhiteSpace(txtRulesRespect.Text) ||
                   string.IsNullOrWhiteSpace(txtComment.Text))
            {
                MessageBox.Show("Please fill all fields",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }else if(Cleanliness > 5 || Cleanliness < 1 || RulesRespect > 5 || RulesRespect < 1)
            {
                MessageBox.Show("Please enter ratings between 1 and 5",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
