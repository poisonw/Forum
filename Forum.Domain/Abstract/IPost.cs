using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Domain.Abstract
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);

        Task Add(Post post);

        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
    }
}
