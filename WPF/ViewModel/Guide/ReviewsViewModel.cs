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
            Reviews = new ObservableCollection<TourReviewDTO>();

            foreach(var review in _tourReviewService.GetAll())
            {
                if(review.TourId == instance.Id)
                    Reviews.Add(new TourReviewDTO(review));
            }
        }
        public void ReportReview()
        {
            if(SelectedReview != null)
            {
                TourReview newReview = _tourReviewService.GetById(SelectedReview.Id);
                newReview.IsValid = false;
                _tourReviewService.Update(newReview);
                MessageBox.Show("Succesfully reported!");
            }
        }
    }
}
