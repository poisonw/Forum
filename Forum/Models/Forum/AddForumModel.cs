using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models.Forum
{
    public class AddForumModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }
    }
}