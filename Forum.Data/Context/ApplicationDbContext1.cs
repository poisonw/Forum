using Forum.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Context
{
   public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
            :base("DefaultConnection")
        {
        }
      //  public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    //    public virtual DbSet<IdentityUserLogin> UserLogins { get; set; }
    //    public virtual DbSet<IdentityUserClaim> UserClaims { get; set; }
   //     public virtual DbSet<IdentityRole> IdentityRoles { get; set; }
     //   public virtual DbSet<IdentityUserRole> UserRoles { get; set; }
        public DbSet<MyForum> MyForums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
