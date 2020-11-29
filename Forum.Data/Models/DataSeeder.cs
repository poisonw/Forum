using Forum.Data.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Models
{
    public class DataSeeder
    {
        public ApplicationDbContext _context;
        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

         public async Task SeedSuperUser()
        {
            var roleManager = new RoleStore<IdentityRole>(_context);
            var userManager = new UserStore<ApplicationUser>(_context);
 

            var user = new ApplicationUser
            {
                UserName = "Admin",
                Email = "test@admin.admin",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                MemberSince = DateTime.Now
            };
            var hasher = new PasswordHasher();
            var heshedPassword = hasher.HashPassword("Zz123+");
            user.PasswordHash = heshedPassword;

            var hasAdminRole = _context.Roles.Any(role => role.Name == "Admin");
            if (!hasAdminRole)
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
            }

            var hasSuperUser = _context.Users.Any(u => u.UserName == user.UserName);
            if (!hasSuperUser)
            {
                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "Admin");
            }


            await _context.SaveChangesAsync();

            await Task.CompletedTask;
        }
    }
}
