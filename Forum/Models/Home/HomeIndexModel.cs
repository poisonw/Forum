﻿using Forum.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models.Home
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<PostListingModel> LatestPosts { get; set; }
    }
}