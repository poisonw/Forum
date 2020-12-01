using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Models.ApplicationUser;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;


        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _userManager = userManager;
            _userService = userService;

        }


        public ActionResult Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user.Id).Result;
            var model = new ProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemeberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UploadProfileImage(HttpPostedFileBase file)
        {
            var userId = User.Identity.GetUserId();

            if (file != null)
            {
                var imageUri = "/images/users/" + file.FileName;
                var path = Server.MapPath("~" + imageUri);
                file.SaveAs(path);

                await _userService.SetProfileImage(userId, imageUri);
            }



            return RedirectToAction("Detail", "Profile", new { id = userId });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var profiles = _userService.GetAll()
                .OrderByDescending(user => user.Rating)
                .Select(user => new ProfileModel
                {
                   Email = user.Email,
                   UserName = user.UserName,
                   ProfileImageUrl = user.ProfileImageUrl,
                   UserRating = user.Rating.ToString(),
                   MemeberSince = user.MemberSince
                });

            var model = new ProfileListModel
            {
                Profiles = profiles
            };
            
                
            return View(model);
        }
    }

}