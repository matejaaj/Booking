using System.Collections.ObjectModel;
using System.ComponentModel;
using BookingApp;
using BookingApp.Domain.Model;

public class ReviewTourFormViewModel : INotifyPropertyChanged, IDataErrorInfo
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
        set { if (value) SelectedRating = 1; }
    }

    public bool IsRating2
    {
        get => SelectedRating == 2;
        set { if (value) SelectedRating = 2; }
    }

    public bool IsRating3
    {
        get => SelectedRating == 3;
        set { if (value) SelectedRating = 3; }
    }

    public bool IsRating4
    {
        get => SelectedRating == 4;
        set { if (value) SelectedRating = 4; }
    }

    public bool IsRating5
    {
        get => SelectedRating == 5;
        set { if (value) SelectedRating = 5; }
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

    public string this[string columnName]
    {
        get
        {
            string result = null;
            switch (columnName)
            {
                case nameof(Comment):
                    if (string.IsNullOrWhiteSpace(Comment))
                        result = TranslationSource.Instance["ValidationComment"];
                    break;
                case nameof(SelectedRating):
                    if (SelectedRating < 1 || SelectedRating > 5)
                        result = TranslationSource.Instance["ValidationRating"];
                    break;
            }
            return result;
        }
    }

    public string Error => null;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
