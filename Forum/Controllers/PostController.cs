using Forum.Data.Context;
using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Models.Post;
using Forum.Models.Reply;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Controllers
{

    [Authorize]
    public class PostController : Controller
    {
        private readonly IPost _postservice;
        private readonly IMyForum _myForumService;
       private static UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;

        public PostController(IPost postService, IMyForum myForumService, UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _postservice = postService;
            _myForumService = myForumService;
            _userManager = userManager;
            _userService = userService;
        }

        public ActionResult Index(int id)
        {
            var post = _postservice.GetById(id);
            var replies = BuildPostReplies(post.Replies);
            var model = new PostIndexModel
                {
                  Id = post.Id,
                  Title = post.Title,
                  AuthorId = post.User.Id,
                  AuthorName = post.User.UserName,
                  AuthorImageUrl = post.User.ProfileImageUrl,
                  AuthorRating = post.User.Rating,
                  Created = post.Created,
                  PostContent = post.Content,
                  Replies = replies,
                  ForumId = post.MyForum.Id,
                  ForumName = post.MyForum.Title,
                  IsAuthorAdmin = IsAuthorAdmin(post.User)  
            };
            return View(model);
        }

        private bool  IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user.Id).Result.Contains("Admin");
        }
        [Authorize]
        public ActionResult Create(int id)
        {
            var forum = _myForumService.GetById(id);
            var model = new NewPostModel
            {
                ForumId = forum.Id,
                ForumName = forum.Title,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddPost(NewPostModel model)
        {
            var userId = User.Identity.GetUserId();
            var user =  await _userManager.FindByIdAsync(userId);
            var post = BuildPost(model, user);
            await _postservice.Add(post);
            await _userService.UpdateUserRating(userId, typeof(Post));
            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _myForumService.GetById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                MyForum = forum
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content,
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        }
    }
}