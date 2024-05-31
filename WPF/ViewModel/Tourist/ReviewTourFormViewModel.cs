using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using BookingApp.Commands;
using BookingApp.Domain.Model;

public class ReviewTourFormViewModel : INotifyPropertyChanged
{
    private int _selectedRating;
    private string _comment;

    public TourGuest Guest { get; set; }
    public ObservableCollection<string> ImagePaths { get; set; } = new ObservableCollection<string>();

    public int SelectedRating
    {
        get => _selectedRating;
        set
        {
            if (_selectedRating != value)
            {
                _selectedRating = value;
                OnPropertyChanged(nameof(SelectedRating));
            }
        }
    }

    public bool IsRating1
    {
        get => SelectedRating == 1;
        set
        {
            if (value) SelectedRating = 1;
        }
    }

    public bool IsRating2
    {
        get => SelectedRating == 2;
        set
        {
            if (value) SelectedRating = 2;
        }
    }

    public bool IsRating3
    {
        get => SelectedRating == 3;
        set
        {
            if (value) SelectedRating = 3;
        }
    }

    public bool IsRating4
    {
        get => SelectedRating == 4;
        set
        {
            if (value) SelectedRating = 4;
        }
    }

    public bool IsRating5
    {
        get => SelectedRating == 5;
        set
        {
            if (value) SelectedRating = 5;
        }
    }

    public string Comment
    {
        get => _comment;
        set
        {
            if (_comment != value)
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
    }

    public ICommand RemovePictureCommand { get; }

    public ReviewTourFormViewModel()
    {
        RemovePictureCommand = new RelayCommand(param =>
        {
            if (param is string imagePath && !string.IsNullOrEmpty(imagePath))
            {
                RemovePicture(imagePath);
            }
        });
    }

    private void RemovePicture(string imagePath)
    {
        if (ImagePaths.Contains(imagePath))
        {
            ImagePaths.Remove(imagePath);
        }
    }

    // Implement INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}