using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Domain.Abstract
{
    public interface IMyForum
    {
        MyForum GetById(int id);
        IEnumerable<MyForum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(MyForum myForum);
        Task Delete(int myForumId);
        Task UpdateForumTitle(int myForumId, string newTitle);
        Task UpdateForumDescription(int myForumId, string newDescription);
    }
}
