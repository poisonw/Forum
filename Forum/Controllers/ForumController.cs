using Forum.Data.Context;
using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Domain.Concrete;
using Forum.Models.Forum;
using Forum.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Forum.Data.Models.ApplicationUser;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IMyForum _myForumService;
        private readonly IPost _postService;


        public ForumController(IMyForum myForumService, IPost postService)

        {
            _myForumService = myForumService;
            _postService = postService;

        }


        public ActionResult Index()
        {
            var forums = _myForumService.GetAll()
                .Select(forum => new ForumListingModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description
                });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return View(model);
        }

        public ActionResult Topic(int id, string searchQuery)
        {
            var forum = _myForumService.GetById(id);
            var posts = new List<Post>();

                posts = _postService.GetFilteredPosts(forum, searchQuery).ToList();
            


            
            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                AuthorName = post.User.UserName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });
            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", "Forum", new { id, searchQuery});
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.MyForum;
            return BuildForumListing(forum);
        }

        private ForumListingModel BuildForumListing(MyForum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }



    }
}