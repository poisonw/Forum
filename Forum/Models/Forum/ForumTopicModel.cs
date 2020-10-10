using Forum.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
    }
}