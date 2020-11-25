using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Models.ApplicationUser;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        
        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
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

            

            return RedirectToAction("Detail", "Profile", new { id = userId});
        }
    }

}