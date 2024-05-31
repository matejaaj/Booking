using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumCommentRepository
    {
        List<ForumComment> GetAll();
        ForumComment Save(ForumComment comment);
        void Delete(ForumComment comment);
        ForumComment Update(ForumComment comment);
        ForumComment GetById(int id);
        List<ForumComment> GetByForumId(int forumId);
        List<ForumComment> GetByUserId(int userId);
    }
}
