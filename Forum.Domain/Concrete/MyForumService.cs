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
        public Task Create(MyForum myForum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int myForumId)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<MyForum> GetAll()
        {
            
             return _context.MyForums.Include(f => f.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public MyForum GetById(int id)
        {
            var forum = _context.MyForums.Where(f => f.Id == id)
                          .Include(p => p.Posts.Select(u => u.User))
                          .Include(p => p.Posts.Select(r => r.Replies.Select(u => u.User)))
                          .FirstOrDefault();
            return forum;
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
