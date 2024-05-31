using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class ForumCommentService
    {
        private readonly IForumCommentRepository _commentRepository;

        public ForumCommentService(IForumCommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public List<ForumComment> GetAll()
        {
            return _commentRepository.GetAll();
        }
        public ForumComment Save(ForumComment comment)
        {
            return _commentRepository.Save(comment);
        }
        public void Delete(ForumComment comment)
        {
            _commentRepository.Delete(comment);
        }
        public ForumComment Update(ForumComment comment)
        {
            return _commentRepository.Update(comment);
        }
        public ForumComment GetById(int id)
        {
            return _commentRepository.GetById(id);
        }
        public List<ForumComment> GetByForumId(int forumId)
        {
            return _commentRepository.GetByForumId(forumId);
        }
        public List<ForumComment> GetByUserId(int userId)
        {
            return _commentRepository.GetByUserId(userId);
        }
    }
}
