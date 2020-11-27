using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Abstract
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();
        Task SetProfileImage(string id, string uri);
        Task UpdateUserRating(string userId, Type type);
    }
}
