using BookingApp.Application.UseCases;
using BookingApp.Application;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Windows.Input;
using BookingApp.Commands;

public class ReviewTourViewModel : INotifyPropertyChanged
{
    private TourReviewService _tourReviewService;
    private ImageService _imageService;

    public TourInstanceViewModel SelectedTourInstance { get; set; }
    public int TouristId { get; set; }
    public ObservableCollection<ReviewTourFormViewModel> ReviewForms { get; set; }

    public ICommand AddPictureCommand { get; }
    public ICommand ConfirmCommand { get; }
    public ICommand CancelCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public ReviewTourViewModel(TourInstanceViewModel tour, int touristId)
    {
        TouristId = touristId;
        SelectedTourInstance = tour;

        _tourReviewService = new TourReviewService(Injector.CreateInstance<ITourReviewRepository>());
        _imageService = new ImageService(Injector.CreateInstance<IImageRepository>());

        ReviewForms = new ObservableCollection<ReviewTourFormViewModel>();
        SetReviewForms();

        AddPictureCommand = new RelayCommand(param => AddPicture((ReviewTourFormViewModel)param));
        ConfirmCommand = new RelayCommand(param => SaveReviews());
        CancelCommand = new RelayCommand(param => Cancel());
    }

    private void SetReviewForms()
    {
        foreach (var guest in SelectedTourInstance.Guests.Where(g => g.CheckpointId != 0))
        {
            ReviewForms.Add(new ReviewTourFormViewModel { Guest = guest });
        }
    }

    private void AddPicture(ReviewTourFormViewModel reviewTourFormViewModel)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*",
            Multiselect = true
        };
        if (openFileDialog.ShowDialog() == true)
        {
            foreach (var fileName in openFileDialog.FileNames)
            {
                reviewTourFormViewModel.ImagePaths.Add(fileName);
            }
        }
    }

    private void SaveReviews()
    {
        foreach (var reviewForm in ReviewForms)
        {
            var review = new TourReview(SelectedTourInstance.Id, TouristId, reviewForm.Guest.Id, reviewForm.SelectedRating, reviewForm.Comment, true);
            _tourReviewService.Save(review);
            foreach (var path in reviewForm.ImagePaths)
            {
                var image = new Image(path, review.Id, ImageResourceType.TOUR_REVIEW, TouristId);
                _imageService.Save(image);
            }
        }
    }

    private void Cancel()
    {
        // Implement cancel logic here
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}