using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class ForumCommentInputViewModel : INotifyPropertyChanged
    {
        public event EventHandler RequestClose;
        public ICommand AddCommand { get; }
        private ForumCommentService _service;
        private string _answer;
        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged(nameof(Answer));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ForumCommentInputViewModel(Domain.Model.Owner loggedInOwner, ForumDisplayOwnerDTO forum)
        {
            LoggedInOwner = loggedInOwner;
            Forum = forum;
            _service = new ForumCommentService(Injector.CreateInstance<IForumCommentRepository>());
            AddCommand = new RelayCommand(Add);
        }

        private void Add(object obj)
        {
            ForumComment newComment = new ForumComment(LoggedInOwner.Id, Forum.Id, Answer, true, 0);
            _service.Save(newComment);
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        public Domain.Model.Owner LoggedInOwner { get; }
        public ForumDisplayOwnerDTO Forum { get; }
    }
}
