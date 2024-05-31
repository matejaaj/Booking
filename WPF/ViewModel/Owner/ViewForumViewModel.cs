using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using BookingApp.WPF.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.Owner
{
    public class ViewForumViewModel : INotifyPropertyChanged
    {
        private Domain.Model.Owner _loggedInOwner;
        private DTO.ForumDisplayOwnerDTO _forum;
        public string Location { get; set; }
        private ForumCommentService _commentService;
        private UserService _userService;
        private ViewForumPage _currentPage;
        public ICommand ReportCommand { get; }
        public ICommand AddCommand { get; }
        public CommentDisplayOwnerDTO SelectedComment { get; set; }

        private ObservableCollection<CommentDisplayOwnerDTO> _comments;
        public ObservableCollection<CommentDisplayOwnerDTO> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ViewForumViewModel(Domain.Model.Owner loggedInOwner, DTO.ForumDisplayOwnerDTO forum, ViewForumPage viewForumPage)
        {
            _loggedInOwner = loggedInOwner;
            _forum = forum;
            _currentPage = viewForumPage;
            Location = forum.Location;
            _commentService = new ForumCommentService(Injector.CreateInstance<IForumCommentRepository>());
            _userService = new UserService(Injector.CreateInstance<IUserRepository>());
            ReportCommand = new RelayCommand(Report);
            AddCommand = new RelayCommand(Add);
            Update();
        }

        private void Add(object obj)
        {
            ForumCommentInput input = new ForumCommentInput(_loggedInOwner, _forum);
            input.Owner = Window.GetWindow(_currentPage);
            input.Closed += (s, args) => Update();
            input.Show();
        }

        private void Report(object obj)
        {
            var dto = (CommentDisplayOwnerDTO)obj;
            ForumComment comment = _commentService.GetById(dto.Id);
            comment.ReportNumber++;
            _commentService.Update(comment);
            Update();
        }

        private void Update()
        {
            Comments = new ObservableCollection<CommentDisplayOwnerDTO>();
            List<ForumComment> comments = _commentService.GetByForumId(_forum.Id);
            foreach (ForumComment comment in comments)
            {
                var user = _userService.GetById(comment.UserId);
                Comments.Add(new CommentDisplayOwnerDTO(user, comment));
            }
        }
    }
}
