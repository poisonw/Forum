using Forum.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Data.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
    : base("DefaultConnection",throwIfV1Schema: false)
        {
        }
        public DbSet<MyForum> MyForums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }

        public static ApplicationDbContext Create()
        {
          return new ApplicationDbContext();
         }
    }
}
