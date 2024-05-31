using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.UseCases
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository;

        public ForumService(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }

        public Forum Save(Forum forum)
        {
            return _forumRepository.Save(forum);
        }

        public void Delete(Forum forum)
        {
            _forumRepository.Delete(forum);
        }

        public Forum Update(Forum forum)
        {
            return _forumRepository.Update(forum);
        }

        public Forum GetById(int id)
        {
            return _forumRepository.GetById(id);
        }

        public List<Forum> GetByLocation(Location location)
        {
            return _forumRepository.GetByLocation(location);
        }

        public int NextId()
        {
            return _forumRepository.NextId();
        }
        public Forum SaveForum(LocationDTO selectedLocation, string comment)
        {
            var newForum = new Forum
            {
                LocationId = selectedLocation.Id,
                Comment = comment,
                IsCancelled = false
            };

            return Save(newForum);
        }
        public bool UserIsCreator(int forumId, int userId)
        {
            var forums = GetByUserId(userId);
            var flag = false;
            foreach(var forum in forums)
            {
                if(forum.Id == forumId)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private List<Forum> GetByUserId(int userId)
        {
            var userForums = GetAll().Where(forum => forum.UserId == userId).ToList();
            return userForums;
        }
    }
}
