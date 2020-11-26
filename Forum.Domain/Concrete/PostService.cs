using Forum.Data.Context;
using Forum.Data.Models;
using Forum.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Domain.Concrete
{
    public class PostService : IPost, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Post post)
        {
            _context.Posts.Add(post);
          await  _context.SaveChangesAsync();
            
           
            
  
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Replies.Select(r => r.User))
                .Include(p => p.MyForum);
        }

        public Post GetById(int id)
        {
            var res = _context.Posts.Where(p => p.Id == id)
                .Include(p => p.User)
                .Include(p => p.Replies.Select(r => r.User))
                .Include(p => p.MyForum)
                .First();
            return (res);
        }

        public IEnumerable<Post> GetFilteredPosts(MyForum forum, string searchQuery)
        {
            
            return string.IsNullOrEmpty(searchQuery)
                ? forum.Posts
                : forum.Posts.Where(post => post.Title.Contains(searchQuery)
                || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.MyForums
                .Where(f => f.Id == id).First()
                .Posts;
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

        public IEnumerable<Post> GetLatestPosts(int n)
        {
            return GetAll().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return GetAll().Where(post
                => post.Title.Contains(searchQuery)
                || post.Content.Contains(searchQuery));
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }
    }
}
