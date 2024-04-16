using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ReviewTourViewModel
    {
        private TourReviewService _tourReviewService;
        private ImageService _imageService;


        public TourInstanceViewModel SelectedTourInstance { get; set; }
        public int TouristId { get; set; }
        public ObservableCollection<ReviewTourFormViewModel> ReviewForms { get; set; }


        public ReviewTourViewModel(TourInstanceViewModel tour, int touristId)
        {
            TouristId = touristId;
            _tourReviewService = new TourReviewService();
            _imageService = new ImageService();
            SelectedTourInstance = tour;
            ReviewForms = new ObservableCollection<ReviewTourFormViewModel>();
            SetReviewForms();
        }
        private void SetReviewForms()
        {
            foreach (var guest in SelectedTourInstance.Guests.Where(g => g.CheckpointId != 0))
            {
                ReviewForms.Add(new ReviewTourFormViewModel { Guest = guest });
            }
        }
        public void AddPicture(ReviewTourFormViewModel reviewTourFormViewModel)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                reviewTourFormViewModel.ImagePaths.Add(openFileDialog.FileName);
            }
        }
        public void RemovePicture(ReviewTourFormViewModel reviewForm, string imagePath)
        {
            if (reviewForm != null && reviewForm.ImagePaths.Contains(imagePath))
            {
                reviewForm.ImagePaths.Remove(imagePath);
            }
        }
        public void SaveReviews()
        {
            foreach (var reviewForm in ReviewForms)
            {
                TourReview review = new TourReview(SelectedTourInstance.Id, TouristId ,reviewForm.Guest.Id, reviewForm.SelectedRating, reviewForm.Comment, true);
                _tourReviewService.Save(review);
                foreach(var path in reviewForm.ImagePaths)
                {
                    Domain.Model.Image image = new Domain.Model.Image(path, review.Id, ImageResourceType.TOUR_REVIEW, TouristId);
                    _imageService.Save(image);
                }
            }
        }


    }
}