using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        private const string FilePath = "../../../Resources/Data/forum_comments.csv";
        private readonly Serializer<ForumComment> _serializer;
        private List<ForumComment> _comments;

        public ForumCommentRepository()
        {
            _serializer = new Serializer<ForumComment>();
            _comments = _serializer.FromCSV(FilePath);
        }

        public List<ForumComment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ForumComment Save(ForumComment comment)
        {
            comment.Id = NextId();
            _comments = _serializer.FromCSV(FilePath);
            _comments.Add(comment);
            _serializer.ToCSV(FilePath, _comments);
            return comment;
        }

        public int NextId()
        {
            _comments = _serializer.FromCSV(FilePath);
            if (_comments.Count < 1)
            {
                return 1;
            }
            return _comments.Max(c => c.Id) + 1;
        }

        public void Delete(ForumComment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            ForumComment founded = _comments.Find(c => c.Id == comment.Id);
            _comments.Remove(founded);
            _serializer.ToCSV(FilePath, _comments);
        }

        public ForumComment Update(ForumComment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            ForumComment current = _comments.Find(c => c.Id == comment.Id);
            int index = _comments.IndexOf(current);
            _comments.Remove(current);
            _comments.Insert(index, comment);
            _serializer.ToCSV(FilePath, _comments);
            return comment;
        }

        public ForumComment GetById(int id)
        {
            _comments = _serializer.FromCSV(FilePath);
            return _comments.Find(c => c.Id == id);
        }

        public List<ForumComment> GetByForumId(int forumId)
        {
            _comments = _serializer.FromCSV(FilePath);
            return _comments.FindAll(c => c.ForumId == forumId);
        }

        public List<ForumComment> GetByUserId(int userId)
        {
            _comments = _serializer.FromCSV(FilePath);
            return _comments.FindAll(c => c.UserId == userId);
        }
    }
}
