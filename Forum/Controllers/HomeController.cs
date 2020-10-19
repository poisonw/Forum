using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Models.Forum;
using Forum.Models.Home;
using Forum.Models.Post;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;
       // private readonly SignInManager<ApplicationUser,> _signInManager;
        public HomeController(IPost postService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;

        }

        public ActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPosts = _postService.GetLatestPosts(10);
            var posts = latestPosts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = GetForumListingForPost(post)
            });
            return new HomeIndexModel
            {
                LatestPosts = posts,
                SearchQuery = ""
               
            };
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            var forum = post.MyForum;
            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                ImageUrl = forum.ImageUrl
            };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult User()
        {
             
            
            return PartialView();
        }
    }
}