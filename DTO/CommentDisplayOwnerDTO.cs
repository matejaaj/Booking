using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class CommentDisplayOwnerDTO : INotifyPropertyChanged
    {
        private int id { get; set; }
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string authorName { get; set; }

        public string AuthorName
        {
            get { return authorName; }
            set
            {
                if (authorName != value)
                {
                    authorName = value;
                    OnPropertyChanged("AuthorName");
                }
            }
        }

        private string comment { get; set; }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private string iconPath { get; set; }
        public string IconPath
        {
            get { return iconPath; }
            set
            {
                if (iconPath != value)
                {
                    iconPath = value;
                    OnPropertyChanged("IconPath");
                }
            }
        }

        private bool wasPresent { get; set; }

        public bool WasPresent
        {
            get { return wasPresent; }
            set
            {
                if (wasPresent != value)
                {
                    wasPresent = value;
                    OnPropertyChanged("WasPresent");
                }
            }
        }

        private int reports { get; set; }
        public int Reports
        {
            get { return reports; }
            set
            {
                if (reports != value)
                {
                    reports = value;
                    OnPropertyChanged("Reports");
                }
            }
        }

        public CommentDisplayOwnerDTO(User user, ForumComment comment)
        {
            Id = comment.Id;
            Reports = comment.ReportNumber;
            Comment = comment.Comment;
            WasPresent = comment.WasPresent;
            AuthorName = user.Username;
            if (WasPresent)
            {
                IconPath = "../../../Resources/Images/check.png";
            }
            else
            {
                IconPath = "../../../Resources/Images/cross.png";
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
