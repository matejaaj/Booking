using BookingApp.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Linq;

public class TourInstanceViewModel : INotifyPropertyChanged
{
    private int id;
    private string name;
    private string currentCheckpoint;
    private DateTime date;
    private List<TourGuest> guests = new List<TourGuest>();
    private List<string> checkpointNames = new List<string>();
    private bool isFinished;
    private string imagePath;

    public int Id
    {
        get => id;
        set
        {
            if (id != value)
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }

    public string Name
    {
        get => name;
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public string CurrentCheckpoint
    {
        get => currentCheckpoint;
        set
        {
            if (currentCheckpoint != value)
            {
                currentCheckpoint = value;
                OnPropertyChanged(nameof(CurrentCheckpoint));
                OnPropertyChanged(nameof(CurrentCheckpointDisplay));
            }
        }
    }

    public string CurrentCheckpointDisplay
    {
        get
        {
            if (CurrentCheckpoint == "START")
            {
                return "Tura nije počela";
            }

            if (CurrentCheckpoint == "END")
            {
                return "Tura se završila";
            }

            return CurrentCheckpoint;
        }
    }

    public DateTime Date
    {
        get => date;
        set
        {
            if (date != value)
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
    }

    public List<TourGuest> Guests
    {
        get => guests;
        set
        {
            if (guests != value)
            {
                guests = value;
                OnPropertyChanged(nameof(Guests));
                OnPropertyChanged(nameof(DisplayGuests));
            }
        }
    }

    public List<string> CheckpointNames
    {
        get => checkpointNames;
        set
        {
            if (checkpointNames != value)
            {
                checkpointNames = value;
                OnPropertyChanged(nameof(CheckpointNames));
            }
        }
    }

    public bool IsFinished
    {
        get => isFinished;
        set
        {
            if (isFinished != value)
            {
                isFinished = value;
                OnPropertyChanged(nameof(IsFinished));
            }
        }
    }

    public string ImagePath
    {
        get => imagePath;
        set
        {
            if (imagePath != value)
            {
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }
    }

    public string DisplayGuests
    {
        get
        {
            if (Guests != null && Guests.Count > 3)
            {
                var firstThreeGuests = Guests.Take(3).Select(g => g.Name).Aggregate((i, j) => i + "\n" + j);
                return $"{firstThreeGuests} ...";
            }

            return Guests == null ? string.Empty : string.Join("\n", Guests.Select(g => g.Name));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
