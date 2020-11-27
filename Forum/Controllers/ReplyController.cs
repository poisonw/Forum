using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Models.Reply;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        public ReplyController(IPost postService, UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _postService = postService;
            _userManager = userManager;
            _userService = userService;
        }

        // GET: Reply
        public async Task<ActionResult> Create(int id)
        {
            var post = _postService.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                PostContent = post.Content,
                PostTitle = post.Title,
                PostId = post.Id,

                AuthorId = user.Id,
                AuthorName = User.Identity.Name,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorRating = user.Rating,
                IsAuthorAdmin = User.IsInRole("Admin"),

                ForumId = post.MyForum.Id,
                ForumName = post.MyForum.Title,
                ForumImageUrl = post.MyForum.ImageUrl,

                Created = DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddReply(PostReplyModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);
            await _postService.AddReply(reply);
            await _userService.UpdateUserRating(userId, typeof(PostReply));
            return RedirectToAction("Index", "Post", new { id = model.PostId});
        }

        private PostReply BuildReply(PostReplyModel model, ApplicationUser user)
        {
            var post = _postService.GetById(model.PostId);
            return new PostReply
            { 
             Post = post,
             Content = model.ReplyContent,
             Created = DateTime.Now,
             User = user
            };
        }
    }
}