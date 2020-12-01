using Forum.Data.Context;
using Forum.Data.Models;
using Forum.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Domain.Concrete
{
    public class MyForumService : IMyForum , IDisposable
    {
        private readonly ApplicationDbContext _context;
        public MyForumService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(MyForum myForum)
        {
            _context.MyForums.Add(myForum);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int myForumId)
        {
            var forum = GetById(myForumId);
            _context.MyForums.Remove(forum);
            await _context.SaveChangesAsync();
        }



        public IEnumerable<MyForum> GetAll()
        {
            
             return _context.MyForums.Include(f => f.Posts).ToList();
        }

        public IEnumerable<ApplicationUser> GetActiveUsers(int id)
        {
            var posts = GetById(id).Posts;

           if (posts != null || !posts.Any())
           {
                var postUsers = posts.Select(p => p.User);
                var replyUsers = posts.SelectMany(p => p.Replies).Select(r => r.User);

                return postUsers.Union(replyUsers).Distinct();
            }
            return new List<ApplicationUser>();
        }

        public MyForum GetById(int id)
        {
            var forum = _context.MyForums.Where(f => f.Id == id)
                          .Include(p => p.Posts.Select(u => u.User))
                          .Include(p => p.Posts.Select(r => r.Replies.Select(u => u.User)))
                          .FirstOrDefault();
            
            return forum;
        }

        public bool HasRecentPost(int id)
        {
            const int hoursAgo = 12;
            var window = DateTime.Now.AddDays(-hoursAgo);
            return GetById(id).Posts.Any(post => post.Created > window);
        }

        public Task UpdateForumDescription(int myForumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int myForumId, string newTitle)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }

}
