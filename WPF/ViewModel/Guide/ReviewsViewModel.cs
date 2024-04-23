using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.Guide
{
    internal class ReviewsViewModel : INotifyPropertyChanged
    {
        private readonly TourReviewService _tourReviewService;
        public ObservableCollection<TourReviewDTO> Reviews { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private TourReviewDTO _selectedReview;
        public TourReviewDTO SelectedReview
        {
            get => _selectedReview;
            set
            {
                if(_selectedReview != value)
                {
                    _selectedReview = value;
                    OnPropertyChanged(nameof(SelectedReview));
                }
            }
        }
        public ReviewsViewModel(TourInstance instance)
        {
            _tourReviewService = new TourReviewService();
            Reviews = new ObservableCollection<TourReviewDTO>(_tourReviewService.GetAllByTourInstanceId(instance.Id));
        }
        public void ReportReview()
        {
            if(SelectedReview != null)
            {
                _tourReviewService.ReportReview(SelectedReview.Id);
                MessageBox.Show("Succesfully reported!");
            }
        }
    }
}
