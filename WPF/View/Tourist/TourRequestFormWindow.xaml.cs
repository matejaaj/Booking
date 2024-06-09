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
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourRequestFormWindow.xaml
    /// </summary>
    public partial class TourRequestFormWindow : Window
    {
        public TourRequestFormViewModel ViewModel { get; set; }

        public TourRequestFormWindow(User user, LocationService location, LanguageService language, TourRequestService request, TourRequestSegmentService segment, PrivateTourGuestService tourGuest)
        {
            InitializeComponent();
            ViewModel = new TourRequestFormViewModel(user, location, language, request, segment, tourGuest);
            DataContext = ViewModel;
        }

        private void AutoCompleteBox_Loaded(object sender, RoutedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;
            if (autoCompleteBox != null)
            {
                autoCompleteBox.SelectedItem = new KeyValuePair<int,string>();
            }
        }

        private void AutoCompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var autoCompleteBox = sender as AutoCompleteBox;
            if (autoCompleteBox != null && autoCompleteBox.SelectedItem == null)
            {
                var viewModel = autoCompleteBox.DataContext as TourRequestSegmentViewModel;
                if (viewModel != null)
                {
                    if (autoCompleteBox.Name == "autoCompleteCountry")
                    {
                        viewModel.ValidateCountry(autoCompleteBox.Text);
                    }
                    else if (autoCompleteBox.Name == "autoCompleteCity")
                    {
                        viewModel.ValidateCity(autoCompleteBox.Text);
                    }
                }
            }
        }


        
    }
}
