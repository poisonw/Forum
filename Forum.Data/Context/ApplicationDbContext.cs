using Forum.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Forum.Data.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
    : base("DefaultConnection",throwIfV1Schema: false)
        {
        }
       // public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<MyForum> MyForums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }
          public static ApplicationDbContext Create()
        {
          return new ApplicationDbContext();
         }
    }
}
