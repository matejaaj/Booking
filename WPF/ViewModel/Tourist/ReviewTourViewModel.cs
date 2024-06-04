using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Commands;

namespace BookingApp.WPF.ViewModel.Tourist
{
    public class ReviewTourViewModel : INotifyPropertyChanged
    {
        private readonly TourReviewService _tourReviewService;
        private readonly ImageService _imageService;

        public TourInstanceViewModel SelectedTourInstance { get; set; }
        public int TouristId { get; set; }
        public ObservableCollection<ReviewTourFormViewModel> ReviewForms { get; set; }
        public ICommand SubmitCommand { get; }

        public ReviewTourViewModel(TourInstanceViewModel tour, int touristId)
        {
            TouristId = touristId;
            SelectedTourInstance = tour;

            _tourReviewService = new TourReviewService(Injector.CreateInstance<ITourReviewRepository>());
            _imageService = new ImageService(Injector.CreateInstance<IImageRepository>());

            ReviewForms = new ObservableCollection<ReviewTourFormViewModel>();
            SetReviewForms();

            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);
        }

        private void SetReviewForms()
        {
            foreach (var guest in SelectedTourInstance.Guests.Where(g => g.CheckpointId != 0))
            {
                ReviewForms.Add(new ReviewTourFormViewModel { Guest = guest, SelectedRating = 5 });
            }
        }

        public void AddPicture(ReviewTourFormViewModel reviewTourFormViewModel)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*",
                Multiselect = true
            };
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
                TourReview review = new TourReview(SelectedTourInstance.Id, TouristId, reviewForm.Guest.Id, reviewForm.SelectedRating, reviewForm.Comment, true);
                _tourReviewService.Save(review);
                foreach (var path in reviewForm.ImagePaths)
                {
                    Domain.Model.Image image = new Domain.Model.Image(path, review.Id, ImageResourceType.TOUR_REVIEW, TouristId);
                    _imageService.Save(image);
                }
            }
        }

        private bool CanSubmit(object parameter)
        {
            foreach (var form in ReviewForms)
            {
                if (string.IsNullOrWhiteSpace(form.Comment))
                {
                    return false;
                }
            }
            return true;
        }

        private void OnSubmit(object parameter)
        {
            SaveReviews();
            MessageBox.Show(TranslationSource.Instance["ReviewSuccessful"]);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
