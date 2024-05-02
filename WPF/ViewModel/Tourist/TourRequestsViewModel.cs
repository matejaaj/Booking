using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.UseCases;
using BookingApp.DTO;
using BookingApp.WPF.View.Tourist;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class TourRequestsViewModel
    {
        public ObservableCollection<TourRequestDTO> Requests { get; private set; }

        private LocationService _locationService;
        private LanguageService _languageService;
        public TourRequestsViewModel(LocationService locationService, LanguageService languageService)
        {
            _locationService = locationService;
            _languageService = languageService;
        }

        public void OpenWindow()
        {
            var tourRequestFormWindow = new TourRequestFormWindow(_locationService, _languageService);
            tourRequestFormWindow.Show();
        }
        
    }
}
